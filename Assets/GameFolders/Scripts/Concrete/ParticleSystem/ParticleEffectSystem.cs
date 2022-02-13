using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectSystem : MonoBehaviour
{
    [SerializeField] ParticleSystem victoryConfetti;

    public void VictoryConfetti()
    {
        victoryConfetti.Play();
    }
}
