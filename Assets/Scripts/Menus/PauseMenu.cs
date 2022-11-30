using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public KeyCode interactkey;
	public GameObject menu;
	public GameObject optMenu;
	private bool isOpen = false;
	private bool isOptOpen = false;

	void Update() {
#if UNITY_WEBGL 
		if(!Input.GetKeyDown(KeyCode.P)) return;
#else
		if(!Input.GetKeyDown(interactkey)) return;
#endif

		if(isOpen) {
			if(isOptOpen) {
				optMenu.SetActive(false);
				menu.SetActive(true);
				isOptOpen = false;
			} else {
				Time.timeScale = 1;
				menu.SetActive(false);
				isOpen = false;
				Cursor.visible = false;
			}
		} else {
			Time.timeScale = 0;
			menu.SetActive(true);
			isOpen = true;
			Cursor.visible = true;
		}
	}

	public void openOptionsMenu() {
		menu.SetActive(false);
		optMenu.SetActive(true);
		isOptOpen = true;
	}

    public void quit() {
		Application.Quit();
	}

	public void closeMenu() {
		Time.timeScale = 1;
		menu.SetActive(false);
		isOpen = false;
		Cursor.visible = false;
	}
}
