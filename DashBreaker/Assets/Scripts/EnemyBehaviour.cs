using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Pool;
using static UnityEngine.GraphicsBuffer;

public class EnemyBehaviour : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public int distance;
    public int moveSpeed;
    public float cooldown;
    public int damageAmount;
    public int enemyType;
    public bool check = true;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
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
            MoveToPlayer();
        }
        if (Vector2.Distance(transform.position, player.transform.position) <= distance && check == true)
        {
            check = false;
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
        check = true;
    }
    IEnumerator RushAttack()
    {
        moveSpeed += 2;
        MoveToPlayer();
        yield return new WaitForSeconds(cooldown);
        moveSpeed -= 2;
        check = true;
    }

    public void MoveToPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed);
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