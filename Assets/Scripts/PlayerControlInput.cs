using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControlInput : MonoBehaviour
{
    public float speed;
    public GameObject centerEye;
    public GameObject playerObject;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //OVRInput.GetDown(OVRInput.Button.PrimaryTouchpad
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            transform.eulerAngles = new Vector3(0, centerEye.transform.localEulerAngles.y, 0);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
            transform.Translate(Vector3.right * speed * Time.deltaTime);

            playerObject.transform.position = Vector3.Lerp(playerObject.transform.position, transform.position, 10f * Time.deltaTime);
        }
 



    }
}
