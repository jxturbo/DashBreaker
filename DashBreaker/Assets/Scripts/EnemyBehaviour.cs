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
    public GameObject gameController;
    public Rigidbody2D rb;
    public int distance;
    public int moveSpeed;
    public float cooldown;
    public int damageAmount;
    public int enemyType;
    public bool check = true;
    public int attackNumber;
    public int attackType;



    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        gameController = GameObject.FindGameObjectWithTag("Controller");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!GlobalVariableHolder.timePaused)
        {
            HeadToPlayer();
        }
        else
        {
            rb.velocity = new Vector2(0f, 0f);
        }
        
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
                case 2:
                    StartCoroutine(BossAttacking());
                    break;
            }
        }
    }

    IEnumerator ShootAttack()
    {
        GameObject bullet = ObjectPool.objPool.GetPooledObject();
        if (bullet != null)
        {
            Debug.Log(transform.position);
            bullet.transform.position = transform.position;
            gameController.GetComponent<EnemyDiff>().DifficultyIncrease(bullet);
            Debug.Log(bullet.transform.position + " bullet");
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

    IEnumerator ShootAttackBoss()
    {
        GameObject bullet = ObjectPool.objPool.GetPooledObject();
        if (bullet != null)
        {
            Debug.Log(transform.position);
            bullet.transform.position = transform.position;
            Debug.Log(bullet.transform.position + " bullet");
            bullet.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        bullet = ObjectPool.objPool.GetPooledObject();
        if (bullet != null)
        {
            Debug.Log(transform.position);
            bullet.transform.position = transform.position;
            Debug.Log(bullet.transform.position + " bullet");
            bullet.SetActive(true);
        }
        yield return new WaitForSeconds(1);
        bullet = ObjectPool.objPool.GetPooledObject();
        if (bullet != null)
        {
            Debug.Log(transform.position);
            bullet.transform.position = transform.position;
            Debug.Log(bullet.transform.position + " bullet");
            bullet.SetActive(true);
        }
        yield return new WaitForSeconds(cooldown);
    }
    IEnumerator RushAttackBoss()
    {
        moveSpeed += 3;
        MoveToPlayer();
        yield return new WaitForSeconds(1);
        MoveToPlayer();
        yield return new WaitForSeconds(cooldown);
        moveSpeed -= 3;
    }

    IEnumerator BossAttacking()
    {
        attackType = Random.Range(0, 2);
        switch (attackType)
        {
            case 0:
                StartCoroutine(ShootAttackBoss());
                break;
            case 1:
                StartCoroutine(RushAttackBoss());
                break;
        }
        yield return new WaitForSeconds(cooldown);
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