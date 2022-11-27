using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;

// Chiedo perdono per lo schifo di codice
public class Box : MonoBehaviour
{
	public bool isWaterMode = false;
	public LayerMask IgnoreLayer;
	public GameObject player;

		public PlayerMovement pMovement;

	private BoxCollider2D cl;
	private List<CompositeCollider2D> waterColliders;
	private bool isMoving = false;
	private Transform spriteRenderer;

	public void Start() {
		cl = GetComponent<BoxCollider2D>();
		waterColliders = Utils.FindGameObjectsWithLayer(LayerMask.NameToLayer("Water")).Select(go => go.GetComponent<CompositeCollider2D>()).ToList();

		spriteRenderer = transform.GetChild(0);
		player = GameObject.FindGameObjectWithTag("Player");
		pMovement = player.GetComponent<PlayerMovement>();
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.layer == 4) {
			Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
			isWaterMode = true;

			var pushable = GetComponent<Pushable>();

			Utils.setTimeout(() => {
				Destroy(GetComponent<Pushable>());
				StartCoroutine(InterpLocation(spriteRenderer, spriteRenderer.position, spriteRenderer.position + new Vector3(0, -0.5f, 0), 1f/5f));

				cl.size = new Vector2(1f, 1f);
				cl.isTrigger = true;
			}, 1 / pushable.speed);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(!other.CompareTag("Player")) return;

		var pMovement = other.GetComponent<PlayerMovement>();

		if(pMovement.isLocked || isMoving) return;

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

		if(data == KeyCode.Space) return;

		if(Input.GetKey(data)) {
			// TODO: animation jump on box
			isMoving = true;

			pMovement.isLocked = true;
			other.GetComponent<Collider2D>().enabled = false;
			setGameobjectAsChildren(other.gameObject);

			// Replace 10 with animation length
			StartCoroutine(InterpLocation(other.transform, other.transform.position, transform.position, 1f/3f));
		}
	}

	void Update() {
		if(!Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.S)) return;
		if(isMoving) return;

		// check if one of the children has tag player and get it
		if(player.transform.parent != transform) return;
		if(!pMovement.isLocked) return;
		
		var PlayerMovement = player.GetComponent<PlayerMovement>();

		var targetPos = (Vector2)transform.position + pMovement.facingDirection;

		Collider2D[] elements = Physics2D.OverlapCircleAll(targetPos, 0.4f, IgnoreLayer);

		if(elements.Length == 0) {
			// il codice all'interno dell'if è eseguito comunque (WTF)

			StartCoroutine(InterpLocation(player.transform, transform.position, targetPos, 1f/3f));

			Utils.setTimeout(() => {
				unsetGameobjectAsChildren(player.gameObject);
				pMovement.isLocked = false;
				player.GetComponent<Collider2D>().enabled = true;
			}, 1f/3f);
		}
	}

	IEnumerator InterpLocation(Transform ts, Vector2 start, Vector2 target, float duration) {
		for (float t = 0f; t < duration; t += Time.deltaTime)
        {
            Vector2 lerped = Vector2.Lerp(start, target, t / duration);
            ts.position = new Vector2(lerped.x, lerped.y);
            yield return 0;
        }

		ts.position = target;
		isMoving = false;

		yield return 0;
	}

	private void setGameobjectAsChildren(GameObject child) {
		child.transform.parent = transform;
	}

	private void unsetGameobjectAsChildren(GameObject child) {
		child.transform.parent = null;
	}
}