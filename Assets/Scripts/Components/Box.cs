using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
	public bool isWaterMode = false;

	public void Start() {
		
	}

	private void OnCollisionEnter2D(Collision2D other) {
		if(other.gameObject.layer == 4) {
			Physics2D.IgnoreCollision(other.collider, GetComponent<Collider2D>());
			isWaterMode = true;

			var pushable = GetComponent<Pushable>();
			Utils.setTimeout(() => {
				Destroy(GetComponent<Pushable>());
			}, 1 / pushable.speed);
		}
	}
}
