using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float gravityMultiplier;
    public float wasdSpeed;
    public float jumpHeight;
    public float rotateSpeed;
    public bool canMoveInAir;
    public bool canDash;
    public float dashDistance;
    public float dashSpeed;

    float gravity;
    Rigidbody myRigidbody;
    bool isGrounded;
    bool doubleJump;
    float angleY;
    float lateralMove;
    float forwardMove;

    Vector3 forwardDir;
    bool dashing = false;
    Vector3 initialPos;

    // Start is called before the first frame update
    void Start()
    {
        Physics.gravity = new Vector3(0.0f, 9.81f * gravityMultiplier, 0.0f);
        myRigidbody = this.GetComponent<Rigidbody>();
        gravity = Physics.gravity.y;

        
    }

    void Update()
    {
        if (dashing)
        {
            return;
        }

        angleY = this.transform.rotation.eulerAngles.y;
        forwardDir = new Vector3(
            Mathf.Sin(angleY / 360 * 2 * Mathf.PI),
            0,
            Mathf.Cos(angleY / 360 * 2 * Mathf.PI)
        );

        

        if (Input.GetMouseButtonDown(0) && canDash)
        {
            dashing = true;
            initialPos = this.transform.position;
        }

        

        bool jump = Input.GetButtonDown("Jump");
        lateralMove = Input.GetAxis("Horizontal");
        forwardMove = Input.GetAxis("Vertical");

        if (jump && (isGrounded || doubleJump))
        {
            Jump();
            if (!isGrounded)
            {
                doubleJump = false;
            }
        }

        if (this.transform.position.y < -5)
        {
            FindObjectOfType<GameManager>().GameOver();
        }

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (dashing)
        {
            Dash();
            return;
        }

        if (canMoveInAir || isGrounded)
        {
            WasdMove(forwardMove, lateralMove);
        }

        

    }

    void WasdMove(float forward, float lateral)
    {
        myRigidbody.velocity = new Vector3(
            forward * wasdSpeed * Mathf.Sin(angleY / 360 * 2 * Mathf.PI)
            + lateral * wasdSpeed * Mathf.Cos(angleY / 360 * 2 * Mathf.PI),
            myRigidbody.velocity.y,
            forward * wasdSpeed * Mathf.Cos(angleY / 360 * 2 * Mathf.PI)
            - lateral * wasdSpeed * Mathf.Sin(angleY / 360 * 2 * Mathf.PI)
        );
    }

    void Jump()
    {
        
        float initialVelocity = Mathf.Sqrt(2 * -gravity * jumpHeight);

        myRigidbody.velocity = new Vector3(
            myRigidbody.velocity.x,
            initialVelocity,
            myRigidbody.velocity.z
        );
    }

    void Dash()
    {
        myRigidbody.velocity = forwardDir * dashSpeed;

        if (Vector3.Distance(initialPos, this.transform.position) >= dashDistance)
        {
            dashing = false;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    
    }
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;

            doubleJump = true;
        }
    }
}
