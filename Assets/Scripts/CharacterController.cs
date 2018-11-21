using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour {
    [SerializeField]
    private float maxSpeed = 10f;
    [SerializeField]
    private Rigidbody2D rigidbody2DInstance;
    [SerializeField]
    private float accelerationForce = 5;

    private float horizontalInput;
    private bool isFacingRight;

    Animator anim;
	// Use this for initialization
	void Start () {

        anim = GetComponent<Animator>();


	}
    void Update()
    {
        UpdateHorizontalInput();


    }
	// Update is called once per frame
	void FixedUpdate () {

        Move();

	}
    private void UpdateHorizontalInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        anim.SetFloat("Speed", Mathf.Abs(horizontalInput));
    }
    private void Move()
    {
        rigidbody2DInstance.AddForce(Vector2.right * horizontalInput * accelerationForce);
        Vector2 clampedVelocity = rigidbody2DInstance.velocity;
        clampedVelocity.x = Mathf.Clamp(rigidbody2DInstance.velocity.x, -maxSpeed, maxSpeed);
        rigidbody2DInstance.velocity = clampedVelocity;
        if (horizontalInput < 0 && !isFacingRight)
            Flip();
        else if (horizontalInput > 0 && isFacingRight)
            Flip();
    }
    void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }
}
