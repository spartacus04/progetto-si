using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuinGame : MonoBehaviour
{
    [SerializeField] KeyCode interactkey;
	[SerializeField] GameObject Menu;
	[SerializeField] GameObject MenuOptions;

	void Update()
    {
		if (Input.GetKeyDown(interactkey) && !Menu.activeSelf && !MenuOptions.activeSelf)
		{
			Time.timeScale = 0;
			Menu.SetActive(true);
		}else if(Input.GetKeyDown(interactkey) && Menu.activeSelf && !MenuOptions.activeSelf)
		{
			Menu.SetActive(false);
		}else if (Input.GetKeyDown(interactkey) && !Menu.activeSelf && MenuOptions.activeSelf)
		{
			MenuOptions.SetActive(false);
			Menu.SetActive(true);

		}


		if (!Menu.activeSelf && !MenuOptions.activeSelf)
		{
			Time.timeScale = 1;
		}

	}
}
