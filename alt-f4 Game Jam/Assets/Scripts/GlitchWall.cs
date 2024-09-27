using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchWall : MonoBehaviour
{
    public float sineStrengh;
    public float sineSpeed;

    public Vector2 offset;

    public Vector2 moveSpeed;

    private bool started = false;

    void Update()
    {
        if (!started) return;

        Vector3 newPosition = transform.position;
        newPosition.y += MathF.Sin(Time.time * sineSpeed) * sineStrengh;
        newPosition += (Vector3)moveSpeed * Time.deltaTime;

        Vector3 PosOffset = new Vector2(UnityEngine.Random.Range(-offset.x, offset.x), UnityEngine.Random.Range(-offset.y, offset.y));


        transform.position = newPosition + PosOffset;
    }

    public void StartWall()
    {
        started = true;
    }
}
