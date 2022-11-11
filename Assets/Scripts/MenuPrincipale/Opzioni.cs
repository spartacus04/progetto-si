using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;

public class Opzioni : MonoBehaviour
{
    string datapath;
    public GameObject OpzioniCanvas;
    ConfigData data;
    Resolution[] resolutions;
    List<Resolution> filteredResolutions;
    private float currentRefreshRate;
    private int currentResolutionIndex;
    public Dropdown dropdownres;

    void Start()
    {
        Screen.SetResolution(Screen.width, Screen.height, true);
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
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + "" + filteredResolutions[i].refreshRate + "Hz";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height) 
			{
                currentResolutionIndex = i;
			}

		}
        dropdownres.AddOptions(options);
        dropdownres.value = currentResolutionIndex;
        dropdownres.RefreshShownValue();
        #region datapath&load
        datapath = $"{ Application.persistentDataPath}/Settings";
        print(datapath);
        Load();
        #endregion

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OpzioniCanvas.SetActive(false);
        }

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

        if (!Directory.Exists(datapath))
        {

            Directory.CreateDirectory(datapath);
        }

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


        
    }
    #endregion


    #region fullscreen
    [SerializeField] Toggle fullscreen;
	public void FullScreen()
    {
        Screen.fullScreen = data.fullscreen;
    }
    public void SetDatafullscreen()
	{
        if (fullscreen.isOn)
            data.fullscreen = true;
        else
            data.fullscreen = false;
	}
    #endregion

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

}


[Serializable]
public class ConfigData
{
    public int resWidth;
    public int resheight;
    public bool fullscreen;
    public int qualitysettings;
}
