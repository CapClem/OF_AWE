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

        Debug.Log("A (Waiting for Projectile to finish)");
        yield return new WaitForSeconds(2f);
        Debug.Log("B (Projectile has finished lerping)");
        Debug.Log("C (Start new Particle Explosion)");
        Debug.Log("Play Explosion Sound");
        yield return new WaitForSeconds(2f);
       
        Debug.Log("D Start Specific Interactions");
        // Woot Woot Say Da Whooot
        //Object Specific Interactiions
        if (GetComponent<InteractableObject>().isMushroom == true)
        {
            //m.material.SetColor("_BaseColor", Random.ColorHSV());
            GetComponent<InteractableObject>().magicMush = true;
        }

        else if (GetComponent<InteractableObject>().isTree == true)
        {
            GetComponent<InteractableObject>().magicTree = true;
            Debug.Log("Play Particle System With Gravity enabled to show motion");
            yield return new WaitForSeconds(2f);
            Debug.Log("Stop Particle Systme");
        }

        else if (GetComponent<InteractableObject>().isRock == true)
        {
            GetComponent<InteractableObject>().magicRock = true;
            
        }

        else if (GetComponent<InteractableObject>().isFlower == true)
        {
            GetComponent<InteractableObject>().flowerJump();
            Debug.Log("Start Gravity Particle Effect");
            Debug.Log("Play Sound for Flower");
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
        */
       
        /*
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




