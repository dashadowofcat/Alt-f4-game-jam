using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class StartTimelineOnTrigger : MonoBehaviour
{
    public PlayableDirector Timeline;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.CompareTag("Player")) return;

        Timeline.Play();
    }
}
