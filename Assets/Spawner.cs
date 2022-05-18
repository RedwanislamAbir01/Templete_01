using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnObject;
    public Vector3 spawnPoint;
    public int maxX = 10;
    public int timeTilNextSpawn = 5;
    float z = 0, y = 0;
    float timer = 0;

    void Start()
    {
        timer = 0;
        spawnPoint.z = z; spawnPoint.y = y;
    }

    private void Update()
    {
        timer += Time.deltaTime;
        Spawn();
    }

    void Spawn()
    {
        if (timer >= timeTilNextSpawn)
        {
            z = Random.Range(6.1f, 7.1f);
            spawnPoint.z = z;
            
            y = Random.Range(.6f, .18f);
            spawnPoint.y = y;
            Instantiate(spawnObject, spawnPoint, Quaternion.identity);
            timer = 0;
        }
    }
}
