using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infectedSurface : MonoBehaviour
{
    [SerializeField] GameObject mSwapPrefab;
    GameObject clone = null;
    float timeLeft = 10.0f;

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "water")
        {
            Destroy(col.gameObject, 0.1f);
            if (gameObject.name.Contains("infected_Surface"))
            {
                clone = Instantiate(mSwapPrefab, transform.position, Quaternion.identity);
                Destroy(gameObject);
                ScoreBoard.disinfected_count++;
            }
            Destroy(clone, 0.5f);
        }
    }

    private void Update()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft < 0)
        {
            ScoreBoard.miss_infected_count--;
            Destroy(gameObject);
        }
    }
}