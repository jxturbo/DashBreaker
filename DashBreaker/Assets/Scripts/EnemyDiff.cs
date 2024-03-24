using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDiff : MonoBehaviour
{

    public int spawnCount;
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
 
        if (enemy.tag == "Bullet")
        {
            if (first == true)
            {
                intialDmg = enemy.GetComponent<Bullet>().damageAmount;
            }
            for (int i = 0; i < spawnCount; i++)
            {
                enemy.GetComponent<Bullet>().damageAmount++;
            }
        }
        else if (enemy.tag == "Enemy")
        {
            for (int i = 0; i < spawnCount; i++)
            {
                enemy.GetComponent<EnemyBehaviour>().damageAmount++;
                enemy.GetComponent<Health>().maxHealth++;
            }
        }

        return enemy;
    }
    public void SpawnCountIncrease()
    {
        spawnCount++;
    }

    public GameObject BulletReset(GameObject bullet)
    {
        bullet.GetComponent<Bullet>().damageAmount = intialDmg;
        return (bullet);
    }
}
