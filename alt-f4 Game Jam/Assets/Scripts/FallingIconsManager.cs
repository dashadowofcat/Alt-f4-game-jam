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
            if (icon == null) continue;

            icon.transform.position += new Vector3(0, -fallSpeed) * Time.deltaTime;

            icon.transform.eulerAngles += new Vector3(0, 0, spinSpeed);

            if (icon.transform.position.y <= minYtoDestroy)
            {
                Destroy(icon.gameObject);
            }
        }


    }

    private void OnEnable()
    {
        StartCoroutine(SpawnIcons());
    }

    private void OnDisable()
    {
        SpawnedIcons.Clear();

        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }


    IEnumerator SpawnIcons()
    {
        while (true)
        {
            float pos = Random.Range(-8, 8);

            GameObject IconToSpawn = icons[Random.Range(0, icons.Count)];

            GameObject icon = Instantiate(IconToSpawn, new Vector2(transform.position.x + pos,transform.position.y),Quaternion.identity);

            SpawnedIcons.Add(icon);

            icon.transform.parent = transform;

            icon.GetComponent<SpriteRenderer>().sortingOrder += Random.Range(-3, 3);

            yield return new WaitForSeconds(timeUntilSpawn);
        }
    }
}
