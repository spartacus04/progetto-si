using UnityEngine;
using System.Linq;

public class Interact : MonoBehaviour {
	public float interactRadius = 1f;
	public bool canInteract = true;

	Transform interactLoc;

	void Start() {
		interactLoc = GetComponent<PlayerMovement>().interactLocation;
	}

	void Update() {
		if(!Input.GetKeyDown(KeyCode.Space) || !canInteract) return;

		var colliders = Physics2D.OverlapCircleAll(interactLoc.position, interactRadius);

		colliders.ToList().ForEach(collider => {
			collider.GetComponent<Interactable>()?.onInteract(gameObject);
		});
	}
}