using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bomb : MonoBehaviour
{
	[SerializeField] 
	private float radius;

	[SerializeField]
	private float time;
	
	public bool canExplode = true;

	private void Start()
	{
		setOff();		
	}

	public void setOff()
	{
		if(!canExplode) return;
		canExplode = false;

		Utils.setTimeout(() => {
			explode();
		}, time);
	}

	void explode()
	{
		Collider2D[] oggetti = Physics2D.OverlapCircleAll(this.transform.position, radius);

		// Eseguo onExplode solo su i componenti che implementano l'interfaccia Bombable
		oggetti.ToList().ForEach(x => x.GetComponent<Bombable>()?.onExplode());

		Destroy(this.gameObject);
	}

	// Visualizza il raggio dell'esplosione quando selezionato
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(this.transform.position, radius);
	}
}
