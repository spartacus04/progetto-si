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
	private Animator playerAnimator;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
		playerAnimator = GetComponent<Animator>();
		lastPosition = this.transform.position;
    }

	void direction()
	{
		playerAnimator.SetFloat("Speed", 0f);

		if (movement.y > 0) {
			facingDirection = Vector2.up;
			playerAnimator.SetTrigger("Top");
			playerAnimator.SetFloat("Speed", 1f);
		} else if(movement.y < 0) {
			facingDirection = Vector2.down;
			playerAnimator.SetTrigger("Down");
			playerAnimator.SetFloat("Speed", 1f);

		}
		else if(movement.x > 0) {
			facingDirection = Vector2.right;
			playerAnimator.SetTrigger("MoveRight");
			playerAnimator.SetFloat("Speed", 1f);

		}
		else if(movement.x < 0) {
			facingDirection = Vector2.left;
			playerAnimator.SetTrigger("MoveLeft");
			playerAnimator.SetFloat("Speed", 1f);


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
