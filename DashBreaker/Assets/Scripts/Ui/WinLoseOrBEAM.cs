using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinLoseOrBEAM : MonoBehaviour
{

    public GameObject WinLoseScreen;
    public TextMeshProUGUI WinLoseText; 

    // Start is called before the first frame update
    void Start()
    {
        WinLoseText = WinLoseScreen.GetComponentInChildren<TextMeshProUGUI>();
    }

    public void WinSpot()
    {
        Time.timeScale = 0f;
        Debug.Log("You win!");
        WinLoseScreen.SetActive(true);
        WinLoseText.text = "You Win!";
    }
    public void Die()
    {
        Time.timeScale = 0f;
        WinLoseScreen.SetActive(true);
        WinLoseText.text = "Game Over!";
    }
}
