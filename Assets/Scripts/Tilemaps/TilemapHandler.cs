using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using System.Linq;

public class TilemapHandler : MonoBehaviour, ThemeHandler
{
	private Tilemap[] tilemaps;
    public bool isDesert;

	void Start()
	{
		tilemaps = GetComponentsInChildren<Tilemap>();
	}

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
        //if(isDesert) enableChilds();
        //else disableChilds();

		tilemaps.ToList().ForEach(e => e.RefreshAllTiles());
    }

    public void onOcean()
    {
        //if(isDesert) disableChilds();
        //else enableChilds();
		tilemaps.ToList().ForEach(e => e.RefreshAllTiles());
    }
}
