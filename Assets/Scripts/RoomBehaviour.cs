using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomBehaviour : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			// Chiedo scusa per questo schifo
			var player = other.gameObject;

			var confiner = player.GetComponent<PlayerMovement>()
				.camera
				.GetComponent<CinemachineConfiner>();
			

			StartCoroutine(fadeIn(confiner));

		}
	}

	IEnumerator fadeIn(CinemachineConfiner confiner) {
		yield return new WaitForSeconds(5f/6f);
		confiner.m_BoundingShape2D = GetComponent<PolygonCollider2D>();
	} 
}
