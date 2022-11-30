using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndObj : MonoBehaviour
{
    public RawImage img;

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.tag == "Player") {
            other.GetComponent<PlayerMovement>().canMove = false;
            other.GetComponent<Animator>().enabled = false;
            StartCoroutine(FadeIn(3f));
            StartCoroutine(FadeoutMusic(3f));
        }
    }

    IEnumerator FadeoutMusic(float timeout)
    {
        for (float t = 0.0f; t < timeout; t += Time.deltaTime) {
            float normalizedTime = t / timeout;
            AudioListener.volume = Mathf.Lerp(1, 0, normalizedTime);
            yield return null;
        }

        AudioListener.volume = 0;
    }

    IEnumerator FadeIn(float timeout)
    {
        for (float t = 0.0f; t < timeout; t += Time.deltaTime) {
            float normalizedTime = t / timeout;
            img.color = new Color(img.color.r, img.color.g, img.color.b, Mathf.Lerp(0, 1, normalizedTime));
            yield return null;
        }

        img.color = new Color(img.color.r, img.color.g, img.color.b, 255);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene("Thanks");
    }
}
