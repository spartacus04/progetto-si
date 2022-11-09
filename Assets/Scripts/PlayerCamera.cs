using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
	public Animator animator;
    private PlayerMovement pm;

    private void Start() {
        pm = GetComponent<PlayerMovement>();
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("CameraWalls")) {
            StartCoroutine(anim(other));
        }
    }

    IEnumerator anim(Collider2D other) {
        pm.canMove = false;
    
        switch(other.gameObject.name) {
            case "Top":
                animator.SetTrigger("Coverup");
                yield return new WaitForSeconds(5f/6f);
                transform.position += new Vector3(0, 1, 0);
                break;
            case "Bottom":
                animator.SetTrigger("Coverdown");
                yield return new WaitForSeconds(5f/6f);
                transform.position += new Vector3(0, -1, 0);
                break;
            case "Left":
                animator.SetTrigger("Coverleft");
                yield return new WaitForSeconds(5f/6f);
                transform.position += new Vector3(1, 0, 0);
                break;
            case "Right":
                animator.SetTrigger("Coverright");
                yield return new WaitForSeconds(5f/6f);
                transform.position += new Vector3(-1, 0, 0);
                break;
        }

        pm.canMove = true;
        yield return null;
    }
}
