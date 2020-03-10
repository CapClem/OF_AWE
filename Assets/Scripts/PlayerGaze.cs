using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGaze : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void GazeEnter()
    {
        Debug.Log("Gaze Entered");
        //Create timer, which starts s the player looks at the STATUE
        //At the same time, start a function which plays the music and animations
        //for showing the power gain

        //Starts a function which gives the player MAGIC after the set time is complete
        //For everything else, Make it glow or cursor on HUD
    }

    public void GazeExit()
    {
        Debug.Log("Gaze Exited");
        //Statue, If timer is active, Stop timer.
        //For everything else Remove the cursor on HUD
        // 
        
    }
}
