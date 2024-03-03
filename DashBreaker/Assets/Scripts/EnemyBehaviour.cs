using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public int distance;
    public int moveSpeed;
    public float cooldown;
    public int damageAmount;
    public int enemyType;
    


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        HeadToPlayer();
    }

    void HeadToPlayer()
    {
        if (Vector2.Distance(transform.position, player.transform.position) >= distance)
        {
            transform.position = Vector2.Lerp(transform.position, player.transform.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.transform.position) <= distance)
            {
                switch (enemyType)
                {
                    case 0:
                        StartCoroutine(ShootAttack());
                        break;
                    case 1:
                        StartCoroutine(RushAttack());
                        break;
                }
            }
        }
    }

     IEnumerator ShootAttack()
     {
         GameObject bullet = ObjectPool.objPool.GetPooledObject();
         if (bullet != null)
         {
             bullet.transform.position = transform.position;
             bullet.transform.rotation = transform.rotation;
             bullet.SetActive(true);
         }
         yield return new WaitForSeconds(cooldown);
     }
    IEnumerator RushAttack()
    {
        moveSpeed += 20;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, moveSpeed * Time.deltaTime);
        yield return new WaitForSeconds(cooldown);
        moveSpeed -= 20;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            PlayerController player = other.GetComponent<PlayerController>();
            if (damageable != null && !player.isInvincible)
            {
                damageable.TakeDamage(damageAmount);
            }
        }
    }
}
