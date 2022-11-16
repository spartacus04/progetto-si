using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils : MonoBehaviour
{
    public static Utils instance;

    void Awake() {
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
        }
    }

	// impossibile vivere senza setTimeout
    public static void setTimeout(Action action, float time)
    {
        instance.StartCoroutine(WaitAndExecute(time, action));
    }

    static IEnumerator WaitAndExecute(float time, Action action)
    {
        yield return new WaitForSeconds(time);
        action();
    }
}
