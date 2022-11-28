using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class StartCinematic : MonoBehaviour
{
	private VideoPlayer player;

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
        if(Input.anyKey) {
			Destroy(gameObject);
		}
    }
}
