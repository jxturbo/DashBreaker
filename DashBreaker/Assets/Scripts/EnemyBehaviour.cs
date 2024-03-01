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
            if (damageable != null)
            {
                damageable.TakeDamage(damageAmount);
            }
        }
    }
}
