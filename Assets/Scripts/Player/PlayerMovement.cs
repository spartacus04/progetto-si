using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
	public bool canMove = true;
    public float speed = 10;
    public new GameObject camera;

    [HideInInspector]
    private Vector2 movement;
    [HideInInspector]
    public Rigidbody2D rb;
    private static PlayerMovement instance;
	Vector2 lastPosition;
	
	public Transform interactLocation;

	[HideInInspector]
	public Vector2 facingDirection = Vector2.zero;
	public float interactDist = 0.9f;

	public void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(gameObject);
    }

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
