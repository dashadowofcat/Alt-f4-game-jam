using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIconsManager : MonoBehaviour
{
    List<GameObject> SpawnedIcons = new List<GameObject>();

    public List<GameObject> icons = new List<GameObject>();


    




    void Update()
    {
        Invoke("SpwanIcons",5f);
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);

            print("poyo");

            GameObject IconToSpawn = icons[Random.Range(0, icons.Count)];

            SpawnedIcons.Add(Instantiate(IconToSpawn));
        }
    }
}
