using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class ExampleInteractableEffect : InteractableEffect
{
    #region Variables

    public AudioSource audioSource;
    
    public AudioClip someAudioClip;
    public ParticleSystem someParticleSystem;

    #endregion
    
    
    private new void Start()
    {
        base.Start();
        
        if(audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    protected override IEnumerator EffectCoroutine()
    {
        audioSource.Play(); // Start(Play) Audio Source (For example an ambient "background music" for the duration of the effect)
        
        
        someParticleSystem?.Play(); // Play some particle system
        
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        
        
        yield return new WaitForEndOfFrame(); // Wait until the end of the frame

        var player = FindObjectOfType<Player>();
        AudioSource.PlayClipAtPoint(someAudioClip, player.transform.position); // Play a certain audio clip at the players position
        
        yield return new WaitForSeconds(someAudioClip.length); // Wait until the audioclip is done playing
        
        someParticleSystem?.Play(); // Play some particle system
        
        yield return new WaitForSeconds(3f); // Wait for 3 seconds
        
        someParticleSystem?.Stop(); // Stop previously started particle system
        
        ResetEffect();
        
        yield return 0;
    }

    // Called when the effect ends or the effect gets stopped
    protected override void ResetEffect()
    {
        audioSource.Stop();
        someParticleSystem?.Stop();
        someParticleSystem?.Clear();
    }
}
