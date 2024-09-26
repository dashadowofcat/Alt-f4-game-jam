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
    public int HitPause;

    public float SideCooldown;

    private float SideCoolDownTimer;

    public float SideRecoil;

    public float SideScreenShake;

    public float DownwardRecoil;

    public float DownwardScreenShake;

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
        Movement.CanjumpRelease = false;

        Movement.SetVelocity(new Vector2(SideRecoil * Movement.Direction, 0));

        CameraShake.Shake(new Vector2(SideRecoil * Movement.Direction, 0) * SideScreenShake);

        HitPauseManager.Pause(HitPause);
    }

    public void FireDown()
    {
        Movement.CanjumpRelease = false;

        Movement.SetVelocity(new Vector2(0, DownwardRecoil));

        CameraShake.Shake(new Vector2(0, DownwardRecoil) * DownwardScreenShake);

        HitPauseManager.Pause(HitPause);
    }
}
