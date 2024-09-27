using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    public ParticleSystem[] BreakParticles;

    public void Break()
    {
        foreach (ParticleSystem particle in BreakParticles)
        {
            particle.Play();
        }

        gameObject.SetActive(false);
    }
}
