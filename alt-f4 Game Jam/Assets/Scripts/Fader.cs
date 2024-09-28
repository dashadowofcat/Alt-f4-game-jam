using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fader : MonoBehaviour
{
    Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void Fade(bool FadeOn)
    {
        if (FadeOn) animator.CrossFade("FadeOn", 0, 0);
        if (!FadeOn) animator.CrossFade("FadeOff", 0, 0);
    }
}
