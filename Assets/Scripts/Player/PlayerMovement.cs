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
    }

    void Update()
    {
        if (!canMove)
        {
            movement = Vector2.zero;
            return;
        }
        
        movement = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }

    void FixedUpdate()
    {
        rb.velocity = movement * speed * 200 * Time.fixedDeltaTime;
    }
}
