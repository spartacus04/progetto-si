using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class Opzioni : MonoBehaviour
{
    public Dropdown qualityDropDown;
    public Dropdown resolutionsDropDown;
    public Toggle FullScreenToggle;
    private int ScreenInt;
    Resolution[] resolutions;
    private bool isFullScreen;
    const string prefName = "optionvalue";
    const string resName = "resolutionoption";

    private void Awake()
    {
        ScreenInt = PlayerPrefs.GetInt("togglestate");
        if (ScreenInt == 1) 
        {
            isFullScreen = true;
            FullScreenToggle.isOn = true;

        }else
        {
            FullScreenToggle.isOn = false;

        }
        resolutionsDropDown.onValueChanged.AddListener(new UnityAction<int>(Index => {
            PlayerPrefs.SetInt(resName, resolutionsDropDown.value);
            PlayerPrefs.Save();

        }));


          qualityDropDown.onValueChanged.AddListener(new UnityAction<int>(Index => {
            PlayerPrefs.SetInt(prefName, qualityDropDown.value);
            PlayerPrefs.Save();

        }));

}
      private void Start()
     {
        qualityDropDown.value = PlayerPrefs.GetInt(prefName, 3);
        resolutions = Screen.resolutions;
        resolutionsDropDown.ClearOptions();
        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRate + "Hz";
            options.Add(option);
            if(resolutions[i].width==Screen.currentResolution.width
                && resolutions[i].height==Screen.currentResolution.height 
                && resolutions[i].refreshRate == Screen.currentResolution.refreshRate)
            {
                currentResolutionIndex = i;
            }
        }
           resolutionsDropDown.AddOptions(options);
           resolutionsDropDown.value = PlayerPrefs.GetInt(resName, currentResolutionIndex);
           resolutionsDropDown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

    }
    public void setQuality(int Quality)
    {
        QualitySettings.SetQualityLevel(Quality);

    }

    public void setFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
        if (isFullScreen==false) 
        {

            PlayerPrefs.SetInt("togglestate", 0);
        
        }
                   
        else
        {
            isFullScreen = true;
            PlayerPrefs.SetInt("togglestate", 1);
        }
    }

}
    