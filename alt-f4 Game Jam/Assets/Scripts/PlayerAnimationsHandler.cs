using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimationsHandler : MonoBehaviour
{
    private GunHandler Gun;
    private PlayerMovement Movement;
    private SpriteRenderer Renderer;
    private Animator Animator;



    public enum PlayerAnimation
    {
        idle,
        walking,
        jumping,
        falling,
        fireDown,
        FireSide
    }

    public PlayerAnimation CurrentAnimation;

    [Serializable]
    public struct PlayerAnimationClipPair
    {
        public PlayerAnimation Animation;
        public AnimationClip Clip;
    }

    public List<PlayerAnimationClipPair> Clips;


    void Start()
    {
        Movement = PlayerMovement.Get();
        Gun = GunHandler.Get();
        Renderer = GetComponent<SpriteRenderer>();
        Animator = GetComponent<Animator>();
    }

    public void Update()
    {
        Renderer.flipX = Movement.Direction > 0;

        Vector2 velocity = Movement.GetVelocity();

        bool IsGrounded = Movement.IsGrounded();

        CurrentAnimation = PlayerAnimation.idle;

        if (velocity.y > .1f && !IsGrounded) CurrentAnimation = PlayerAnimation.falling;

        if (velocity.y < .1f && !IsGrounded) CurrentAnimation = PlayerAnimation.jumping;

        if (MathF.Abs(Input.GetAxisRaw(Movement.HorizontalAxis)) > .3f && IsGrounded) CurrentAnimation = PlayerAnimation.walking;

        if (Gun.HasShotDownSinceTouchedGround) CurrentAnimation = PlayerAnimation.fireDown;

        if (Gun.IsShootingSide) CurrentAnimation = PlayerAnimation.FireSide;

        Animator.CrossFade(Clips.Where(C => C.Animation == CurrentAnimation).FirstOrDefault().Clip.name, 0, 0);
    }
}
