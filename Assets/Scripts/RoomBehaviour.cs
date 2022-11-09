using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class RoomBehaviour : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			// Chiedo scusa per questo schifo
			var confiner = other.GetComponent<PlayerMovement>()
				.camera
				.GetComponent<CinemachineConfiner>();

			

			confiner.m_BoundingShape2D = GetComponent<PolygonCollider2D>();
		}
	} 
}
