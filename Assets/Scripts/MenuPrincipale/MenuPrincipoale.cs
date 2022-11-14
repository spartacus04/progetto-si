using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuPrincipoale : MonoBehaviour
{
    [SerializeField] string indexscena;
    public void Play()
	{
        SceneManager.LoadScene(indexscena);
	}

    public void Quit()
    {
        Application.Quit();
    }
}
