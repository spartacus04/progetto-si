using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

// Chiedo perdono per lo schifo di codice
public class Box : MonoBehaviour
{
	public bool isWaterMode = false;

	private BoxCollider2D cl;
	private List<CompositeCollider2D> waterColliders;
	private bool isMoving = false;
	private Transform spriteRenderer;

	public void Start() {
		cl = GetComponent<BoxCollider2D>();
		waterColliders = Utils.FindGameObjectsWithLayer(LayerMask.NameToLayer("Water")).Select(go => go.GetComponent<CompositeCollider2D>()).ToList();

		spriteRenderer = transform.GetChild(0);
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.layer == 4) {
			Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
			isWaterMode = true;

			var pushable = GetComponent<Pushable>();

			Utils.setTimeout(() => {
				Destroy(GetComponent<Pushable>());
				StartCoroutine(InterpLocation(spriteRenderer, spriteRenderer.position, spriteRenderer.position + new Vector3(0, -0.5f, 0), 1f/5f));
			}, 1 / pushable.speed);

			cl.size = new Vector2(1f, 1f);

			cl.isTrigger = true;

		}
		else if(other.gameObject.CompareTag("Player") && isWaterMode) {
			cl.isTrigger = true;

			OnTriggerStay2D(other.collider);
		}
	}

	private void OnTriggerStay2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			handlePlayer(other);
			return;
		}
	}

	private void handlePlayer(Collider2D other) {
		if(other.GetComponent<PlayerMovement>().isLocked || isMoving) return;

		// Player isn't riding box
		var playerPos = other.transform.position;

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

		var data = closerIndex switch {
			0 => KeyCode.W,
			1 => KeyCode.S,
			2 => KeyCode.D,
			3 => KeyCode.A,
			_ => KeyCode.Space
		};

		if(KeyCode.Space == data) return;

		if(Input.GetKey(data)) {
			// TODO: animation jump on box
			cl.isTrigger = false;
			isMoving = true;
			// disable player collider and movement
			other.GetComponent<PlayerMovement>().isLocked = true;
			other.GetComponent<Collider2D>().enabled = false;
			setGameobjectAsChildren(other.gameObject);

			// Replace 10 with animation length
			StartCoroutine(InterpLocation(other.transform, other.transform.position, transform.position, 1f/3f));

			Utils.setTimeout(() => {
				isMoving = false;
			}, 1f/3f);
		}
	}

	private void Update() {
		var other = GameObject.FindGameObjectWithTag("Player");

		// check if one of the children has tag player and get it
		if(other.transform.parent != transform) return;
		if(!other.GetComponent<PlayerMovement>().isLocked) return;
		if(isMoving) return;


		if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) return;
			// player is riding box
			var PlayerMovement = other.GetComponent<PlayerMovement>();

			var targetPos = Utils.roundToNearestHalf(PlayerMovement.interactLocation.position * 1.2f);
			Debug.Log(targetPos);

			var elements = Physics2D.OverlapCircleAll(targetPos, 0.4f);

			if(elements.Length > 1) return;

			unsetGameobjectAsChildren(other.gameObject);

			StartCoroutine(InterpLocation(other.transform, transform.position, targetPos, 1f/3f));

			Utils.setTimeout(() => {
				cl.isTrigger = true;

				other.GetComponent<PlayerMovement>().isLocked = false;
				other.GetComponent<Collider2D>().enabled = true;
			}, 1f/3f);
	}

	IEnumerator InterpLocation(Transform ts, Vector2 start, Vector2 target, float duration) {
		for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            Vector2 lerped = Vector2.Lerp(start, target, t / duration);
            ts.position = new Vector2(lerped.x, lerped.y);
            yield return 0;
        }


		Debug.Log(target);
		ts.position = target;
	}

	private void setGameobjectAsChildren(GameObject child) {
		child.transform.parent = transform;
	}

	private void unsetGameobjectAsChildren(GameObject child) {
		child.transform.parent = null;
	}
}