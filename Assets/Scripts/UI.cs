using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI : MonoBehaviour
{
    [SerializeField] GameObject hightLight1;
    [SerializeField] GameObject hightLight2;
    [SerializeField] GameObject hightLight3;
    [SerializeField] GameObject hightLight4;
    [SerializeField] GameObject hightLight5_charged;
    [SerializeField] GameObject hightLight5_stdby;
    [SerializeField] Text TIME_HOUR;
    [SerializeField] Text TIME_MIN;
    [SerializeField] Text TIME_SEC;
    [SerializeField] Text level;

    public static float timer = 0.0f; 
    public static int levelCounter = 1;
    public static int levelAdder = 0;

    void Start()
    {
        timer = 0.0f;
    }
        void Update()
    {
        timer += Time.deltaTime;
        TIME_HOUR.text = ((int)(timer / 3600)).ToString();
        TIME_MIN.text = ((int)((timer % 3600)/60)).ToString();
        TIME_SEC.text = ((int)((timer % 3600) % 60)).ToString();
        levelCounter = (int)(timer / 50)+ levelAdder;
        level.text = levelCounter.ToString();

       
        if (!PauseMenu.isPaused && Input.GetKeyDown(KeyCode.Alpha1))
        {
            hightLight1.SetActive(true);
            hightLight2.SetActive(false);
            hightLight3.SetActive(false);
            hightLight4.SetActive(false);
            hightLight5_stdby.SetActive(false);
        }
        if (!PauseMenu.isPaused && Input.GetKeyDown(KeyCode.Alpha2))
        {
            hightLight1.SetActive(false);
            hightLight2.SetActive(true);
            hightLight3.SetActive(false);
            hightLight4.SetActive(false);
            hightLight5_stdby.SetActive(false);
        }
        if (!PauseMenu.isPaused && Input.GetKeyDown(KeyCode.Alpha3))
        {
            hightLight1.SetActive(false);
            hightLight2.SetActive(false);
            hightLight3.SetActive(true);
            hightLight4.SetActive(false);
            hightLight5_stdby.SetActive(false);
        }
        if (!PauseMenu.isPaused && Input.GetKeyDown(KeyCode.Alpha4))
        {
            hightLight1.SetActive(false);
            hightLight2.SetActive(false);
            hightLight3.SetActive(false);
            hightLight4.SetActive(true);
            hightLight5_stdby.SetActive(false);
        }
        if (!PauseMenu.isPaused && Input.GetKeyDown(KeyCode.Alpha5))
        {
            hightLight1.SetActive(false);
            hightLight2.SetActive(false);
            hightLight3.SetActive(false);
            hightLight4.SetActive(false);
            hightLight5_stdby.SetActive(true);
        }
        if (UsingWeapon.isPortalCharged)
        {
            hightLight5_charged.SetActive(true);
        }
        else if (!UsingWeapon.isPortalCharged)
        {
            hightLight5_charged.SetActive(false);
        }
    }
}
