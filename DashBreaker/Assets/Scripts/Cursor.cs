using UnityEngine;

public class Cursor : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        // Call the method to update the cursor position
        UpdateCursorPosition();
    }

    void UpdateCursorPosition()
    {
        // Get the mouse position in screen coordinates
        Vector3 mousePosition = Input.mousePosition;

        // Convert the screen coordinates to world coordinatess
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Set the z-coordinate of the world position to match the transform's z-coordinate
        worldPosition.z = transform.position.z;

        // Set the position of the transform to the world position
        transform.position = worldPosition;
    }
}
