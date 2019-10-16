using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class belowNet : MonoBehaviour
{
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
        if (other.gameObject.tag == "Volleyball")
        {
            FloorFall.Me.ballBounceTimes = FloorFall.Me.ballBounceTimesDefault;
            other.gameObject.GetComponent<Rigidbody>().velocity = Vector3.down * FloorFall.Me.startVel;
            other.transform.position = FloorFall.Me.startPos;
        }
    }
}
