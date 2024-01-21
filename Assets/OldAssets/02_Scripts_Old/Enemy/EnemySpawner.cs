using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EnemySpawner : MonoBehaviour
{
    public enum SpawnPattern
    {
        Single,
        Double,
        Triple,
        // Add more patterns as needed
    }

    public float startDelay = 0.0f;

    [Header("SpawnTimes")]
    public float defaultSpawnTime = 1.0f;
    public float singleSpawnTime = 1.0f;
    public float DoubleSpawnTime = 0.5f;
    public float quadraSpawnTime = 0.25f;

    [Header("Any property")]
    public int patternCount = 0;
    bool onStartDelay = true;

    public GameObject spawnPrefab;

    Vector3 spawnPos = new Vector3(0, 0.4f, 0);
    private void Start()
    {
        //spawnTrsnsorm = transform.GetChild(0);
        StartCoroutine(EnemySpawn());
    }

    ///while (true)
    ///{
    ///    Instantiate(spawnPrefab, transform.position + spawnPos, Quaternion.identity);
    ///    yield return new WaitForSeconds(spawnTime);
    ///}
    IEnumerator EnemySpawn()
    {
        if (onStartDelay)   //시작 딜레이 한 번만 기다리고 스폰 시작
        {
            yield return new WaitForSeconds(startDelay);
            onStartDelay = false;
        }
        while (true)
        {
            if (patternCount < 2)        //패턴 카운트가 2보다 작을 때
            {
                Instantiate(spawnPrefab, transform.position + spawnPos, Quaternion.identity);
                yield return new WaitForSeconds(defaultSpawnTime);
                patternCount++;
            }
            else if(patternCount >= 2 &&patternCount < 4)   // 카운트가 2보다 크로 4보다작을 때 패턴을 생성하지 않고 기다림
            {
                patternCount++;
                yield return new WaitForSeconds(defaultSpawnTime);
            }
            else        //카운트가 4보다 커지면 패턴 다시 시작
            {
                patternCount = 0;
            }

        }

    }

    IEnumerator SinglePattern()
    {
        yield return new WaitForSeconds(startDelay);
    }    
    IEnumerator DoublePattern()
    {
        yield return new WaitForSeconds(startDelay);
    }    
    IEnumerator QuadraPattern()
    {
        yield return new WaitForSeconds(startDelay);
    }
}
