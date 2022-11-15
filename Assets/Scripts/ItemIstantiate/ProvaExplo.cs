using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProvaExplo : MonoBehaviour,IBombable
{
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }
    
     public void canExplode()
	{
        Destroy(this.gameObject);
	}
}
