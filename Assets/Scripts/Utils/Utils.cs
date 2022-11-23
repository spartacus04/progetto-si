using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

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

	public static List<GameObject> FindGameObjectsWithLayer(int layer){
     	var arr = FindObjectsOfType<GameObject>();

     	// Filter objects with layer

		return arr.Where(
	 		go => go.layer == layer
	 	).ToList();
	}
}
