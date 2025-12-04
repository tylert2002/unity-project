using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    private bool gameOverTriggered = false;
    // Update is called once per frame
    void Update()
    {
        if (gameOverTriggered) return; 

        remainingTime -= Time.deltaTime;
        remainingTime = Mathf.Max(remainingTime, 0);

        int minutes = Mathf.FloorToInt(remainingTime / 60);
        int seconds = Mathf.FloorToInt(remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (remainingTime <= 0 && !gameOverTriggered)
        {
            gameOverTriggered = true;
            Destroy(FindObjectOfType<PlayerControllerLV4>().gameObject);
            GameManagerLV4.instance.gameOver();
        }
    }
}
