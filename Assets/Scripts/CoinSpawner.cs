using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CoinSpawner : MonoBehaviour
{
    public GameObject itemPrefab;
    public Transform[] spawnPoints;

    public int tokenTotal = 0;
    public static CoinSpawner instance;
    private void Awake()
    {
        instance = this;
        foreach (Transform t in spawnPoints)
        {
            SpawnToken(t);
            tokenTotal++;
        }
    }

    private void Update()
    {

    }

    void SpawnToken(Transform t)
    {
        Instantiate(itemPrefab, t.position, Quaternion.identity);
    }
}
