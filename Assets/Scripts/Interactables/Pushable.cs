using UnityEngine;
using System.Linq;

public class Pushable : Interactable, ThemeHandler
{
	public float speed = 1f;
	public bool isDesert = true;
	private Rigidbody2D rb;
	private Vector2 direction = Vector2.zero;

	public override void Start() {
		base.Start();

		rb = GetComponent<Rigidbody2D>();
	}

    public override void Update()
    {
		base.Update();

		if(isDesert) {
			rb.MovePosition((Vector2)transform.position + direction);
			direction = Vector2.zero;
			canInteract = true;
		}
		else {
			rb.velocity = direction * Time.deltaTime * speed * 200;
		}
    }


	void OnCollisionEnter2D(Collision2D other) {
		if(!isDesert && !other.gameObject.CompareTag("Player")) {
			transform.position = new Vector2(Mathf.Round(transform.position.x * 2) / 2, Mathf.Round(transform.position.y * 2) / 2);
			direction = Vector2.zero;
			canInteract = true;
		}
	}

	public override void onInteract()
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

    public void onOcean()
    {
		isDesert = false;
    }

    public void onDesert()
    {
		isDesert = true;
    }
}
