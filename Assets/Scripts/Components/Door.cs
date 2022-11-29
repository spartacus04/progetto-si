using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider2D col;
    private Animator anim;

    private void Start() {
        col = GetComponent<Collider2D>();
        anim = GetComponent<Animator>();
    }

    public void Open() {
        col.enabled = false;
        anim.SetFloat("speed", 1);
        anim.SetTrigger("open");
    }

    public void Close() {
        col.enabled = true;
        anim.SetFloat("speed", -1);
        anim.SetTrigger("open");
    }
}
