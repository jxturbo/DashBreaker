using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CountUpTimer : MonoBehaviour
{
    public TextMeshProUGUI timerText;
    private float elapsedTime;

    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateTimer();
    }

    void UpdateTimer()
    {
        // Increment the elapsed time
        elapsedTime += Time.deltaTime;
        // Format the time as minutes and seconds
        string minutes = Mathf.Floor(elapsedTime / 60).ToString("00");
        string seconds = (elapsedTime % 60).ToString("00");
        // Update the TextMeshPro text
        timerText.text = string.Format("{0} min {1} sec", minutes, seconds);
    }
}
