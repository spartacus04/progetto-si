using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warp : MonoBehaviour
{
    public RoomBehaviour room;
	public AudioSource src;
	public AudioClip clip;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Player")
		{
			room.reset();

			StartCoroutine(fadeOutAudio(1f/3f/1f));
		}
	}

	IEnumerator fadeOutAudio(float time) {
		float startVolume = src.volume;
		while (src.volume > 0) {
			src.volume -= startVolume * Time.deltaTime / time;
			yield return null;
		}
		src.Stop();
		Destroy(src.GetComponent<AudioHandler>());

		src.volume = startVolume;
		src.clip = clip;
		src.Play();
	}
}
