using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Controller : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float DashSpeed = 10f;

    public Vector3 inputVector;

    public Rigidbody rb;

    public GameObject VisualBox;

    //Dash
    public float DashCount = 0f;
    public float DashCountDefault;
    public float DashTime = .2f;
    public float DashTimeDefault = .2f;
    public bool canDash = true;
    public bool Dashing = false;
    public Vector3 DashVector;
    //
    //Jump
    public bool canJump = true;
    public bool Jumping = false;
    public float JumpSpeed;
    public Vector3 MaxJumpSpeed;
    public Vector3 MinJumpSpeed;
    public float jumpTime;
    public float jumpTimeDefault;


    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        DashTime = DashTimeDefault;
        jumpTime = jumpTimeDefault;
    }

    public float mouseX;
    public float mouseY;

    private void Update()
    {
        //Dash
        if(DashCount <= 0f)
        {
            canDash = true;
            DashCount = 0f;
        }
        else
        {
            canDash = false;
        }

        if (canDash)
        {
            if (Input.GetKeyDown(KeyCode.RightShift))
            {
                DashVector = inputVector;
                Dashing = true;
                DashCount = DashCountDefault;
            }
            else
            {

            }
        }
        else
        {
            DashCount -= Time.deltaTime;
        }
        if (Dashing)
        {
            DashTime -= Time.deltaTime;
        }
        if (DashTime <= 0)
        {
            DashTime = DashTimeDefault;
            Dashing = false;
        }
        else
        {

        }
        //

        //Jump
        if (Input.GetKeyDown(KeyCode.Slash) && canJump)
        {
            Jumping = true;
            canJump = false;
        }

        if (Jumping)
        {
            jumpTime -= Time.deltaTime;
        }

        if(jumpTime <= 0)
        {
            jumpTime = jumpTimeDefault;
            Jumping = false;
        }

        //View
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(0, mouseX, 0);
        Camera.main.transform.Rotate(-mouseY, 0, 0);



        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        inputVector = transform.forward * vertical;
        inputVector += transform.right * horizontal;
        //
    }

    void FixedUpdate()
    {
        rb.velocity = inputVector * moveSpeed + Physics.gravity * .3f;
        Dash();
        Jump();
    }

    //DashVoid
    public void Dash()
    {
        if (Dashing)
        {
            rb.velocity = DashVector * DashSpeed;
        }
    }
    //
    //JumpVoid
    public void Jump()
    {
        if (Jumping)
        {
            rb.AddForce(Vector3.up * JumpSpeed, ForceMode.Impulse);
        }

    }
}
