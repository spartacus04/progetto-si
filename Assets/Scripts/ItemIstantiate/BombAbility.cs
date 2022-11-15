using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAbility : MonoBehaviour
{
	[SerializeField] KeyCode InstantiateKey;
	[SerializeField] GameObject bomb;
	[SerializeField] GameObject Player;


	private void bombistantiate()
	{
		if (!Input.GetKeyDown(InstantiateKey))
		{
			return;
		}
		Instantiate(bomb, Player.transform.position, Quaternion.identity);
	}

	private void Update()
	{
		bombistantiate();
	}

}
