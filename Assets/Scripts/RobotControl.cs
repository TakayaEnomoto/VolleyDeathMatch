using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotControl : MonoBehaviour
{
    public static RobotControl Main;
    public int lives;
    public float upforce;
    public float forwardforce;

    private void Awake()
    {
        Main = this;
    }
    void Start()
    {
        lives = 3;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Volleyball")
        {
            collision.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, upforce, forwardforce);
        }
    }
}
