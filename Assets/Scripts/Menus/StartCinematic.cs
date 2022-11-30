using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class StartCinematic : MonoBehaviour
{
	private VideoPlayer player;
	public AudioSource audioSource;
	public float timeout;
	public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
		AudioListener.volume = 1;
        player = GetComponentInChildren<VideoPlayer>();

		var autoSkip = player.clip.length;

		Utils.setTimeout(() => {
			if(!audioSource.isPlaying) audioSource.Play();

			Destroy(gameObject);
		}, (float)autoSkip);

		Utils.setTimeout(() => {
			StopAllCoroutines();
			StartCoroutine(fadeText());
			StartCoroutine(animateText());
		}, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown) {
			if(text.color.a >= 0.3f) {
				audioSource.Play();
				Destroy(gameObject);
				return;
			}

			StopAllCoroutines();
			StartCoroutine(fadeText());
			StartCoroutine(animateText());
		}
    }

	IEnumerator fadeText() {
		text.color = new Color(text.color.r, text.color.g, text.color.b, 1);
		// fade text gradually to 0 in timeout seconds

		for (float t = 0.0f; t < timeout; t += Time.deltaTime) {
			float normalizedTime = t / timeout;
			text.color = new Color(text.color.r, text.color.g, text.color.b, Mathf.Lerp(1, 0, normalizedTime));
			yield return null;
		}

		text.color = new Color(text.color.r, text.color.g, text.color.b, 0);
	}

	IEnumerator animateText() {
		text.text = "Premi un tasto per saltare";

		yield return new WaitForSeconds(0.33f);

		text.text = "Premi un tasto per saltare.";

		yield return new WaitForSeconds(0.33f);

		text.text = "Premi un tasto per saltare..";

		yield return new WaitForSeconds(0.33f);

		text.text = "Premi un tasto per saltare...";

		yield return new WaitForSeconds(0.33f);

		StartCoroutine(animateText());
	}
}
