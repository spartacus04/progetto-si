using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trash : MonoBehaviour, ThemeHandler
{
    private SpriteRenderer spriteRenderer;
    public Sprite d;
    public Sprite o;

	public void onDesert()
	{
        spriteRenderer.sprite = d;
	}

	public void onOcean()
	{
        spriteRenderer.sprite = o;
    }

	void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

}
