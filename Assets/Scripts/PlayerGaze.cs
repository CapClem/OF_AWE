using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGaze : MonoBehaviour
{
    public float playerPowerChargeTime = 10; // Time in seconds the player needs to stare to gain power.
    
    public Transform playerPowerChannelingAnimTarget; // Target Location where the channeling animation flows to on the player
    public Transform playerPowerTransmissionAnimTarget; // Target Location where the final transmition of power animation flows to on the player

    public event Action<bool> OnGazeStateChanged;
    public event Action OnPowerReceived;

    public bool gazeActive;
    public PowerObject starredPowerObject;

    public float activeGazeTime = 0;
    public bool activeGazeMagicGained = false;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (gazeActive)
        {
            activeGazeTime += Time.deltaTime;

            if (activeGazeMagicGained == false && activeGazeTime >= playerPowerChargeTime)
            {
                activeGazeMagicGained = true;
                starredPowerObject?.ChannelTarget(false); // Stop Channeling Animation
                GainMagic();
            }
        }
    }

    private void GainMagic()
    {
        OnPowerReceived?.Invoke();
        starredPowerObject?.TransmitPower(true);
        
        // TODO Start Gain Magic Effect, activate magic etc...
    }


    private void GazeEnter()
    {
        gazeActive = true;
        activeGazeTime = 0;
        OnGazeStateChanged?.Invoke(true);
        Debug.Log("Gaze Entered");

        starredPowerObject?.ChannelTarget(true);
        //Create timer, which starts s the player looks at the STATUE
        //At the same time, start a function which plays the music and animations
        //for showing the power gain

        //Starts a function which gives the player MAGIC after the set time is complete
        //For everything else, Make it glow or cursor on HUD
    }

    private void GazeExit()
    {
        activeGazeTime = 0;
        OnGazeStateChanged?.Invoke(false);
        Debug.Log("Gaze Exited");
        starredPowerObject?.ChannelTarget(false);
        //Statue, If timer is active, Stop timer.
        //For everything else Remove the cursor on HUD
        // 
        gazeActive = false;
        starredPowerObject = null;
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PowerObject powerObject = other.GetComponent<PowerObject>();
        if (powerObject != null)
        {
            
            // TODO / Info: Currently it only checks in the beginning if there are any obstacles between the player and the statue.
            powerObject.currentChannelTarget = playerPowerChannelingAnimTarget;
            powerObject.currentTransmissionTarget = playerPowerTransmissionAnimTarget;
            var startPos = transform.position;
            var rayDir = other.transform.position - startPos;
            if (Physics.Raycast(startPos, rayDir, rayDir.magnitude + 1))
            {
                if (starredPowerObject != null)
                    GazeExit();
                starredPowerObject = powerObject;
                GazeEnter();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PowerObject powerObject = other.GetComponent<PowerObject>();
        if (powerObject != null && powerObject == starredPowerObject)
        {
            GazeExit();
        }
    }
}
