using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorFall : MonoBehaviour
{
    public Rigidbody rb;
    public float upForce;
    public float forwardforce;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        
    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            RigidbodyConstraints a = new RigidbodyConstraints();
            a = RigidbodyConstraints.FreezeRotation;
            collision.rigidbody.constraints = a;

            rb.velocity = new Vector3(0f, upForce, 0f);
        }
        if(collision.gameObject.tag == "BotFloor")
        {
            rb.velocity = new Vector3(0f, upForce, forwardforce);
        }
    }
}
