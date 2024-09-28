using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float tension;

    public float dampening;


    private static Vector2 velocity;

    private Vector2 basePosition;

    private Vector2 currentPosition;

    public static void Shake(Vector2 Velocity)
    {
        velocity = Velocity;
    }

    public static void Shake(float x)
    {
        velocity = new Vector2(x, velocity.y);
    }

    private void FixedUpdate()
    {
        Vector2 heightOffset = basePosition - currentPosition;
        velocity += tension * heightOffset - velocity * dampening;
        currentPosition += velocity;

        transform.position += (Vector3)velocity;
    }
}
