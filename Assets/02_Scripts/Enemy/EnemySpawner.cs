using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public float spawnTime = 1.0f;

    public GameObject spawnPrefab;

    Vector3 spawnPos = new Vector3(0, 0.4f, 0);
    private void Start()
    {
        //spawnTrsnsorm = transform.GetChild(0);
        StartCoroutine(EnemySpawn());
    }

    IEnumerator EnemySpawn()
    {
        while (true)
        {
            Instantiate(spawnPrefab, transform.position + spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(spawnTime);
        }
    }
}
