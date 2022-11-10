using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
	[HideInInspector]
	public GameObject player;
	public float interactRadius = 1f;
	public bool canInteract = true;

	public virtual void Start() {
		player = GameObject.FindGameObjectWithTag("Player");
	}

    public virtual void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) &&
			Vector2.Distance(transform.position, player.transform.position) < interactRadius &&
			canInteract) {
			onInteract();
		}
    }

	public virtual void OnCollisionEnter2D(Collision2D other) {}

	public abstract void onInteract();
}
