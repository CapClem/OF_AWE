using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    //Mouse&Keyboard inputs for Testing
    Vector2 rotation = new Vector2(0, 0);
    public float speed = 3;


    public bool canShoot;

    //public AudioClip clip;
    //public AudioSource audioSource;

    //public Transform handForwardTransform;


    // Start is called before the first frame update
    void Start()
    {
       // audioSource = GetComponent<AudioSource>()'
       // audioSource.clip = clip;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);

        if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {
            if (canShoot == true)
            {
                MagicShoot();
            }
        }


        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {

        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {

        }

       /* if (OVRInput.GetDown(OVRInput.Two))
        {

        }
        */
        //Mouse&Keyboard inputs for Testing
        rotation.y += Input.GetAxis("Mouse X");
        rotation.x += -Input.GetAxis("Mouse Y");
        transform.eulerAngles = (Vector2)rotation * speed;
    }



    private void MagicShoot()
    {

    }
}
