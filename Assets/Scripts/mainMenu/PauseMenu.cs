using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static bool isPaused;

    void Start()
    {
        isPaused = false;
    }
        // Update is called once per frame
        void Update()
    {
        if (!isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            transform.GetChild(1).gameObject.SetActive(true);
            isPaused = true;
        }
        else if(isPaused && Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 1;
            transform.GetChild(1).gameObject.SetActive(false);
            isPaused = false;
        }

        if (ScoreBoard.score < -20)
        {
            isPaused = true;
            if (isPaused)
            {
                Time.timeScale = 0;
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(true);
                isPaused = true;
            }
        }
    }
}
