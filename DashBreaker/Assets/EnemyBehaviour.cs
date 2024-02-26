using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public Transform player;
    public int distance;
    public int moveSpeed;
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
            //Debug.Log("Checking");

            transform.position = Vector3.Lerp(transform.position, player.position, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, player.position) <= distance)
            {
                AttackPlayer();
            }
        }
    }

    void AttackPlayer()
    {
        Debug.Log("Attacking");
    }
}
