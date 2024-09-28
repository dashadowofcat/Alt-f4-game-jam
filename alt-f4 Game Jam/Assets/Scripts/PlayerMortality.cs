using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMortality : MonoBehaviour
{
    public Material DefaultPlayerMaterial;
    public Material GlitchPlayerMaterial;

    public bool UseGlitchMaterial;

    [HideInInspector] public bool IsDead;

    public Level CurrentLevel;

    [Header("Sound")]
    public AudioClip DeathSound;

    public float PitchVariation;

    public AudioSource Audio;


    private SpriteRenderer sprite;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        sprite.material = UseGlitchMaterial ? GlitchPlayerMaterial : DefaultPlayerMaterial;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Hazard"))
        {
            PlaySound();
            IsDead = true;
        }
    }

    public void Revive()
    {
        CurrentLevel.AnchorWall();

        foreach (BreakableWall wall in Resources.FindObjectsOfTypeAll<BreakableWall>())
        {
            wall.gameObject.SetActive(true);
        }

        transform.position = CurrentLevel.RespawnAnchor.position;

        IsDead = false;
    }

    private void PlaySound()
    {
        Audio.pitch = 1;

        Audio.pitch += UnityEngine.Random.Range(-PitchVariation, PitchVariation);

        Audio.PlayOneShot(DeathSound);
    }

    public static PlayerMortality Get()
    {
        return FindObjectOfType<PlayerMortality>();
    }
}
