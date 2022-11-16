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
    [SerializeField]
	GameObject instanziaBomba;
    public bool facingRight;
	public bool facingLeft;
	public bool facingDown;
	public bool facingUp;
	[SerializeField]
	float distanceBomb;

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

	void direction(float distanceBomb)
	{

		facingRight = (movement.x > 0) ? true : false;
		facingLeft = (movement.x < 0) ? true : false;
		facingDown = (movement.y < 0) ? true : false;
	    facingUp = (movement.y > 0) ? true : false;

		if (facingRight)
		{
			instanziaBomba.transform.position = new Vector2(transform.position.x + distanceBomb, transform.position.y);
		}
		else if(facingLeft)
		{
			instanziaBomba.transform.position = new Vector2(transform.position.x - distanceBomb, transform.position.y);

		}else if (facingUp)
		{
			instanziaBomba.transform.position = new Vector2(transform.position.x, transform.position.y+distanceBomb);
		}
		else if (facingDown)
		{
			instanziaBomba.transform.position = new Vector2(transform.position.x, transform.position.y-distanceBomb);
		}

	


	}
	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(instanziaBomba.transform.position, 0.4f);
	}

	void Update()
    {
		
        if (!canMove)
        {
            movement = Vector2.zero;
            return;
        }
        
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")).normalized;
		direction(distanceBomb);
	}

    void FixedUpdate()
    {
        rb.velocity = movement * speed * 200 * Time.fixedDeltaTime;
    }
}
