using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
	[SerializeField] 
	private float radius;
	[SerializeField]
	private LayerMask CanExplodeLayers;

	[SerializeField]
	private float time;
	
	bool canExplose;

	private void Start()
	{
		canExplose = true;

		StartCoroutine(Explosion(canExplose));
	}
	

	private IEnumerator Explosion(bool canExplose)
	{
		if (canExplose)
		{
			yield return !canExplose;
			yield return new WaitForSeconds(time);
			
			Destroy(this.gameObject);
		}

	}

	private void OnDestroy()
	{
		print("distrutto");
		explode();
	}
	void explode()
	{





		Collider2D[] Oggetti = Physics2D.OverlapCircleAll(transform.position, radius, CanExplodeLayers);
		
		foreach (Collider2D canexplodes in Oggetti)
		{
			
			canexplodes.GetComponent<IBombable>().canExplode();
			

		}

	}
	void OnDrawGizmosSelected()
	{

		// Visualizza il raggio dell'esplosione quando selezionato
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, radius);
	}


}



interface IBombable
{
	public void canExplode()
	{}


}
