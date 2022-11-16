using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Bomb : MonoBehaviour
{
	private float radius;

	private float time;
	
	public void setup(float radius, float time) {
		this.radius = radius;
		this.time = time;
		setOff();
	}

	public void setOff()
	{
		Debug.Log(time);
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
