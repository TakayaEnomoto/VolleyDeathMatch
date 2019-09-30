using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class P1Controller : MonoBehaviour
{
    public float moveSpeed = 1f;

    public Vector3 inputVector;

    public Rigidbody rb;

    public GameObject VisualBox;

    public Vector3 TempSpeed;

    //Dash
    [Header("Dash")]
    public float MaxDashSpeed = 10f;
    public float MinDashSpeed = 0f;
    public float DashCount = 0f;
    public float DashCountDefault;
    public float DashTime = .2f;
    public float DashTimeDefault = .2f;
    public float DashingTime = 0f;
    public float DashLerp = 1f;
    public bool canDash = true;
    public bool Dashing = false;
    public Vector3 DashVector;
    //
    //Jump
    [Header("Jump")]
    public float LerpTime = 2f;
    public bool canJump = true;
    public bool Jumping = false;
    public float MaxJumpSpeed = 10f;
    public float MinJumpSpeed = 0f;
    public float jumpTimeInSky = 0.0f;
    public float jumpTime;
    public float jumpTimeDefault;
    //
    //Fall
    [Header("Fall")]
    public bool Falling = false;
    public float MinFallSpeed = 0f;
    public float MaxFallSpeed = 10f;
    public float FallTimeInSky = 0.0f;

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
            if(DashVector == Vector3.zero)
            {
                
            }
            else
            {
                canDash = false;
            }
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
            DashingTime = 0f;
        }
        if(DashingTime >= 1f)
        {
            DashingTime = 1f;
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

        if(jumpTime <= 0 || jumpTimeInSky >= 1f)
        {
            jumpTime = jumpTimeDefault;
            Jumping = false;
            Falling = true;
            jumpTimeInSky = 0f;
        }
        Jump();
        //Reset FallTimeInSky
        if(FallTimeInSky >= 1f)
        {
            FallTimeInSky = 1f;
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
        inputVector.Normalize();
        //
    }

    private void OnCollisionEnter(Collision collision)
    {
        //StopFall && Reset canJump
        if (collision.gameObject.tag == "Floor")
        {
            Falling = false;
            FallTimeInSky = 0f;
            canJump = true;
        }
        //
    }

    void FixedUpdate()
    {
        TempSpeed = inputVector * moveSpeed;
        Dash();
        rb.velocity = new Vector3(TempSpeed.x, rb.velocity.y, TempSpeed.z);
        //Jump();
        //Fall();
    }

    //DashVoid
    public void Dash()
    {
        if (Dashing)
        { 
            TempSpeed += (DashVector * (Mathf.Lerp(MaxDashSpeed, MinDashSpeed, DashingTime)));
            DashingTime += DashLerp * Time.fixedDeltaTime;
        }
    }
    //
    //JumpVoid
    public void Jump()
    {
        //if (Jumping)
        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            rb.velocity += Vector3.up * MaxJumpSpeed;//Mathf.Lerp(MaxJumpSpeed, MinJumpSpeed, jumpTimeInSky);
            jumpTimeInSky += LerpTime * Time.fixedDeltaTime;
        }

    }
    //
    //FallVoid
    public void Fall()
    {
        if (Falling)
        {
            rb.velocity += Vector3.down * Mathf.Lerp(MinFallSpeed, MaxFallSpeed, FallTimeInSky);
            FallTimeInSky += LerpTime * Time.fixedDeltaTime;
        }
    }
}
