using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform player;
    public Vector2 hold;
    public int moveSpeed;
    public int distance;
    public int expiry;
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
        if (Vector2.Distance(transform.position, player.position) <= distance)
        {
            gameObject.SetActive(false);
            check = false;
            //TakeDamage()
        }
        StartCoroutine(MissedBullet());
    }

    IEnumerator MissedBullet()
    {
        yield return new WaitForSeconds(expiry);
        gameObject.SetActive(false);
        check = false;
    }
}
