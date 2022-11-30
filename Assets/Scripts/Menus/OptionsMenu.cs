using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Linq;

public class OptionsMenu : MonoBehaviour
{
	public Dropdown qualityDropDown;
	public Dropdown resolutionDropdown;
	public Toggle FullScreenToggle;

	private int ScreenInt;
	Resolution[] resolutions;
	private bool isFullScreen;
	public const string prefix = "options";

	// Tengo a mente 2 variabili in vista di una possibile implementazione di un secondo menu per applicare le impostazioni
	private Options oldOptions;
	private Options newOptions;

	void loadValues() {
		oldOptions = Options.load();
		newOptions = oldOptions;

		qualityDropDown.value = oldOptions.quality;
		resolutionDropdown.value = oldOptions.resolution;
		FullScreenToggle.isOn = oldOptions.fullscreen;
	}

	private void Start()
	{
		resolutionDropdown.ClearOptions();

		resolutionDropdown.AddOptions(Screen.resolutions.Select(
			r => $"{r.width}x{r.height}@{r.refreshRate}Hz"
		).ToList());

		int index = Screen.resolutions.ToList().FindIndex(r => r.Equals(Screen.currentResolution));
		resolutionDropdown.value = index;

		loadValues();
		resolutionDropdown.RefreshShownValue();

		// Add dropdown listeners
		resolutionDropdown.onValueChanged.AddListener(new UnityAction<int>(i => {
			newOptions.resolution = i;
		}));

		qualityDropDown.onValueChanged.AddListener(new UnityAction<int>(i => {
			newOptions.quality = i;
		}));

		FullScreenToggle.onValueChanged.AddListener(new UnityAction<bool>(i => {
			newOptions.fullscreen = i;
		}));
	}

	public void Apply() {
		newOptions.Apply();
		newOptions.save();

		oldOptions = newOptions;
	}
}


struct Options {
	public Options(int quality, int resolution, bool fullscreen) {
		this.quality = quality;
		this.resolution = resolution;
		this.fullscreen = fullscreen;
	}

	public int quality;
	public int resolution;
	public bool fullscreen;

	public static string getKey(string key) {
		return $"{OptionsMenu.prefix}.{key}";
	}

	public static Options load() {
		return new Options(
			PlayerPrefs.GetInt(getKey("quality"), 2),
			PlayerPrefs.GetInt(getKey("resolution"), 0),
			PlayerPrefs.GetInt(getKey("fullscreen"), 1) == 1
		);
	}

	public void save() {
		PlayerPrefs.SetInt(getKey("quality"), quality);
		PlayerPrefs.SetInt(getKey("resolution"), resolution);
		PlayerPrefs.SetInt(getKey("fullscreen"), fullscreen ? 1 : 0);
		PlayerPrefs.Save();
	}

	public void Apply() {
		var res = Screen.resolutions[resolution];
		Screen.SetResolution(res.width, res.height, Screen.fullScreen);

		QualitySettings.SetQualityLevel(quality);

		Screen.fullScreen = fullscreen;
	}
}