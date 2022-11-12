using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ThemeSwitcher : MonoBehaviour
{
    public bool isDesert;
    public ThemeHandler[] handlers;
    void Start()
    {
        // Utilizzo sto schifo per prendere tutti gli oggetti che implementano l'interfaccia ThemeHandler
        var mObjs = GameObject.FindObjectsOfType<MonoBehaviour>();
        handlers = (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(ThemeHandler)) select (ThemeHandler)a).ToArray();
    }

    public void fireChange() {
        if(isDesert) {
            foreach(var handler in handlers) {
                handler.onDesert();
            }
        } else {
            foreach(var handler in handlers) {
                handler.onOcean();
            }
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            isDesert = !isDesert;
            fireChange();
        }
    }
}
