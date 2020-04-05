using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //Bools to identify the object it is attached to
    [Header("Type Settings")]
    public bool isMushroom;
    public bool isBirch;
    public bool isOak;
    public bool isWillow;
    public bool isWater;
    public bool isFlower;
    public bool isPine;
    public bool isRock;
    

    //Water/Dupe
    [Header("Water/Duplication Settings")]
    public GameObject water;
    private Vector3 theLocation;

    //Mushrooms
    [Header("Mushroom Settings")]
    public Renderer m;
    public GameObject mushTop;
    private bool magicMush;
    public Material matA;
    public Material matB;

    //Flowers
    [Header("Flower Settings")]
    public float jumpHeight;
    public Rigidbody r;

    //Rocks
    [Header("Rock Settings")]
    public float degreesPerSecond = 15.0f;
    public float amplitude = 0.5f;
    public float frequency = 1f;
    private Vector3 posOffset = new Vector3();
    private Vector3 tempPos = new Vector3();
    public bool magicRock;
    public bool hasRisen;
    public GameObject rock;
    public Vector3 startPos;
    public Vector3 endPos;
    private float lerpTime = 3;
    private float currentLerpTime = 0;
    public float distance;

    //Trees
    [Header("Tree Settings")]
    Vector3 minScale;
    public Vector3 maxScale;
    private int grown;
    public float speed = 2f;
    public float duration = 5f;
    private bool magicBirch;
    private float treelerpTime = 3;
    private float growthRate = 0;

    public InteractableEffect interactableEffect;

    // Start is called before the first frame update
     void Start()
     {
        //TreeStart
        minScale = transform.localScale;
        


        //RockStart
        rock = gameObject;
        distance = Random.Range(4f, 8f);
        startPos = rock.transform.position;
        endPos = rock.transform.position + Vector3.up * distance;
        posOffset = endPos;

        //FlowerStart
        if (isFlower == true)
        {
            r = GetComponent<Rigidbody>();
        }
       

        //MushroomStart
        if(isMushroom == true)
        {
            m = mushTop.GetComponent<Renderer>();
        }


        

         if (interactableEffect == null)
             interactableEffect = GetComponent<InteractableEffect>();
    
        //Water/DupeStart
        if (isWater == true)
        {
            water = gameObject;
            //r.AddForce(Vector3.up * jumpHeight);
        }
     }
     
    // Update is called once per frame
    void Update()
    {
        if(magicMush == true)
        {
            m.material = matB;
        }



        //BirchStuff
        if (magicBirch == true)
        {

            growthRate += Time.deltaTime;
            if (growthRate >= treelerpTime)
            {
                growthRate = treelerpTime;
            }

            float treePerc = growthRate / treelerpTime;
            transform.localScale = Vector3.Lerp(minScale, maxScale, treePerc);
        }
        
        //Rockstuff
        if (magicRock == true)
        {
            if (hasRisen == false)
            {
                currentLerpTime += Time.deltaTime;
                if (currentLerpTime >= lerpTime)
                {
                    currentLerpTime = lerpTime;
                }

                float Perc = currentLerpTime / lerpTime;
                rock.transform.position = Vector3.Lerp(startPos, endPos, Perc);

            }
            else
            {
                transform.Rotate(new Vector3(Time.deltaTime * degreesPerSecond, Time.deltaTime * degreesPerSecond, Time.deltaTime * degreesPerSecond), Space.World);
                tempPos = posOffset;
                tempPos.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amplitude;
                transform.position = tempPos;
            }

             if (rock.transform.position == endPos)
             {
                 hasRisen = true;
             }
             
        }


    }

    void FixedUpdate()
    {
        if (isFlower && r.velocity.magnitude > 0.15f)
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
            //m.material.SetColor("_BaseColor", Random.ColorHSV());
            magicMush = true;
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

        else if (isBirch == true)
        {
            magicBirch = true;
        }

        else if (isRock == true)
        {
            magicRock = true;

        }


        Debug.Log("WOOOT");
    }


}