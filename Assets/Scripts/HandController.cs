using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandController : MonoBehaviour
{
    public bool stopIntrActOnStopShoot = true;
    //public bool CanShoot;
    public bool CanShoot => player?.HasMagic ?? false;

    public ParticleSystem shootParticleSys;
    public ParticleSystem hasMagicParticleSys;

    [Header("Interactable Object Slots")]
    public GameObject objectSlot;
    public GameObject emptySlot;

    [Header("Projectile Settings")]
    public GameObject magicProjectile;
    public GameObject magicspawnLocation;
    public RaycastHit hit;

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

        //FOR KEYBOARD
        if (CanShoot)
        {          
            if (Input.GetKeyDown(KeyCode.Z))
            {
                //shootParticleSys?.Play();
                GameObject newProj;
                newProj = Instantiate(magicProjectile, magicspawnLocation.transform.position, Quaternion.identity);
                objectSlot?.GetComponent<InteractableObject>()?.magicHappens();
            }
        }

        if (Input.GetKeyUp(KeyCode.Z) || !CanShoot)
        {
            if(stopIntrActOnStopShoot)
                objectSlot?.GetComponent<InteractableObject>()?.StopMagic(); // TODO 
            shootParticleSys?.Stop();
            shootParticleSys?.Clear();
        }

        // FOR OVR
        if (CanShoot)
        {  
            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {
                //shootParticleSys?.Play();
                GameObject newProj;
                newProj = Instantiate(magicProjectile, magicspawnLocation.transform.position, Quaternion.identity);
                objectSlot?.GetComponent<InteractableObject>()?.magicHappens();
            }
        }

        if (OVRInput.GetUp(OVRInput.Button.PrimaryIndexTrigger) || !CanShoot)
        {
            if (stopIntrActOnStopShoot)
                objectSlot?.GetComponent<InteractableObject>()?.StopMagic(); // TODO 
            shootParticleSys?.Stop();
            shootParticleSys?.Clear();
        }
    }
}
