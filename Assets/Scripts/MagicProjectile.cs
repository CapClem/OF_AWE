using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    public GameObject proj;
    public Vector3 startPos;
    public Vector3 endPos;
    public float lerpTime = 3;
    private float currentLerpTime = 0;
    public float maxDistance = 35;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lerpTime);
        startPos = proj.transform.position;
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, maxDistance))
        {
            endPos = hit.point;
        }
        else
        {
            endPos = transform.position + transform.forward.normalized * maxDistance;
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        currentLerpTime += Time.deltaTime;
        if (currentLerpTime >= lerpTime)
        {
            currentLerpTime = lerpTime;
        }

        float Perc = currentLerpTime / lerpTime;
        proj.transform.position = Vector3.Lerp(startPos, endPos, Perc);
    }
}
