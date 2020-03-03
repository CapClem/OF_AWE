using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllerInput : MonoBehaviour
{
    //Mouse&Keyboard inputs for Testing
    //public AudioClip clip;
    //public AudioSource audioSource;

    public Transform controllerForwardTransform;
    public GameObject magic;

    public bool canShoot;

    // Start is called before the first frame update
    void Start()
    {
        // audioSource = GetComponent<AudioSource>()
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
                Debug.Log("shoot");
            }
        }


        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {

        }

        if (OVRInput.GetDown(OVRInput.Button.One))
        {

        }

        //Mouse&Keyboard inputs for Testing
    }


    private void MagicShoot()
    {
       //Raycast Shot
        RaycastHit hit;

        if (Physics.Raycast(controllerForwardTransform.position, controllerForwardTransform.forward, out hit))
        {
            if (hit.collider.gameObject.CompareTag("Interactable"))
            {
                Destroy(hit.collider.gameObject);
            }
        }

        //Projectile Shot
        GameObject.Instantiate(magic, controllerForwardTransform.position, controllerForwardTransform.forward);
       // GetCompnent<Rigidbody>().AddForce.forward * 10;
        


    }
}
