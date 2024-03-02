using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform player;
    public Vector2 hold;
    public int moveSpeed;
    public int expiry;
    public int damageAmount;
    public bool check = false;

    // Start is called before the first frame update
    void Start()
    {
        
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
        transform.position = Vector2.Lerp(transform.position, hold, moveSpeed * Time.deltaTime);
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
