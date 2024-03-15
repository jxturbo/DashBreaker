using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DmgNumberBehaviour : MonoBehaviour
{
    private Vector3 spawnPosition;

    // Store the world position when spawned
    private void Start()
    {
        spawnPosition = transform.position;
    }

    // Ensure the object stays at its spawn position
    private void Update()
    {
        transform.position = spawnPosition;
    }

    // Method to destroy the GameObject
    public void DestroyObject()
    {
        Destroy(gameObject);
    }
}
