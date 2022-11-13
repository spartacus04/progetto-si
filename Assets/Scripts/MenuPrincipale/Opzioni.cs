using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using TMPro;

public class Opzioni : MonoBehaviour
{
#nullable enable
    string datapath;

    ConfigData data;
    Resolution[]  resolutions;
    List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    private int currentResolutionIndex;
    public Dropdown dropdownres;
    [SerializeField] Toggle fullscreen;


     [SerializeField]
    TextMeshProUGUI qualita;
    [SerializeField]

    TextMeshProUGUI? risoluzione;
    [SerializeField]

    TextMeshProUGUI fulls;

#nullable disable

    public void AggiornaTxT()
	{
        if (data.qualitysettings == 2)
		{
            qualita.text = "Qualità: Media";
            
        }

		if (data.qualitysettings == 3)
		{
            qualita.text = "Qualità: Alta";
		}
       if (data.qualitysettings == 5)
        {
            qualita.text = "Qualità: Ultra";
        }

        risoluzione.text = $"Risoluzione: {data.resWidth}x{data.resheight}";

		if (data.fullscreen)
		{
            fulls.text = "FullScreen: ON";
		}
		else
		{

            fulls.text = "FullScreen: OFF";
        }

    }

    void Start()
    {

        #region res;
        
        
		resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();
        dropdownres.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

		for (int i = 0; i < resolutions.Length; i++)
		{
            if(resolutions[i].refreshRate == currentRefreshRate)
			{
                filteredResolutions.Add(resolutions[i]);

			}

		}

        List<string> options = new List<string>();
		for (int i = 0; i < filteredResolutions.Count; i++)
		{ 
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height) 
			{
                currentResolutionIndex = i;
			}

		}
        dropdownres.AddOptions(options);
        dropdownres.value = currentResolutionIndex;
        
        dropdownres.RefreshShownValue();
        #endregion
        #region datapath&load
        datapath = $"{ Application.persistentDataPath}/Settings";
        print(datapath);
        if (!Directory.Exists(datapath))
        {

            Directory.CreateDirectory(datapath);
        }
        Load();
        
        if (data.resheight!=0 && data.resWidth!=0)
        {
            SetRess();
            print($"{data.resWidth}x{data.resheight}");
        }
        else
        {
            data.resheight = Screen.height;
            data.resWidth = Screen.width;
            Screen.SetResolution(Screen.width, Screen.height, true);
            print($"{Screen.width}x{Screen.height}");
        }


            FullScreen();
         
	
		if(!data.fullscreen)
		{
            Screen.fullScreen = false;
            print(data.fullscreen);
		}

        if (data.qualitysettings != 0)
        {
            SetQuality();
            print(data.qualitysettings);
        }


#endregion
        AggiornaTxT();

    }


    #region quality
    public void SetQuality()
    {
        QualitySettings.SetQualityLevel(data.qualitysettings);

    }

    public void setData(int qualityindex)
    {


        data.qualitysettings = qualityindex;

    }
    #endregion

    #region save&loadMethod
    public void save()
    {

        

        string jsonData = JsonUtility.ToJson(data, true);
        File.WriteAllText($"{datapath}/settings.config", jsonData);
    }
    public void Load()
    {

        if (!File.Exists($"{datapath}/settings.config"))
        {
            data = new ConfigData();
            data.qualitysettings = 5;
            data.fullscreen = true;
            string jsonDatas = JsonUtility.ToJson(data, true);
            File.WriteAllText($"{datapath}/settings.config", jsonDatas);
            return;
        }

        string jsonData = File.ReadAllText($"{datapath}/settings.config");
        data = JsonUtility.FromJson<ConfigData>(jsonData);

        fullscreen.isOn = data.fullscreen;
        
    }
    #endregion


    #region fullscreen
	public void FullScreen()
    {
        
        Screen.fullScreen = data.fullscreen;
		
       
    }
    public void SetDatafullscreen()
	{
       
            data.fullscreen = fullscreen.isOn;
            
	}
	#endregion

	#region resolution
	public void SetRes(int resIndex)
    {
        
        Resolution res = resolutions[resIndex];


        data.resWidth = res.width;
        data.resheight = res.height;
  

    }

    public void SetRess()
	{
        Screen.SetResolution(data.resWidth, data.resheight, true);
    }

    #endregion
}


[Serializable]
public class ConfigData
{
    public int resWidth;
    public int resheight;
    public bool fullscreen;
    public int qualitysettings;
}
