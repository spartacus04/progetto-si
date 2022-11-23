using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool canMove = true;
	public bool isLocked = true;
    public float speed = 10;
    public new GameObject camera;

    [HideInInspector]
    private Vector2 movement;
    [HideInInspector]
    public Rigidbody2D rb;
	Vector2 lastPosition;
	
	public Transform interactLocation;

	[HideInInspector]
	public Vector2 facingDirection = Vector2.zero;
	public float interactDist = 0.9f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		lastPosition = this.transform.position;
    }

	void direction()
	{
		if(movement.y > 0) {
			facingDirection = Vector2.up;
		} else if(movement.y < 0) {
			facingDirection = Vector2.down;
		} else if(movement.x > 0) {
			facingDirection = Vector2.right;
		} else if(movement.x < 0) {
			facingDirection = Vector2.left;
		}

		interactLocation.localPosition = (Vector3)facingDirection * interactDist;
	}

	void Update()
    {
        if (!canMove)
        {
            movement = Vector2.zero;
            return;
        }
        
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
		direction();
		if(isLocked) movement = Vector2.zero;
	}

    void FixedUpdate()
    {
        rb.velocity = movement * speed * 200 * Time.fixedDeltaTime;
    }

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(interactLocation.position, 0.4f);
	}
}
