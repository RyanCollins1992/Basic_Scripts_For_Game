using UnityEngine;
using System.Collections;

public class Player : MonoBehaviour
{

    /// <summary>
    /// The player's movement speed
    /// </summary>
    [SerializeField]
    private float movementSpeed;

    /// <summary>
    /// Defines what ground is
    /// </summary>
    [SerializeField]
    private LayerMask whatIsGround;

    /// <summary>
    /// An array of the groundpoints 
    /// that are used for checking if the player is on the ground
    /// </summary>
    [SerializeField]
    private Transform[] groundPoints;

    /// <summary>
    /// Indicates if the player can move while he is in the air
    /// </summary>
    [SerializeField]
    private bool airControl;

    /// <summary>
    /// A reference to the player's animator
    /// </summary>
    private Animator myAnimator;

    /// <summary>
    /// The player's start position
    /// </summary>
    private Vector2 startPos;

    /// <summary>
    /// A reference to the player's rigidbody
    /// </summary>
    private Rigidbody2D myRigidbody;

    /// <summary>
    /// Indicates if the player should jump
    /// </summary>
    private bool jump;

    /// <summary>
    /// Indicates if the player should attack
    /// </summary>
    private bool attack;

    /// <summary>
    /// Indicates if the player should do a jump attack
    /// </summary>
    private bool jumpAttack;

    /// <summary>
    /// Indicates if the player should slide
    /// </summary>
    private bool slide;

    /// <summary>
    /// Indicates if the player is grounded
    /// </summary>
    private bool grounded;

    /// <summary>
    /// The radius of the contact points
    /// </summary>
    private float groundRadius = .2f;

    /// <summary>
    /// Indicates if the player is facing right
    /// </summary>
    private bool facingRight = true;

    private void Awake()
    {
        //Sets the references to the components
        myAnimator = GetComponent<Animator>();
        myRigidbody = GetComponent<Rigidbody2D>();
        startPos = transform.position;

    }

    // Update is called once per frame
    private void Update()
    {
        //Handles the player's input
        HandleInput();
    }

    private void FixedUpdate()
    {
        //Gets the horizontal input
        float horizontal = Input.GetAxis("Horizontal");

        //Checks if the player is grounded
        grounded = IsGrounded();

        //Flips the player in the correct direction
        Flip(horizontal);

        //Handles the player's movement
        HandleMovement(horizontal);

        //Handles the player's attacks
        HandleAttacks();

        //Handles the animator layers
        HandleLayers();

        //Resets all actions
        ResetActions();
    }

    /// <summary>
    /// Handles the player's movement
    /// </summary>
    /// <param name="horizontal">The horizontal input axis</param>
    private void HandleMovement(float horizontal)
    {

        
        if (myRigidbody.velocity.y < 0) //We need to land if the player is falling
        {
            myAnimator.SetBool("Land", true); //Trigers the landing animation
        }
        if (!myAnimator.GetBool("Slide") && grounded && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack") || airControl)  //chesk if we should move the player
        {
            myRigidbody.velocity = new Vector2(horizontal * movementSpeed, myRigidbody.velocity.y); //Moves the player
        }
        if (grounded && jump) //if we should jump
        {
            // Add a vertical force to the player.
            grounded = false;

            //Makes the player jump
            myRigidbody.AddForce(new Vector2(0f, 400));
        }
        if (grounded && slide && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide")) //If we need to slide
        {
            myAnimator.SetBool("Slide", true); //Triggers the slide animation
        }
        else if (!this.myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Slide")) //If we are sliding
        {
            myAnimator.SetBool("Slide", false); //Indicate that we are done sliding
        }

        //Keeps the speed in the animator up to date
        myAnimator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (transform.position.y <= -10) //If we fall down then respawn the player
        {
            transform.position = startPos;
            myRigidbody.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// Handles the player's attacks
    /// </summary>
    private void HandleAttacks()
    {
        //Checks if we need to attack
        if (attack && grounded && !this.myAnimator.GetCurrentAnimatorStateInfo(0).IsTag("Attack"))
        {
            myAnimator.SetTrigger("Attack"); //Makes the player attack

            myRigidbody.velocity = Vector2.zero; //Stops the player from moving while attacking
        }
        //Checks if we need to make a jump attack
        if (jumpAttack && !grounded && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack"))
        {
            myAnimator.SetBool("JumpAttack", true); //Makes the player do a jump attack
        }
        //Check if we need to stop doing the jump attack
        if (!jumpAttack && !this.myAnimator.GetCurrentAnimatorStateInfo(1).IsName("JumpAttack"))
        {   
            //Stop the player from doing the jump attack
            myAnimator.SetBool("JumpAttack", false);
        }
    }

    /// <summary>
    /// Checks if the player is on the ground
    /// </summary>
    /// <returns>true if the player is on the ground</returns>
    private bool IsGrounded()
    {
        //Cheks if we are falling or if the y axis is steady
        if (myRigidbody.velocity.y <= 0)
        {
            //Runs through all the ground points
            foreach (Transform point in groundPoints)
            {
                //Makes an array of all colliders that are overlapping the ground points
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);

                //Runs through the colliders to check if we are overlapping something that isn't the player
                for (int i = 0; i < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject) //If we are overlapping something else than our self
                    {
                        myAnimator.SetBool("Land", false); //Stops the land animation
                        return true;
                    }
                }
            }
        }


        return false; //We are not grounded
    }

    private void Flip(float horizontal)
    {
        //If the input is moving the player right and the player is facing left...
        if (horizontal > 0 && !facingRight || horizontal < 0 && facingRight)
        {
            // Switch the way the player is labelled as facing.
            facingRight = !facingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

    /// <summary>
    /// Handles the animation layers
    /// </summary>
    private void HandleLayers()
    {

        //If we are in the air
        if (!IsGrounded())
        {   
            //Activate the air layer
            myAnimator.SetLayerWeight(1, 1);
        }
        else //If we are on the ground
        {
            //Activates the ground layer
            myAnimator.SetLayerWeight(1, 0);
        }
    }

    /// <summary>
    /// Resets all actions
    /// </summary>
    private void ResetActions()
    {
        jump = false;
        jumpAttack = false;
        attack = false;
        slide = false;
    }

    /// <summary>
    /// Handles the players input
    /// </summary>
    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            attack = true;
            jumpAttack = true;

        }
        if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            slide = true;
        }
    }
}
