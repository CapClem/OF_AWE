using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //Bools to identify the object it is attached to
    [Header("Type Settings")]
    public bool isMushroom;
    public bool isTree;
    public bool isFlower;
    public bool isRock;
    public bool isDuplication;
    

    //Water/Dupe
    [Header("Duplication Settings")]
    public GameObject duplicationObject;
    private Vector3 theLocation;

    //Mushrooms
    [Header("Mushroom Settings")]
    public Renderer m;
    public GameObject mushTop;
    public bool magicMush;
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
    public bool magicTree;
    private float treelerpTime = 3;
    private float growthRate = 0;

    public MagicBurst interactableEffect;

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
             interactableEffect = GetComponent<MagicBurst>();
    
        //DupeStart
        if (isDuplication == true)
        {
            duplicationObject = gameObject;
        }
     }
     
    // Update is called once per frame
    void Update()
    {
        if(magicMush == true)
        {
            m.material = matB;
        }



        //TreeStuff
        if (magicTree == true)
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
        //interactableEffect?.Stop();
    }

    public void magicHappens()
    {
        interactableEffect?.PlayEffect();
    }

    public void flowerJump()
    {
        r.AddForce(Vector3.up * jumpHeight);
    }
}