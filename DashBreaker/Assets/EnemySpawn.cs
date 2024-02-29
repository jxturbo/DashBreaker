using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public int spawnNumber;
    public int randomNumber;
    public GameObject spawner;
    public List<GameObject> spawnList;
    public GameObject enemy;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy", 2.0f);
    }

    void SpawnEnemy()
    {
        GameObject[] tmpList;
        tmpList = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (GameObject tmp in tmpList)
        {
            spawnList.Add(tmp);
        }
        spawnNumber = spawnList.Count;
        for (int i = 0; i < spawnNumber; i++)
        {
            randomNumber = Random.Range(0, spawnList.Count);
            spawner = spawnList[randomNumber];
            Instantiate(spawner);
            Instantiate(enemy, spawner.transform.position, spawner.transform.rotation);
            enemy.SetActive(true);
        }
    }
}
