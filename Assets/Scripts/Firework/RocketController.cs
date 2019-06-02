using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour
{
    public AudioSource onBirthSound;
    public AudioSource onDeathSound;

    private int numberOfParticles = 0;
    private ParticleSystem particleSystem;

    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();    
    }

    void Update()
    {
        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
        }

        if (!onBirthSound && !onDeathSound)
        {
            return;
        }
        int count = particleSystem.particleCount;
        if (count < numberOfParticles)
        {
            onDeathSound.Play();
        }
        else if (count > numberOfParticles)
        {
            onBirthSound.Play();
        }
        numberOfParticles = count;
    }
}
