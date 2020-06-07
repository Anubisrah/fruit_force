using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControls : MonoBehaviour
{
    private Rigidbody2D playerRigidBody;
    public float movementSpeed;
    public float upNess;

    public Transform groundCheckPoint;
    public float groundCheckRadius;
    public LayerMask groundLayer;

    private bool isTouchingGround;
    private bool jumpPressed = false;

    public void Awake()
    {
        playerRigidBody = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        isTouchingGround = Physics2D.OverlapCircle(groundCheckPoint.position, groundCheckRadius, groundLayer);
        if (playerRigidBody != null)
        {
           ApplyInput();
        }
        else
        {
            Debug.LogWarning("Rigid body not attached to player " + gameObject.name);
        }
    }

    /// <summary>
    /// Check to see if any player keys are down, and if so... perform relevant actions
    /// </summary>
    public void ApplyInput()
    {
 
        // Calculate x movement.
        float xInput = Input.GetAxis("Horizontal");
        float xForce = xInput * movementSpeed * Time.deltaTime;
        
        bool jump = Input.GetKeyDown("space");
        float yForce;
        if (jump && isTouchingGround && jumpPressed == false)
        {
            Debug.Log("Jump!");
            yForce = upNess;
            jumpPressed = true;
        }
        else
        {
            yForce = 0;
        }

        if (Input.GetKeyUp("space"))
        {
            // Ensure the button has been released before allowing more jumping is allowed.
            jumpPressed = false;
        }


        Vector2 force = new Vector2(xForce, yForce);

        playerRigidBody.AddForce(force);

        //Debug.Log("xForce: " + xForce);
        //Debug.Log("Velocity: " + playerRigidBody.velocity.x + " " + playerRigidBody.velocity.y);
    }
}