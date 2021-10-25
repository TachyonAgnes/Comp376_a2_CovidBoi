using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBoard : MonoBehaviour
{
    [SerializeField] Text ScoreBoardText;
    public static int disinfected_count = 0;
    public static int miss_infected_count = 0;
    public static int one_shot_two_mask = 0;
    public static int social_distance = 0;
    public static int isGoodShot = 0;
    public static int isBadShot = 0;

    public static int score = 0;
    void Start()
    {
      disinfected_count = 0;
      miss_infected_count = 0;
      one_shot_two_mask = 0;
      social_distance = 0;
      isGoodShot = 0;
      isBadShot = 0;

      score = 0;
    }

    // Update is called once per frame
    void Update()
    {
        score = (disinfected_count != 0 ? disinfected_count * 2 : 0) +
                (miss_infected_count != 0 ? miss_infected_count * 1 : 0) +
                (one_shot_two_mask != 0 ? one_shot_two_mask * 2 : 0) +
                (social_distance != 0 ? social_distance * 5 : 0)+
                (isGoodShot != 0 ? isGoodShot * 1 : 0)-
                (isBadShot != 0 ? isBadShot * 2 : 0);
        ScoreBoardText.text = score.ToString();
    }
}
