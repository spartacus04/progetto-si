using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Video;

public class StartCinematic : MonoBehaviour
{
	private VideoPlayer player;
	public float timeout;
	public TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInChildren<VideoPlayer>();

		var autoSkip = player.clip.length;

		Utils.setTimeout(() => {
			Destroy(gameObject);
		}, (float)autoSkip);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.anyKeyDown) {
			if(text.color.a >= 0.3f)	 {
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
		text.text = "Skip";

		yield return new WaitForSeconds(0.33f);

		text.text = "Skip.";

		yield return new WaitForSeconds(0.33f);

		text.text = "Skip..";

		yield return new WaitForSeconds(0.33f);

		text.text = "Skip...";

		yield return new WaitForSeconds(0.33f);

		StartCoroutine(animateText());
	}
}
