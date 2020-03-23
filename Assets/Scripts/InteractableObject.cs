using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //Bools to identify the object it is attached to
    public bool isMushroom;
    public bool isBirch;
    public bool isOak;
    public bool isWillow;
    public bool isWater;
    public bool isFlower;
    public GameObject water;
    public Vector3 theLocation;
    public Rigidbody r;
    public Renderer m;

    //specify how many times the object can be changed
    public int timesRemaining;

    // Start is called before the first frame update
         void Start()
         {

             r = GetComponent<Rigidbody>();

            m = GetComponent<Renderer>();
        
        //Sets water to be the gameobject this script is attached to
            if (isWater == true)
                {
                water = gameObject;            
                }
         }
        // Update is called once per frame
        void Update()
        {

        }


        public void magicHappens()
        {
            if (isMushroom == true)
            {
                m.material.SetColor("_BaseColor", Random.ColorHSV());

            }

            else if (isBirch == true)
            {
                transform.localScale += new Vector3(0, 1, 0);
            }

            else if (isWater == true)
            {
            for (int i = 0; i < 10; i++)
                {
                    theLocation = (water.transform.position - new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f)));
                    Instantiate(water, theLocation, Quaternion.identity);
                }
                
            }

            else if (isFlower == true)
            {
                r.AddForce(Vector3.up * 500f);
                Debug.Log("im flying");
            }

            else if (isOak == true)
            {
                transform.localScale += new Vector3(0, 1, 0);
            }

            else if (isWillow == true)
            {
                transform.localScale += new Vector3(0, 1, 0);
            }


        Debug.Log("WOOOT");
        }

        //As magic collides with interactable object, Do "X"
        //Can, change material, change scale/height/width
        //Move, destroy etc.
        /*  void OnCollisionEnter(Collision col)
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
          */

    
}