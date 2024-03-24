using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiff : MonoBehaviour
{

    public int spawnCount;
    public float intialHp;
    public int intialDmg;
    public bool first = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject DifficultyIncrease(GameObject enemy)
    {
        if (first == true)
        {
            intialDmg = enemy.GetComponent<EnemyBehaviour>().damageAmount;
            intialHp = enemy.GetComponent<Health>().maxHealth;
        }
        for (int i = 0; i < spawnCount; i++)
        {
            enemy.GetComponent<EnemyBehaviour>().damageAmount++;
            enemy.GetComponent<Health>().maxHealth++;
        }
        return enemy;
    }
    public void SpawnCountIncrease()
    {
        spawnCount++;
    }
}
