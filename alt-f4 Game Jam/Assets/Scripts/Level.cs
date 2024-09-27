using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public string Name;

    public BoxCollider2D Bounds;

    public GameObject Contents;

    public Transform CameraAnchor;

    public Transform RespawnAnchor;

    public Level[] LevelsToDisable;

    public bool PreloadLevel;


    private void Awake()
    {
        Contents.SetActive(PreloadLevel);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (PreloadLevel) return;

        if (!collision.CompareTag("Player")) return;

        collision.GetComponent<PlayerMortality>().CurrentLevel = this;

        OnEnter();
    }

    public void OnEnter()
    {
        foreach (Level level in LevelsToDisable)
        {
            level.DisableLevel();
        }

        EnableLevel();

        FindObjectOfType<Camera>().transform.position = CameraAnchor.transform.position;
    }

    public void EnableLevel()
    {
        Contents.SetActive(true);
    }

    public void DisableLevel()
    {
        Contents.SetActive(false);
    }
}
