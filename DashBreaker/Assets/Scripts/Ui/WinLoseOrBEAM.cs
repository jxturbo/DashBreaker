using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinLoseOrBEAM : MonoBehaviour
{

    public GameObject WinLoseScreen;
    public TextMeshProUGUI WinLoseText;
    public PlayerController playerCtrl;
    public TextMeshProUGUI killcount;
    public AudioSource backgroundAudio;
    public AudioClip Lose; 

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
        if (killcount != null)
        {
            killcount.text = (playerCtrl.killcount).ToString();
        }
        backgroundAudio.clip = Lose;
        backgroundAudio.Play();
    }
}
