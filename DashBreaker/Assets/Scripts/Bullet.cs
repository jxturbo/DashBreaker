using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject player;
    public Rigidbody2D rb;
    public int moveSpeed;
    public int expiry;
    public int damageAmount;
    public bool check = false;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (check == false)
        {
            check = true;
            FireAtPlayer();
        }
    }

    void FireAtPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        rb.AddForce(direction * rb.mass * moveSpeed, ForceMode2D.Impulse);
        StartCoroutine(MissedBullet());
    }

    IEnumerator MissedBullet()
    {
        yield return new WaitForSeconds(expiry);
        gameObject.SetActive(false);
        check = false;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            IDamageable damageable = other.GetComponent<IDamageable>();
            PlayerController player = other.GetComponent<PlayerController>();
            if (damageable != null)
            {
                Debug.Log("Dmg");
                if(!player.isInvincible)
                {
                    damageable.TakeDamage(damageAmount);
                }
                gameObject.SetActive(false);
                check = false;
                return;
            }
        }
    }
}