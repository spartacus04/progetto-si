using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombAbility : MonoBehaviour
{

	[SerializeField] GameObject bomb;
    [SerializeField] GameObject spawnBomb;
	public bool CanDropBomb;

	public float bombTimer = 0.5f;
	public float bombTimerReset = 0.5f;
	public float bombRadius = 1f;

	private float lastTimer;

	[Header("Bomb Quantity")]
	[SerializeField] int maxQuantity;
	private int quantityBomb=0;
	private void Start()
	{
		lastTimer = Time.time;
	}


	
	private void Update()
	{
		var controllo = Physics2D.OverlapCircleAll(spawnBomb.transform.position, 0.2f);
		print(controllo.Length);
		
		if (lastTimer + bombTimer + bombTimerReset + Time.deltaTime < Time.time)
		{
			CanDropBomb = true;
		}
		

		if (CanDropBomb && Input.GetKeyDown(KeyCode.Q) && maxQuantity > quantityBomb && controllo.Length <=1)
		{

			CanDropBomb = false;
			lastTimer = Time.time;

			var bombI = Instantiate(bomb, spawnBomb.transform.position, Quaternion.identity);
			quantityBomb++;
			bombI.GetComponent<Bomb>().setup(bombRadius, bombTimer);
		}
	}
}
