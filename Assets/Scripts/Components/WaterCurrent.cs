using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCurrent : MonoBehaviour, ThemeHandler
{
    public Vector2 direction;
    private ParticleSystem spriteRenderer;

    public void Start() {
        spriteRenderer = GetComponent<ParticleSystem>();
        spriteRenderer.Stop();
        spriteRenderer.Clear();
    }

    public void onOcean()
    {
        spriteRenderer.Play();
    }

    public void onDesert()
    {
        spriteRenderer.Pause();
        spriteRenderer.Clear();
    }

    private void OnCollisionStay2D(Collision2D other) 
    {
        
    }
}
