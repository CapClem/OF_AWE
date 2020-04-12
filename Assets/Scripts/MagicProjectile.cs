using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicProjectile : MonoBehaviour
{
    public GameObject proj;
    public Vector3 startPos;
    public Vector3 endPos;
    public float lerpTime;
    public float travelTime;
    public float explosionTime;
    private float currentLerpTime = 0;
    public float maxDistance = 35;
    public ParticleSystem explosionParticle;
    // Start is called before the first frame update
    void Start()
    {
        travelTime = lerpTime - 1;
        explosionTime = lerpTime - travelTime;
        Destroy(gameObject, lerpTime);
        StartCoroutine(magicExplosion());
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
        if (currentLerpTime >= travelTime)
        {
            currentLerpTime = travelTime;
        }

        float Perc = currentLerpTime / travelTime;
        proj.transform.position = Vector3.Lerp(startPos, endPos, Perc);
    }


    IEnumerator magicExplosion()
    {
        yield return new WaitForSeconds(travelTime);
        explosionParticle.Play();
        Debug.Log("Explosion Courotine A");
   }
}
