using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Level : MonoBehaviour
{
    public string Name;

    public BoxCollider2D Bounds;

    public GameObject Contents;

    public Transform CameraAnchor;

    public Transform RespawnAnchor;

    public Transform WallAnchor;

    public Level[] LevelsToDisable;

    public bool PreloadLevel;

    public bool FinalLevel;


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

        AnchorWall();

        foreach (Level level in LevelsToDisable)
        {
            level.DisableLevel();
        }

        EnableLevel();

        FindObjectOfType<Camera>().transform.position = CameraAnchor.transform.position;

        if(FinalLevel)
        {
            FindObjectsOfType<GlitchWall>().Where(G => G.name == "glitch wall").FirstOrDefault().gameObject.SetActive(false);
        }
    }

    public void EnableLevel()
    {
        Contents.SetActive(true);
    }

    public void DisableLevel()
    {
        Contents.SetActive(false);
    }

    public void AnchorWall()
    {
        FindObjectOfType<GlitchWall>().transform.position = WallAnchor.transform.position;
    }
}
