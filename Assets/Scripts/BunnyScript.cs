using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BunnyScript : MonoBehaviour
{
    public int jumpCount;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void onCollisionEnter (Collision col)
    {
        if (col.gameObject.tag == "Player")
        {
            jumpAway();
        }
    }

    public void jumpAway()
    {
        for (int i = 0; jumpCount > i; i++)
        {
            //Find Vector From Player to Bunny Center



            jumpCount++;
        }
    }
}
