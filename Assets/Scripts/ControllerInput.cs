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
    public float cooldown;
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
            if (canShoot == true && cooldown <= 0)
            {
                MagicShoot();
                Debug.Log("shoot");
            }
        }


        if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
        {
            //insert movement script
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
        //GameObject.Instantiate(magic, controllerForwardTransform.position, controllerForwardTransform.forward);
        // GetCompnent<Rigidbody>().AddForce.forward * 10;
        //Add force to the instantiated object

        //Add cooldown to magic so they cant spam and cause issues
        //Need IEnumerator and Couroutine
        cooldown += 1;
    }
}
