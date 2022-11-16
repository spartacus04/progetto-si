using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAbility : MonoBehaviour
{
	[SerializeField] GameObject bomb;
	public bool CanDropBomb;

	public float bombTimer = 0.5f;
	public float bombTimerReset = 0.5f;

	public float bombRadius = 1f;

	private float lastTimer;

	private void Start()
	{
		lastTimer = Time.time;
	}

	private void Update()
	{
		if(lastTimer + bombTimer + bombTimerReset + Time.deltaTime < Time.time)
		{
			CanDropBomb = true;
		}

		if(CanDropBomb && Input.GetKeyDown(KeyCode.Q))
		{
			CanDropBomb = false;
			lastTimer = Time.time;

			var bombI = Instantiate(bomb, transform.position, Quaternion.identity);
			bombI.GetComponent<Bomb>().setup(bombRadius, bombTimer);
		}
	}
}
