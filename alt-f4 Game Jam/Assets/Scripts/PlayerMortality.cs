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
            IsDead = true;
        }
    }

    public void Revive()
    {
        transform.position = CurrentLevel.RespawnAnchor.position;

        IsDead = false;
    }

    public static PlayerMortality Get()
    {
        return FindObjectOfType<PlayerMortality>();
    }
}
