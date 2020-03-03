using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{


    public bool isMushroom;
    public bool isTree;
    public bool isWater;
    public bool isFlower;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void OnCollisionEnter(Collision col)
    {
        if(col.gameObject.tag == "Magic")
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



    }
}
