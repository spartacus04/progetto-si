using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterCurrent : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) {
        if(other.gameObject.CompareTag("Box")) {
            other.gameObject.GetComponent<Rigidbody2D>().AddForce(transform.up * 100);
        }
    }
}
