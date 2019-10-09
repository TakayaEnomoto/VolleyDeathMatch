using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFall : MonoBehaviour
{
    public Rigidbody rb;
    public float upForce;
    public float forwardforce;
    public float minVelocity;
    public float maxVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //Change to xyz
        if(rb.velocity.magnitude <= minVelocity)
        {
            rb.velocity = rb.velocity.normalized * minVelocity;
        }
        if(rb.velocity.magnitude >= maxVelocity)
        {
            rb.velocity = rb.velocity.normalized * maxVelocity;
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            P1Controller.player.P1Lives -= 1;
            /*
            RigidbodyConstraints a = new RigidbodyConstraints();
            a = RigidbodyConstraints.FreezeRotation;
            collision.rigidbody.constraints = a;

            rb.velocity += new Vector3(0f, upForce, 0f);
            */
        }
        if(collision.gameObject.tag == "BotFloor")
        {
            RobotControl.Main.lives -= 1;
            rb.velocity += new Vector3(0f, upForce, 0f);
        }
        /*
        Vector3 velocity = rb.velocity;

        var mag = velocity.magnitude;
        Vector3 norm = collision.GetContact(0).normal;
        var dot = Vector3.Dot(-norm, velocity);

        if (dot > 0)
            velocity += norm * (dot * 2f);
        //velocity = velocity.normalized * mag;
        */
    }
}
