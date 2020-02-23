using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Movement : MonoBehaviour
{

    public OVRInput.Controller goController;
    public NavMeshAgent navAgent;
    public Transform navDest;
    public GameObject navTo;


    void Start()
    {

        goController = OVRInput.Controller.RTrackedRemote;
        navAgent = gameObject.GetComponent<NavMeshAgent>();

        if(navTo.activeSelf == false)
        {

            navTo.SetActive(true);

        }

    }

    void Update()
    {
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

        RaycastHit Hit;

        //transform.rotation = OVRInput.GetLocalControllerRotation(goController);

        if (Physics.Raycast(transform.position, transform.forward, out Hit, 50))
        {

            navTo.transform.position = Hit.transform.position;

            if (OVRInput.GetDown(OVRInput.Button.PrimaryIndexTrigger))
            {

                navDest = Hit.transform;
                navAgent.destination = Hit.transform.position;

            }                

        }

    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.red;

        Gizmos.DrawLine(transform.position, transform.forward);

    }

}
