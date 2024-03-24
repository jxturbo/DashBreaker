using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    public int spawnNumber;
    public int randomNumber;
    public int randomType;
    public int cooldown;
    public int spawnCount;
    public GameObject spawner;
    public List<GameObject> spawnList;
    public List<GameObject> enemyList;
    public GameObject enemy;
    public GameObject enemyInstance;
    public EnemyBehaviour enemyBehaviour;
    public EnemyDiff enemyDiff;
    public GameObject gameController;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SpawnEnemy", 0f);
    }

    void SpawnEnemy()
    {
        GameObject[] tmpList;
        tmpList = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (GameObject tmp in tmpList)
        {
            spawnList.Add(tmp);
        }
        StartCoroutine(RandomSpawn());
    }
    IEnumerator RandomSpawn()
    {
        for (int i = 0; i < spawnNumber; i++)
        {
            cooldown = Random.Range(0, 3);
            randomNumber = Random.Range(0, spawnList.Count);
            randomType = Random.Range(0, 1);
            spawner = spawnList[randomNumber];
            enemy = enemyList[randomType];
            enemyInstance = Instantiate(enemy, spawner.transform.position, spawner.transform.rotation);
            gameController.GetComponent<EnemyDiff>().DifficultyIncrease(enemyInstance);
            enemyInstance.SetActive(true);
            yield return new WaitForSeconds(cooldown);
        }
        gameController.GetComponent<EnemyDiff>().SpawnCountIncrease();
    }
}
