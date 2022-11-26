using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Tilemaps;

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

    public static Vector3 roundToNearestHalf(Vector3 v) {
        // Round to nearest 0.5 (0.5, 1.5, 2.5, etc)
        return new Vector3(
            Mathf.Round(v.x * 2) / 2,
            Mathf.Round(v.y * 2) / 2,
            Mathf.Round(v.z * 2) / 2
        );
    }
}
