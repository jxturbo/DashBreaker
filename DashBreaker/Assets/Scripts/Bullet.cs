using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rb;
    public Vector2 hold;
    public int moveSpeed;
    public int expiry;
    public int damageAmount;
    public bool check = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (check == false)
        {
            hold = player.position;
            check = true;
        }
        FireAtPlayer();
    }

    void FireAtPlayer()
    {
        Vector2 direction = player.transform.position - transform.position;
        direction.Normalize();
        rb.velocity = new Vector2(direction.x * moveSpeed, direction.y * moveSpeed); 
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