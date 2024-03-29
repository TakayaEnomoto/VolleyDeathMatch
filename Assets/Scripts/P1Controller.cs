﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class P1Controller : MonoBehaviour
{
    public static P1Controller player;

    public int P1Lives = 3;
    public float moveSpeed = 1f;

    public Vector3 inputVector;

    public Rigidbody rb;

    public GameObject VisualBox;

    public Vector3 TempSpeed;

    public float gravity;

    public float mouseX;
    public float mouseY;

    public Text Life;
    //Sounds
    [Header("Sounds")]
    public AudioSource dash;
    public AudioSource loseHP;
    public AudioSource jump;
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
    public bool canJump = true;
    public float MaxJumpSpeed = 10f;
    public GameObject footcanJump;
    public float jumpRayDistance;
    //
    //HitBall
    [Header("Hit")]
    public float maxRayDistance;
    public float upForce;
    public float forwardForce;
    public Vector3 punchDirection;
    public float downwardDirection;

    public void Awake()
    {
        player = this;
    }

    public void Start()
    {
        Cursor.visible = false;//Hide the cursor
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        DashTime = DashTimeDefault;
    }

    private void Update()
    {
        Life.text = "Your lives: " + P1Lives;
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
                //dash.Play();
                canDash = false;
            }
        }

        if (canDash)
        {
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                if(dash.isPlaying == false)
                    dash.Play();
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
        //Detect canJump
        int floorLayer = 1 << 9;
        Ray jumpRay = new Ray(footcanJump.transform.position, -footcanJump.transform.up);
        Debug.DrawRay(jumpRay.origin, jumpRay.direction * jumpRayDistance, Color.red);
        if (Physics.Raycast(jumpRay, jumpRayDistance, floorLayer))
        {
            canJump = true;
        }
        else
        {
            canJump = false;
        }
        //
        Jump();
        
        //View
        mouseX = Input.GetAxis("Mouse X");
        mouseY = Input.GetAxis("Mouse Y");

        transform.Rotate(0, mouseX, 0);
        Camera.main.transform.Rotate(-mouseY, 0, 0);



        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        /*
        Vector3 WalkVector = Vector2.zero;
        bool haveInput = false;
        if (Input.GetKey(KeyCode.W))
        {
            WalkVector.z += 1;
            haveInput = true;
        }
        if (Input.GetKey(KeyCode.S))
        {
            WalkVector.z -= 1;
            haveInput = true;
        }
        if (Input.GetKey(KeyCode.A))
        {
            WalkVector.x -= 1;
            haveInput = true;
        }
        if (Input.GetKey(KeyCode.D))
        {
            WalkVector.x += 1;
            haveInput = true;
        }
        */
        inputVector = transform.forward * vertical;
        inputVector += transform.right * horizontal;
        //inputVector = WalkVector.normalized;
        inputVector.Normalize();
        //

        //HitBallUp
        int layermask = 1 << 8;
        Ray AimRay = new Ray(Camera.main.transform.position, Camera.main.transform.forward);

        Debug.DrawRay(AimRay.origin, AimRay.direction * maxRayDistance, Color.cyan);

        RaycastHit mouseHit = new RaycastHit();

        if(Physics.Raycast(AimRay, out mouseHit, maxRayDistance, layermask))
        {
            if (Input.GetMouseButtonDown(1))
            {
                mouseHit.rigidbody.velocity = new Vector3(0f, upForce, 0f);
            }
            if (Input.GetMouseButtonDown(0))
            {
                punchDirection = new Vector3(Camera.main.transform.forward.x, Camera.main.transform.forward.y, Camera.main.transform.forward.z);
                mouseHit.rigidbody.velocity = punchDirection * forwardForce;
            }
        }
        //
    }

    void FixedUpdate()
    {
        TempSpeed = inputVector * moveSpeed;
        Dash();
        rb.velocity = new Vector3(TempSpeed.x, rb.velocity.y, TempSpeed.z);
        rb.velocity -= new Vector3(0f, gravity, 0f) * Time.fixedDeltaTime;
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
        if(Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            jump.Play();
            canJump = false;
            rb.velocity += Vector3.up * MaxJumpSpeed;
        }

    }
    //
}
