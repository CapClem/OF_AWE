using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //Bools to identify the object it is attached to
    public bool isMushroom;
    public bool isTree;
    public bool isWater;
    public bool isFlower;

    //specify how many times the object can be changed
    public int timesRemaining;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }


    //As magic collides with interactable object, Do "X"
    //Can, change material, change scale/height/width
    //Move, destroy etc.
    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Magic" && timesRemaining != 0)
        {
            if (isMushroom == true)
            {
              //  gameObject.GetComponent<Transform>().Scale * 2;
            }

            else if (isTree == true)
            {
              //  gameObject.GetComponent<Transform>().Scale / 2;
            }

            else if (isWater == true)
            {
               // gameObject.GetComponent<Transform>().Scale * 2;

            }

            else if (isFlower == true)
            {
               // Destroy(gameObject);
            }

        }
        //Reduces the value to limit the amount of times the object can warp
        timesRemaining -= 1;

    }
}
