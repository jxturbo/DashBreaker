using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public GameObject indicator; // Reference to the GameObject you want to rotate
    public float flipAngleThreshold = 90f; // Set the threshold angle for flipping


    void Update()
    {
        RotateTowardsMouse();
    }

    void RotateTowardsMouse()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert the mouse position to a point in the world
        Vector3 worldMousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.z));

        // Calculate the direction from the gun to the mouse position
        Vector3 directionToMouse = worldMousePosition - transform.position;

        // Calculate the angle in degrees
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;

        // Rotate the indicator to face the mouse
        indicator.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    
}
