using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour
{
    public Sprite toggledSprite;
    private Sprite offSprite;
    private SpriteRenderer sr;
    public UnityEvent onButtonExit;
    public UnityEvent onButtonEnter;
    private List<GameObject> colliding = new List<GameObject>();

    private void Start() {
        sr = GetComponent<SpriteRenderer>();
        offSprite = sr.sprite;
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(!other.CompareTag("Player") && !other.CompareTag("Object")) return;

        if(colliding.Count == 0) {
            sr.sprite = toggledSprite;
            onButtonEnter.Invoke();
        }

        colliding.Add(other.gameObject);
    }

    private void OnTriggerExit2D(Collider2D other) {
        if(colliding.Contains(other.gameObject)) {
            colliding.Remove(other.gameObject);
        }

        if(colliding.Count == 0) {
            sr.sprite = offSprite;
            onButtonExit.Invoke();
        }
    }
}
