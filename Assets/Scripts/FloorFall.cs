using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FloorFall : MonoBehaviour
{
    public Rigidbody rb;
    public float upForce;
    public float forwardforce;
    public float minVelocity;
    public float maxVelocity;
    public float startVel;
    public int ballBounceTimes;
    public int ballBounceTimesDefault = 5;
    public Vector3 startPos;

    public Color fourColor;
    public Color threeColor;
    public Color twoColor;
    public Color oneColor;
    public Color ballColor;

    public static FloorFall Me;

    private void Awake()
    {
        Me = this;
    }

    void Start()
    {
        startPos = transform.position;
        ballBounceTimes = ballBounceTimesDefault;
        rb = GetComponent<Rigidbody>();
        rb.velocity = Vector3.down * startVel;
    }

    void Update()
    {
        //Change to xyz
        
        if(rb.velocity.magnitude >= maxVelocity)
            rb.velocity = rb.velocity.normalized * maxVelocity;
        
        if (rb.velocity.magnitude <= maxVelocity)
            rb.velocity = rb.velocity.normalized * maxVelocity;

        //change ball color
        var ballRenderer = GetComponent<MeshRenderer>();
        if(ballBounceTimes == 5)
            ballRenderer.material.color = ballColor;
        if (ballBounceTimes == 4)
            ballRenderer.material.color = fourColor;
        if (ballBounceTimes == 3)
            ballRenderer.material.color = threeColor;
        if (ballBounceTimes == 2)
            ballRenderer.material.color = twoColor;
        if (ballBounceTimes == 1)
            ballRenderer.material.color = oneColor;
        //ballColor = Color.Lerp(fullColor, lowColor, (ballBounceTimesDefault-ballBounceTimes) / ballBounceTimesDefault);

        //End Game
        if(RobotControl.Main.lives <= 0 || P1Controller.player.P1Lives <= 0)
        {
            SceneManager.LoadScene("End");
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        ballBounceTimes -= 1;
        if(ballBounceTimes == 0)
        {
            rb.velocity = Vector3.down * startVel;
            transform.position = startPos;
            ballBounceTimes = ballBounceTimesDefault;
        }
        if (collision.gameObject.tag == "Floor")
        {
            P1Controller.player.P1Lives -= 1;
            rb.velocity = Vector3.down * startVel;
            transform.position = startPos;
            ballBounceTimes = ballBounceTimesDefault;
            /*
            RigidbodyConstraints a = new RigidbodyConstraints();
            a = RigidbodyConstraints.FreezeRotation;
            collision.rigidbody.constraints = a;

            rb.velocity += new Vector3(0f, upForce, 0f);
            */
        }
        if(collision.gameObject.tag == "Pile")
        {
            RobotControl.Main.lives -= 1;
            Destroy(collision.gameObject);
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
