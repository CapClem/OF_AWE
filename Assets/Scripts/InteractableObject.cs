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
    public bool isPine;
    public bool isNewFlower;
    public GameObject water;
    public GameObject mushTop;
    private Vector3 theLocation;
    private Rigidbody r;
    private Renderer m;
    public int rotSpeed;
    public float jumpHeight;

    public InteractableEffect interactableEffect;
    

    //specify how many times the object can be changed
    public int timesRemaining;

    // Start is called before the first frame update
     void Start()
     {

         r = GetComponent<Rigidbody>();
         if(mushTop != null)
            m = mushTop.GetComponent<Renderer>();

         if (interactableEffect == null)
             interactableEffect = GetComponent<InteractableEffect>();
    
        //Sets water to be the gameobject this script is attached to
        if (isWater == true)
        {
            water = gameObject;            
        }

        if (isNewFlower == true)
        {
            r.AddForce(Vector3.up * jumpHeight);
        }
     }
     
    // Update is called once per frame
    void Update()
    {

    }

    void FixedUpdate()
    {
        if (isFlower == true && r.velocity.magnitude > 0.05f)
        {
            transform.Rotate(0, 5f, 0);
        }
            
    }

    public void StopMagic()
    {
        interactableEffect?.Stop();
    }

    public void magicHappens()
    {
        interactableEffect?.PlayEffect();
        
        // Woot Woot Say Da Whooot
        
        
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
            r.AddForce(Vector3.up * jumpHeight);
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

        else if (isPine == true)
        {
            transform.localScale += new Vector3(0, 1, 0);
        }


        Debug.Log("WOOOT");
    }


}