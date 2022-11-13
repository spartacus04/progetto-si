using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransitionManager : MonoBehaviour
{
    public Animator[] animators;

    public void Start() {
        animators = GetComponentsInChildren<Animator>();
    }

    public void onTransition(float speed) {
        foreach(var animator in animators) {
            animator.SetFloat("speed", speed);
            animator.SetTrigger("transition");
            animator.enabled = true;
        }
    }
}
