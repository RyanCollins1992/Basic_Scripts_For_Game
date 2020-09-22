using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour {

	private Rigidbody2D rb;
	private Animator an;

	public float movementSpeed;

    [SerializeField]
    public float jumpForce;



	private bool facingRight;
	private bool attack;

    [SerializeField]
	private Transform[] groundPoints;

    [SerializeField]
    private float groundRadius;

	private LayerMask whatIsGround;

    [SerializeField]
    private bool isGrounded;

    private bool jump;

	// Use this for initialization
	void Start () {
	
		rb = GetComponent<Rigidbody2D> ();
		an = GetComponent<Animator> ();

		facingRight = true;
	}

	void update()
	{
		HandleInput();
	}
		

	void FixedUpdate(){

		float horizontal = Input.GetAxis ("Horizontal");


		isGrounded = IsGrounded ();

		HandleMovement(horizontal);
		Flip(horizontal);
		HandleAttacks ();
		ResetValues ();
        
	}

	private void HandleMovement(float horizontal)
	{

        if(isGrounded && jump)
        {
            isGrounded = false;
            rb.AddForce(new Vector2(0, jumpForce));
        }

        if (!this.an.GetCurrentAnimatorStateInfo (0).IsTag ("attack")) 
		{
			rb.velocity = new Vector2 (horizontal * movementSpeed, rb.velocity.y);
		}


		an.SetFloat ("speed", Mathf.Abs(horizontal));
	}


	// Character facing in correct direction
	private void Flip(float horizontal)
	{
		if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight) 
		{
			facingRight = !facingRight;

			Vector3 theScale = transform.localScale;

			theScale.x *= -1;

			transform.localScale = theScale;
		}
	}

	private void HandleAttacks()
	{
		if (attack && (!this.an.GetCurrentAnimatorStateInfo (0).IsTag ("Attack"))) 
		{
		
			an.SetTrigger ("attack");
			rb.velocity = Vector2.zero;

		}
	}

	private void HandleInput()
	{
        if(Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }

        if (Input.GetKeyDown(KeyCode.R))
			{
				attack = true;
			}
	}

	private void ResetValues()

	{
	
		attack = false;
        jump = false;
	}


	private bool IsGrounded()
	{
		if (rb.velocity.y <= 0) 
		{
			foreach (Transform point in groundPoints) 
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll (point.position, groundRadius, whatIsGround);
				for (int i = 0; i < colliders.Length; i++) {
				
					if (colliders [i].gameObject != gameObject)
                    {

						return true;
					}

				}
			}

		}
		return false;

	}
	
}