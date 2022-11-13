using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using System.Linq;

public class RoomBehaviour : MonoBehaviour
{
	private GameObject RoomState;
	private bool playerInRoom;
	public TransitionManager fadeInAnim;
	public Transform playerStart;
	public bool defaultStyleIsDesert = true;

	void Start() {
		RoomState = Instantiate(gameObject);
		RoomState.SetActive(false);
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			playerInRoom = true;

			// Chiedo scusa per questo schifo
			var player = other.gameObject;

			var confiner = player.GetComponent<PlayerMovement>()
				.camera
				.GetComponent<CinemachineConfiner>();

			StartCoroutine(fadeIn(confiner));
		}
	}

	private void OnTriggerExit2D(Collider2D other) {
		if(other.CompareTag("Player")) {
			playerInRoom = false;
		}
	}

	IEnumerator fadeIn(CinemachineConfiner confiner) {
		yield return new WaitForSeconds(5f/6f);
		confiner.m_BoundingShape2D = GetComponent<PolygonCollider2D>();
	}

	private void Update() {
		if(Input.GetKeyUp(KeyCode.R) && playerInRoom) {
			// Brutto ma non ho voglia di tenere a mente il player
			var player = GameObject.FindGameObjectWithTag("Player");

			var mObjs = GameObject.FindObjectsOfType<MonoBehaviour>();
        	var handlers = (from a in mObjs where a.GetType().GetInterfaces().Any(k => k == typeof(ThemeHandler)) select (ThemeHandler)a).ToList();
			
			fadeInAnim.onTransition(1f);

			Utils.setTimeout(() => {
				// Chiedo scusa di nuovo per questo altro schifo
				player.GetComponent<PlayerMovement>().camera.GetComponent<CinemachineConfiner>().m_BoundingShape2D = RoomState.GetComponent<PolygonCollider2D>();

				player.transform.position = (Vector2)playerStart.position;

				if(!ThemeSwitcher.isDesert && defaultStyleIsDesert) {
					ThemeSwitcher.isDesert = true;
					handlers.ForEach(e => {
						e.onDesert();
					});
				} else if(ThemeSwitcher.isDesert && !defaultStyleIsDesert) {
					ThemeSwitcher.isDesert = false;
					handlers.ForEach(e => {
						e.onOcean();
					});
				}

				RoomState.SetActive(true);
				Destroy(gameObject);
			}, 1f/3f/1f);
		}
	} 
}
