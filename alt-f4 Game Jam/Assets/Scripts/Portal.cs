using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform TeleportOutput;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        collision.transform.position = TeleportOutput.transform.position;
    }
}
