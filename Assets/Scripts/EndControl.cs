using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndControl : MonoBehaviour
{
    public Text cong;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(P1Controller.player.P1Lives == 0)
        {
            cong.color = Color.red;
            cong.text = "YOU LOSE";
        }else if(RobotControl.Main.lives == 0)
        {
            cong.color = Color.cyan;
            cong.text = "YOU WIN";
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene("PlayScene");
        }
    }
}
