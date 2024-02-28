using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Transform player;
    public Vector2 hold;
    public int moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        hold = player.position;
    }

    // Update is called once per frame
    void Update()
    {
        FireAtPlayer();
    }

    void FireAtPlayer()
    {
        transform.position = Vector2.Lerp(transform.position, hold, moveSpeed * Time.deltaTime);
        //Place on collision and destroy code here.
        //Use gameobject.SetActive(false); instead of Destroy(gameObject)
    }
}
