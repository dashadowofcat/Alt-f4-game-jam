using NaughtyAttributes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class GunHandler : MonoBehaviour
{
    [Header("Input")]
    public int ShootButton;

    private PlayerMovement Movement;

    void Start()
    {
        Movement = PlayerMovement.Get();
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(ShootButton))
        {

        }
    }
}
