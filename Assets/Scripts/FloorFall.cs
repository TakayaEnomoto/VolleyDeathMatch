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
    public int ballBounceTimesDefault;
    public Vector3 startPos;

    public GameObject volleyball;

    public Color sixColor;
    public Color sevenColor;
    public Color fourColor;
    public Color threeColor;
    public Color twoColor;
    public Color oneColor;
    public Color ballColor;

    public bool countinst = false;
    public float instTime;
    public float defaultinstTime;

    public static FloorFall Me;

    public AudioSource hitwall;
    public AudioSource desBall;
    public AudioSource desPile;

    public ParticleSystem exp;

    private void Awake()
    {
        Me = this;
    }

    void Start()
    {
        instTime = defaultinstTime;
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
        if (ballBounceTimes == 7)
            ballRenderer.material.color = sevenColor;
        if (ballBounceTimes == 6)
            ballRenderer.material.color = sixColor;
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

        if (countinst)
            instTime -= Time.deltaTime;

        if (instTime <= 0)
        {
            countinst = false;
            instTime = defaultinstTime;
            Instantiate(volleyball, startPos, transform.rotation);
        }
    }


    private void OnCollisionEnter(Collision collision)
    {
        ballBounceTimes -= 1;
        //if(hitwall.isPlaying == false)
        hitwall.Play();
        if(ballBounceTimes == 0)
        {
            hitwall.Stop();
            desBall.Play();
            rb.velocity = Vector3.down * startVel;
            transform.position = startPos;
            ballBounceTimes = ballBounceTimesDefault;
        }
        if (collision.gameObject.tag == "Floor")
        {
            hitwall.Stop();
            P1Controller.player.loseHP.Play();
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
            hitwall.Stop();
            desPile.Play();
            RobotControl.Main.lives -= 1;
            Instantiate(exp, collision.transform.position, collision.transform.rotation);
            Destroy(collision.gameObject);
            countinst = true;
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
