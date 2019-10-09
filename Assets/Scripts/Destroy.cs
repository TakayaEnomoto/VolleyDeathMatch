using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Destroy : MonoBehaviour
{
    public float upforce;
    public float forwardforce;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Floor")
        {
            Destroy(other.gameObject);
        }
        else if (other.tag == "Volleyball")
        {
            other.transform.position = RobotControl.Main.transform.position;
            other.gameObject.GetComponent<Rigidbody>().velocity = new Vector3(0f, upforce, forwardforce);
        }else if(other.tag == "Player")
        {
            //SceneManager.LoadScene("End");
        }
    }
}
