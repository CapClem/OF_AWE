using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.VR;

public class Movement : MonoBehaviour
{

    public OVRInput.Controller goController;
    public NavMeshAgent navAgent;
    public Transform navDest;
    public GameObject navTo;


    void Start()
    {
        
        goController = OVRInput.Controller.RTrackedRemote;
        navAgent = gameObject.GetComponentInParent<NavMeshAgent>();

        /*if(navTo.activeSelf == false)
        {

            navTo.SetActive(true);

        }*/

    }

    void Update()
    {
        OVRInput.Update();
        
        
        /*if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
        {

            RaycastHit Hit;

            //transform.rotation = OVRInput.GetLocalControllerRotation(goController);

            if (Physics.Raycast(transform.position, transform.forward, out Hit, 50))
            {
                navDest = Hit.transform;
                navAgent.destination = Hit.transform.position;

            }

        }*/

       

    }

    void FixedUpdate()
    {
        OVRInput.FixedUpdate();

        RaycastHit Hit;

        transform.rotation = OVRInput.GetLocalControllerRotation(goController);

        if (Physics.Raycast(transform.position, transform.forward, out Hit, 20))
        {

            navTo.transform.position = Hit.point;
            //navAgent.destination = Hit.point;

            if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
            {

                Debug.Log("Button Pressed");
                //navDest = Hit.transform;
                navAgent.destination = Hit.point;

            }

        }

    }

    /*private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position += new Vector3(0, 0, 0), transform.forward * 10);

    }*/

}
