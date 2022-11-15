using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAbility : MonoBehaviour
{
	[SerializeField] KeyCode InstantiateKey;
	[SerializeField] GameObject bomb;
	[SerializeField] GameObject Player;
	float lastTimer;

	[SerializeField] float timer;
	bool CanDropBomb;
	private void Start()
	{
		CanDropBomb = true;
		lastTimer = timer; 
	}
	private void bombistantiate()
	{
		if (!Input.GetKeyDown(InstantiateKey) && CanDropBomb)
		{
			return;
		}
		Instantiate(bomb, Player.transform.position, Quaternion.identity);
		timer = lastTimer;
		CanDropBomb = false;
	}

	private void Update()
	{
		if (timer > 0)
		{
			timer -= Time.deltaTime;
			print(timer);
		}
		else
		{
			CanDropBomb = true;
			bombistantiate();
		}
	}

}
