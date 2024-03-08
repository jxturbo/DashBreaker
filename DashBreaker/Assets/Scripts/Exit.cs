using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour
{
    public WinLoseOrBEAM winloseOrBeam;
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            Debug.Log("End Reached!");
            winloseOrBeam.WinSpot();
        }
    }
}
