using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerObject : MonoBehaviour
{
    public ParticleTarget channelParticleTarget;
    public ParticleTarget powerParticleTarget;
    
    public Transform currentChannelTarget;
    public Transform currentTransmissionTarget;

    private bool _channelTarget = false;
    private bool _transmitPower = false;

    public void ChannelTarget(bool active, float duration = -1)
    {
        if (_channelTarget == active || channelParticleTarget == null) return;
        
        _channelTarget = active;
        if (_channelTarget)
        {
            channelParticleTarget.particleTarget = currentChannelTarget;
            channelParticleTarget.particleSys.Play();

            if (duration > 0)
            {
                StartCoroutine(ParticleSystemStopper(channelParticleTarget.particleSys, duration));
            }
        }
        else
        {
            channelParticleTarget.particleSys.Stop(true);
        }
    }

    public void TransmitPower(bool active, float duration = -1)
    {
        if (_transmitPower == active || powerParticleTarget == null) return;
        
        _transmitPower = active;
        if (_transmitPower)
        {
            powerParticleTarget.particleTarget = currentTransmissionTarget;
            powerParticleTarget.particleSys.Play();

            if (duration > 0)
            {
                StartCoroutine(ParticleSystemStopper(powerParticleTarget.particleSys, duration));
            }
        }
        else
        {
            powerParticleTarget.particleSys.Stop(true);
        }
    }

    private IEnumerator ParticleSystemStopper(ParticleSystem ps, float duration)
    {
        yield return new WaitForSeconds(duration);
        
        ps?.Stop();
        
        yield return 0;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
