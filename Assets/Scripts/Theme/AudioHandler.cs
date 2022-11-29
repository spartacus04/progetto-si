using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioHandler : MonoBehaviour, ThemeHandler {
    public AudioClip desertTheme;
    public AudioClip oceanTheme;

    private AudioSource audioSource;

    private void Start() {
        audioSource = GetComponent<AudioSource>();
    }

    public void onOcean() {
        StartCoroutine(volumeFromTo(0.2f, 0, 1f/3f));

        Utils.setTimeout(() => {
            var seconds = audioSource.time;
            audioSource.clip = oceanTheme;
            audioSource.Play();
            audioSource.time = seconds;
        }, 1f/6f);
    }

    public void onDesert() {
        StartCoroutine(volumeFromTo(0.2f, 0, 1f/3f));
        
        Utils.setTimeout(() => {
            var seconds = audioSource.time;
            audioSource.clip = desertTheme;
            audioSource.Play();
            audioSource.time = seconds;
        }, 1f/6f);
    }

    IEnumerator volumeFromTo(float from, float to, float duration) {
        float time = 0;

        while (time < duration / 2f) {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(from, to, time / duration);
            yield return null;
        }

        audioSource.volume = to;

        time = 0;

        while (time < duration / 2f) {
            time += Time.deltaTime;
            audioSource.volume = Mathf.Lerp(to, from, time / duration);
            yield return null;
        }

        audioSource.volume = from;
    }
}