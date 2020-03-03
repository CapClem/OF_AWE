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
    public GameObject cubeOne;
    public GameObject trackingS;


    void Start()
    {
        
        goController = OVRInput.Controller.RTrackedRemote;
        navAgent = gameObject.GetComponentInParent<NavMeshAgent>();

    }

    void Update()
    {
        //OVRInput.Update();

    }

    void FixedUpdate()
    {
        //OVRInput.FixedUpdate();

        RaycastHit Hit;

        transform.rotation = OVRInput.GetLocalControllerRotation(goController);

        if (Physics.Raycast(transform.position, transform.forward, out Hit, 20))
        {

            navTo.transform.position = Hit.point;
            //navAgent.destination = Hit.point;

            if (OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad))
            {

                //Instantiate(cubeOne, Hit.point, Quaternion.identity);

                navAgent.destination = Hit.point;

            }

        }

    }

}
