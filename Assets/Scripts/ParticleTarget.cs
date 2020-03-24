
using UnityEngine;

public class ParticleTarget : MonoBehaviour
{
    public ParticleSystem particleSys;
    public Transform particleTarget;
    
    private ParticleSystem.Particle[] _particles;

    private void Start ()
    {
        particleSys = GetComponent<ParticleSystem> ();
    }

    private void Update ()
    {
        _particles = new ParticleSystem.Particle[particleSys.particleCount];
        var particleCount = particleSys.GetParticles(_particles);
        if (particleCount <= 0) return;

        for (var i = 0; i < particleCount; i++)
        {
            var particleTargetDist = Vector3.Distance(particleTarget.position, _particles[i].position);
            var passedLifetime = particleSys.main.startLifetimeMultiplier - _particles[i].remainingLifetime;
            var force = passedLifetime * particleTargetDist * particleSys.main.startSpeedMultiplier;

            _particles [i].velocity = (particleTarget.position - _particles[i].position).normalized * force;
        }

        particleSys.SetParticles (_particles, _particles.Length);
    }
}