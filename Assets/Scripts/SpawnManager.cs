using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject powerUpPrefab;
    private float spawnRange = 8.5f;
    private int enemyCount;
    private int waveNumber = 1;
    // Start is called before the first frame update
    void Start()
    {
        SpawnWave(waveNumber);
    }
    private void Update()
    {
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if(enemyCount == 0)
        {
            waveNumber++;
            SpawnWave(waveNumber);
        }
    }

    void SpawnWave(int enemyNum)
    {
        Instantiate(powerUpPrefab, GenerateSpawnPosition(), powerUpPrefab.transform.rotation);
        for (int i=0; i< enemyNum; i++)
        {
            Instantiate(enemyPrefab, GenerateSpawnPosition(), enemyPrefab.transform.rotation);
        }
    }

    // Update is called once per frame
    Vector3 GenerateSpawnPosition()
    {
        float xPos = Random.Range(-spawnRange, spawnRange);
        float zPos = Random.Range(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(xPos, enemyPrefab.transform.position.y, zPos);
        return spawnPos;
    }
}
