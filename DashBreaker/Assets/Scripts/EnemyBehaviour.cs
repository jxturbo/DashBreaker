using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player;
    public int distance;
    public int moveSpeed;
    public float cooldown;
    public int damageAmount;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        HeadToPlayer();
    }

    void HeadToPlayer()
    {
        if (Vector2.Distance(transform.position, player.position) >= distance)
        {
            transform.position = Vector2.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.position) <= distance)
            {
                StartCoroutine(AttackPlayer());
            }
        }
    }

     IEnumerator AttackPlayer()
     {
         Debug.Log("Attacking");
         GameObject bullet = ObjectPool.objPool.GetPooledObject();
         if (bullet != null)
         {
             bullet.transform.position = transform.position;
             bullet.transform.rotation = transform.rotation;
             bullet.SetActive(true);
         }
         yield return new WaitForSeconds(cooldown);
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
