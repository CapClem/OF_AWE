using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBurst : MonoBehaviour
{
    protected bool _isPlaying;
    public AudioSource audioSource;
    public AudioClip audioClip;
    public ParticleSystem particlesSystem;


    public bool IsPlaying
    {
        get => _isPlaying;
        protected set => _isPlaying = value;
    }

    // Start is called before the first frame update
    void Start()
    {

        if (audioSource == null)
        {
            audioSource = GetComponent<AudioSource>();
        }
            
    }

    IEnumerator MagicBurstCoroutine()
    {

        Debug.Log("A");
        Debug.Log("B");
        yield return new WaitForSeconds(3f);

        Debug.Log("C");
        yield return new WaitForSeconds(3f);
       
        Debug.Log("my coroutine is finished");
        // Woot Woot Say Da Whooot

        if (GetComponent<InteractableObject>().isMushroom == true)
        {
            //m.material.SetColor("_BaseColor", Random.ColorHSV());
            GetComponent<InteractableObject>().magicMush = true;
        }

        else if (GetComponent<InteractableObject>().isTree == true)
        {
            GetComponent<InteractableObject>().magicTree = true;
        }

        else if (GetComponent<InteractableObject>().isRock == true)
        {
            GetComponent<InteractableObject>().magicRock = true;
        }
        Debug.Log("WOOOT");

        /*else if (isWater == true)
        {
            for (int i = 0; i < 10; i++)
            {
                theLocation = (water.transform.position - new Vector3(Random.Range(-2f, 2f), 0, Random.Range(-2f, 2f)));
                Instantiate(water, theLocation, Quaternion.identity);
            }
        }

        /*else if (isFlower == true)
        {
            r.AddForce(Vector3.up * jumpHeight);
            Debug.Log("im flying");
        }

        else if (isOak == true)
        {
            transform.localScale += new Vector3(0, 1, 0);
        }

        else if (isWillow == true)
        {
            transform.localScale += new Vector3(0, 1, 0);
        }

        else if (isPine == true)
        {
            transform.localScale += new Vector3(0, 1, 0);
        }*/

    }

    public void PlayEffect()
    {
        StartCoroutine(MagicBurstCoroutine());
    }
}




