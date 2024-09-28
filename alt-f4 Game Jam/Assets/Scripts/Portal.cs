using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform TeleportOutput;

    public Vector2 Shake;

    public int HitPause;

    [Header("Sound")]
    public AudioClip TeleportSound;

    public float PitchVariation;

    public AudioSource Audio;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = TeleportOutput.transform.position;

        HitPauseManager.Pause(HitPause);

        CameraShake.Shake(Shake);

        PlaySound();
    }

    private void PlaySound()
    {
        Audio.pitch = 1;

        Audio.pitch += UnityEngine.Random.Range(-PitchVariation, PitchVariation);

        Audio.PlayOneShot(TeleportSound);
    }
}
