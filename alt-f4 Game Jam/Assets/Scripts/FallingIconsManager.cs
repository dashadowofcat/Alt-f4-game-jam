using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingIconsManager : MonoBehaviour
{
    List<GameObject> SpawnedIcons = new List<GameObject>();

    public List<GameObject> icons = new List<GameObject>();


    public float minYtoDestroy;


    public float timeUntilSpawn;


    public float spinSpeed;

    public float fallSpeed;



    void Update()
    {
        foreach (GameObject icon in SpawnedIcons)
        {
            icon.transform.position += new Vector3(0, -fallSpeed) * Time.deltaTime;

            icon.transform.eulerAngles += new Vector3(0, 0, spinSpeed);

            if (icon.transform.position.y <= minYtoDestroy)
            {
                SpawnedIcons.Remove(icon);
                Destroy(icon.gameObject);
            }
        }
    }

    IEnumerator Start()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeUntilSpawn);

            float randomino = Random.Range(-8, 8);

            GameObject IconToSpawn = icons[Random.Range(0, icons.Count)];

            SpawnedIcons.Add(Instantiate(IconToSpawn,transform.position = new Vector2(randomino,transform.position.y),Quaternion.identity));
        }
    }
}
