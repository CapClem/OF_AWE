using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public bool stopIntrActOnStopShoot = true;
    //public bool canShoot;
    public bool CanShoot => player?.HasMagic ?? false;

    public ParticleSystem shootParticleSys;
    public ParticleSystem hasMagicParticleSys;
    
    public GameObject objectSlot;
    public GameObject emptySlot;

    public Player player;
    // Start is called before the first frame update
    void Start()
    {
        emptySlot = new GameObject();
        objectSlot = emptySlot;

        if (player == null)
        {
            player = FindObjectOfType<Player>();
        }
        if(player != null)
            player.OnHasMagicStateChanged += HasMagicChanged;
    }

    private void HasMagicChanged(bool hasMagic)
    {
        if (hasMagic && !hasMagicParticleSys.isPlaying)
        {
            hasMagicParticleSys?.Play();
        }
        else
        {
            hasMagicParticleSys?.Stop();
        }
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTrackedRemote);
        //Shoots out a raycast, if it hits an interactable object it will store it in the Slot
        //Which we can call the InteractableObject script and use the functions inside it from here
        if (Physics.Raycast(transform.position, transform.forward, out hit))
        {
            if(hit.collider.gameObject.CompareTag("Interactable"))
            {
            
              
                if(objectSlot != hit.collider.gameObject)
                {
                    objectSlot.transform.SendMessage("OnVRExit");
                    objectSlot = hit.transform.gameObject;
                    objectSlot.transform.SendMessage("OnVREnter");
                    Debug.Log("on VR Raycast Enter");
                }

            }
        }
        else
        {
            if (objectSlot != null)
            {
                objectSlot.transform.SendMessage("OnVRExit");
                objectSlot = emptySlot;
            }
        }
           
        if(CanShoot)
        {   //Input.GetKeyDown(KeyCode.UpArrow
            //OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger)
            
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                shootParticleSys?.Play();
                objectSlot?.GetComponent<InteractableObject>()?.magicHappens();
            }
        }

        if (Input.GetKeyUp(KeyCode.UpArrow) || !CanShoot)
        {
            if(stopIntrActOnStopShoot)
                objectSlot?.GetComponent<InteractableObject>()?.StopMagic(); // TODO 
            shootParticleSys?.Stop();
            shootParticleSys?.Clear();
        }




    }
}
