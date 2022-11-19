using UnityEngine;
using System.Linq;
using System.Collections.Generic;

public class Pushable : MonoBehaviour, Interactable
{
	public float interactRadius { get; set; } = 1f;
	public bool canInteract {get; set; } = true;
	public float speed = 1f;
	private Rigidbody2D rb;
	private Vector2 direction = Vector2.zero;
	private Vector2 oPos;

	public void Start() {
		rb = GetComponent<Rigidbody2D>();
	}

    public void Update()
    {
		rb.velocity = direction * Time.deltaTime * speed * 200;

		if(canInteract) {
			transform.position = new Vector2(Mathf.Round(transform.position.x + 0.5f) - 0.5f, Mathf.Round(transform.position.y + 0.5f) - 0.5f);
		}

		if(Vector2.Distance(oPos, transform.position) > 1 && !canInteract) {
			direction = Vector2.zero;
			canInteract = true;
		}
    }


	void OnCollisionEnter2D(Collision2D other) {
		if(!other.gameObject.CompareTag("Player")) {
			transform.position = new Vector2(Mathf.Round(transform.position.x + 0.5f) - 0.5f, Mathf.Round(transform.position.y + 0.5f) - 0.5f);
			direction = Vector2.zero;
			canInteract = true;
		}
	}

	public void onInteract(GameObject player)
	{
		canInteract = false;

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
	}
}
