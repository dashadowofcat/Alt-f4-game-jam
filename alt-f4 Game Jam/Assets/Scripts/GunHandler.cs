using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class GunHandler : MonoBehaviour
{
    [Header("Input")]
    public KeyCode FireKey;
    public KeyCode FireDownModifier;

    [Header("Gun parameters")]
    public float SideCooldown;

    private float SideCoolDownTimer;

    public float SideRecoil;

    public float DownwardRecoil;

    private bool HasShotDownSinceTouchedGround;

    private PlayerMovement Movement;

    void Start()
    {
        Movement = PlayerMovement.Get();
    }

    void Update()
    {
        if (SideCoolDownTimer > 0) SideCoolDownTimer -= Time.deltaTime;


        if (Movement.IsGrounded()) HasShotDownSinceTouchedGround = false;

        if (Input.GetKeyDown(FireKey) && Input.GetKey(FireDownModifier) && HasShotDownSinceTouchedGround == false)
        {
            HasShotDownSinceTouchedGround = true;

            FireDown();
        }
        else if (Input.GetKeyDown(FireKey) && SideCoolDownTimer <= 0)
        {
            Fire();
            SideCoolDownTimer = SideCooldown;
        }
           
    }

    public void Fire()
    {
        print("shoot");

        Movement.AddVelocity(new Vector2(SideRecoil * Movement.Direction, 0));
    }

    public void FireDown()
    {
        print("shoot down");

        Movement.SetVelocity(new Vector2(0, DownwardRecoil));
    }
}
