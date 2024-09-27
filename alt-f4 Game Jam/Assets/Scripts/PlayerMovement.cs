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
    public float MaxSpeed;

    [Header("Jump")]
    public LayerMask PlatformMask;
    public float jumpForce;

    public float JumpReleaseDivider;
    [HideInInspector] public bool CanjumpRelease = false;


    [Header("Ground detection")]
    public Transform GroundDetectorPositon;
    public Vector2 GroundDetectorSize;

    private float MovementVelocity;

    [HideInInspector] public int Direction;

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
        float Horizontal = Input.GetAxisRaw(HorizontalAxis);

        if (Horizontal != 0)
        {
            MovementVelocity += Horizontal * AccelerationSpeed * Time.deltaTime;
        }
        else
        {
            MovementVelocity = Mathf.MoveTowards(MovementVelocity, 0, DeccelerationSpeed * Time.deltaTime);
        }

        MovementVelocity = Mathf.Clamp(MovementVelocity, -MaxSpeed, MaxSpeed);

        rb.velocity = new Vector2(MovementVelocity, rb.velocity.y);

        if(Horizontal != 0) Direction = (Horizontal > 0) ? -1 : 1;

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

        Gizmos.DrawLine(transform.position, transform.position + new Vector3(MovementVelocity, 0));
    }

    public Vector2 GetVelocity() => new Vector2(MovementVelocity, rb.velocity.y);


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

    public bool IsGrounded()
    {
        return Physics2D.BoxCast(GroundDetectorPositon.position, GroundDetectorSize, 0, Vector2.zero, 0, PlatformMask);
    }

    public static PlayerMovement Get()
    {
        return FindObjectOfType<PlayerMovement>();
    }
}
