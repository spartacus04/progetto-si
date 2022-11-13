using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    private Collider2D col;

    private void Start() {
        col = GetComponent<Collider2D>();
    }

    public void Open() {
        Debug.Log("Opening door");
        col.enabled = false;
        //TODO: anim
    }

    public void Close() {
        Debug.Log("Closing door");
        col.enabled = true;
        //TODO: anim
    }
}
