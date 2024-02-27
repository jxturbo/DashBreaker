using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTracker : MonoBehaviour
{
    public Transform player;  
    public float lerpSpeed = 10f;  

    // LateUpdate is called once per frame, after all Update calls
    void LateUpdate()
    {
        if (player != null)
        {
            // Calculate the target position with a slight offset in the z-axis
            Vector3 targetPosition = new Vector3(player.position.x, player.position.y, transform.position.z);
            // Linearly interpolate the camera position towards the target position using a smoothed deltaTime
            float smoothDeltaTime = Mathf.Clamp01(lerpSpeed * Time.deltaTime);
            transform.position = Vector3.Lerp(transform.position, targetPosition, smoothDeltaTime);
        }
    }
}
