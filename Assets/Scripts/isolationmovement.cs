using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class isolationmovement : MonoBehaviour
{
    private float movementSpeed = 2;
    private GameObject[] isolatedArr;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = new Vector3(-1.0f, 1.0f, 0.0f);
        isolatedArr = GameObject.FindGameObjectsWithTag("isolated");
        foreach (GameObject o in isolatedArr)
        {
            o.transform.Translate(direction * movementSpeed * Time.deltaTime);
        }
    }
}
