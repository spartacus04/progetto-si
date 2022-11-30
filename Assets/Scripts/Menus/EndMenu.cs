using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{
    public RawImage endImage;
    public TextMeshProUGUI endText;
    public TextMeshProUGUI endScore;

    void Start() {
        Utils.setTimeout(() => {
            StartCoroutine(FadeOut(1f));
        }, 5f);
    }
    
    IEnumerator FadeOut(float timeout)
    {
		for (float t = 0.0f; t < timeout; t += Time.deltaTime) {
			float normalizedTime = t / timeout;
			endText.color = new Color(endText.color.r, endText.color.g, endText.color.b, Mathf.Lerp(1, 0, normalizedTime));
            endScore.color = endText.color;
            endImage.color = endText.color;
			yield return null;
		}

		endText.color = new Color(endText.color.r, endText.color.g, endText.color.b, 0);
        endScore.color = endText.color;
        endImage.color = endText.color;


        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Menu");
    }
}
