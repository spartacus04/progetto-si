using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pushable : MonoBehaviour, Interactable {
	public float interactRadius { get; set; } = 1f;
	public float speed = 1f;
	private Rigidbody2D rb;
	private Vector2 direction = Vector2.zero;
	private Vector2 oPos;

	[HideInInspector]
	public bool canPush = true;

	public void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

    public void Update()
    {
		// rb.velocity = direction * Time.deltaTime * speed * 200;
    }

	private void OnCollisionEnter2D(Collision2D other) {
		/*if(!other.gameObject.CompareTag("Player")) {
			transform.position = new Vector2(Mathf.Round(transform.position.x * 2) / 2, Mathf.Round(transform.position.y * 2) / 2);
			StopAllCoroutines();
			direction = Vector2.zero;
		}*/
	}

	public void onInteract(GameObject player)
	{
		if(!canPush) return;

		var playerPos = player.transform.position;

		var orientations = new Vector2[] {
			(Vector2)transform.position + Vector2.down,
			(Vector2)transform.position + Vector2.up,
			(Vector2)transform.position + Vector2.left,
			(Vector2)transform.position + Vector2.right,
		};

		var distances = orientations.Select(
			orientation => Vector2.Distance(orientation, playerPos)
		).ToArray();

		var closerDistance = distances.Min();
		var closerIndex = 0;

		// For bruttissimo
		for (int i = 0; i < 4; i++)
		{
			if(distances[i] == closerDistance) {
				closerIndex = i;
				break;
			}
		}

		oPos = transform.position;

		switch(closerIndex) {
			case 0:
				direction = Vector2.up;
				break;
			case 1:
				direction = Vector2.down;
				break;
			case 2: 
				direction = Vector2.right;
				break;
			case 3:
				direction = Vector2.left;
				break;
		}

		StartCoroutine(InterpLocation(transform.position, transform.position + (Vector3)direction, 1 / speed));
	}

	IEnumerator InterpLocation(Vector2 start, Vector2 target, float duration) {
		for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            Vector2 lerped = Vector2.Lerp(start, target, t / duration);
            transform.position = new Vector2(lerped.x, lerped.y);
            yield return 0;
        }

		transform.position = target;
		direction = Vector2.zero;
	}
}
