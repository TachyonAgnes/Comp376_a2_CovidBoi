using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class bullettime : MonoBehaviour
{
    [SerializeField] GameObject hightLightShift;
    [SerializeField] Text countDown;
    public static float bulletTimeTimer = 4.0f;
    public static bool isRechargeTime = false;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        UI.levelAdder = 3;
        countDown.text = ((int)bulletTimeTimer).ToString();
        if (!PauseMenu.isPaused && Input.GetKeyDown(KeyCode.LeftShift))
        {
            hightLightShift.SetActive(true);
            Target.isBulletTime = true;
            isRechargeTime = false;
            Time.timeScale = 0.2f;
        }
        if (Target.isBulletTime && bulletTimeTimer > 0)
        {
            bulletTimeTimer -= Time.deltaTime;
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                hightLightShift.SetActive(false);
                Target.isBulletTime = false;
                isRechargeTime = true;
                Time.timeScale = 1.0f;
            }
        }
        if (bulletTimeTimer < 0)
        {
            hightLightShift.SetActive(false);
            Target.isBulletTime = false;
            isRechargeTime = true;
            Time.timeScale = 1.0f;
        }
        if (isRechargeTime) bulletTimeTimer += Time.deltaTime;
        if(bulletTimeTimer > 4)
        {
            isRechargeTime = false;
        }
        //Update score:
        if (Target.isBulletTime)
        {
            ScoreBoard.isGoodShot = Target.perfectCase;
            ScoreBoard.isBadShot = Target.badCase;
        }
    }
}
