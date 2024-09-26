using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[RequireComponent(typeof(Collider2D), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Input")]
    [InputAxis]
    public string HorizontalAxis;

    public KeyCode JumpKey;

    [Header("Movement")]
    public float AccelerationSpeed;
    public float DeccelerationSpeed;

    [Header("Jump")]
    public LayerMask PlatformMask;
    public float jumpForce;

    public float JumpReleaseDivider;
    private bool CanjumpRelease = false;


    [Header("Ground detection")]
    public Transform GroundDetectorPositon;
    public Vector2 GroundDetectorSize;

    private float MovementVelocity;

    [HideInInspector] public BoxCollider2D coll;
    [HideInInspector] public SpriteRenderer sprite;
    [HideInInspector] public Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        MovementVelocity += Input.GetAxisRaw(HorizontalAxis) * AccelerationSpeed * Time.deltaTime;

        MovementVelocity = Mathf.Lerp(MovementVelocity, 0, DeccelerationSpeed * Time.deltaTime);

        rb.velocity = new Vector2(MovementVelocity, rb.velocity.y);



        if (Input.GetKeyDown(JumpKey) && IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            CanjumpRelease = true;
        }

        if (Input.GetKeyUp(JumpKey) && rb.velocity.y > 0 && CanjumpRelease)
        {
            CanjumpRelease = false;
            rb.velocity /= new Vector2(1, JumpReleaseDivider);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;

        if (GroundDetectorPositon != null) Gizmos.DrawWireCube(GroundDetectorPositon.position, GroundDetectorSize);

        Gizmos.DrawLine(transform.position, transform.position + new Vector3(0, MovementVelocity));
    }

    public Vector2 GetVelocity()
    {
        return new Vector2(MovementVelocity, rb.velocity.y);
    }

    public void SetVelocity(Vector2 velocity)
    {
        rb.velocity = new Vector2(rb.velocity.x, velocity.y);
        MovementVelocity = velocity.x;
    }

    public void AddVelocity(Vector2 velocity)
    {
        rb.velocity += new Vector2(0, velocity.y);
        MovementVelocity += velocity.x;
    }

    private bool IsGrounded()
    {
        return Physics2D.BoxCast(GroundDetectorPositon.position, GroundDetectorSize, 0, Vector2.zero, 0, PlatformMask);
    }

    public static PlayerMovement Get()
    {
        return FindObjectOfType<PlayerMovement>();
    }
}
