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

    public float DashCount = 0f;
    public float DashTime = .5f;
    public bool canDash = true;
    public bool Dashing = false;
    public Vector3 DashVector;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
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
                DashCount = 10f;
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
            DashTime = .5f;
            Dashing = false;
        }
        else
        {

        }
        //

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
        rb.velocity = inputVector * moveSpeed + Physics.gravity * .69f;
        Dash();
    }

    //DashVoid
    public void Dash()
    {
        if (Dashing)
        {
            rb.velocity = DashVector * DashSpeed + Physics.gravity * .69f;
        }
    }
}
