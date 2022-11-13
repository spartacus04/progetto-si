using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tilemap : MonoBehaviour, ThemeHandler
{
    public bool isDesert;

    void disableChilds() {
        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(false);
        }
    }

    void enableChilds() {
        foreach (Transform item in transform)
        {
            item.gameObject.SetActive(true);
        }
    }

    public void onDesert()
    {
        if(isDesert) enableChilds();
        else disableChilds();
    }

    public void onOcean()
    {
        if(isDesert) disableChilds();
        else enableChilds();
    }
}
