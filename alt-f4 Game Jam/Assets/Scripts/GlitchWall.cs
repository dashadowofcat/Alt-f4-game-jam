using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlitchWall : MonoBehaviour
{
    public float sineStrengh;
    public float moveSpeed;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 newPosition = transform.position;
        newPosition.y += MathF.Sin(Time.time) * sineStrengh;
        newPosition.x += moveSpeed * Time.deltaTime;
        transform.position = newPosition;
    }
}
