using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlInput : MonoBehaviour
{
    public float speed;
    public GameObject centerEye;
    public GameObject playerObject;
    public GameObject handObject;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {   //Input.GetKeyDown(KeyCode.UpArrow)
        //OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad)
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            transform.eulerAngles = new Vector3(0, centerEye.transform.localEulerAngles.y, 0);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            //handObject.transform.eulerAngles = new Vector3(0, centerEye.transform.localEulerAngles.y, 0);

            //If we want strafing
            //transform.Translate(Vector3.right * speed * Time.deltaTime)
            //playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, transform.position, Time.deltaTime);
        }
 



    }
}
