using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public UnityEvent onButtonExit;
    public UnityEvent onButtonEnter;

    private void OnTriggerEnter2D(Collider2D other) {
        onButtonEnter.Invoke();
        //TODO: animazione
    }

    private void OnTriggerExit2D(Collider2D other) {
        onButtonExit.Invoke();
        //TODO: animazione
    }
}
