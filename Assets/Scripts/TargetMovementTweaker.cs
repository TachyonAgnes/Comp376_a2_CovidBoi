using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMovementTweaker : MonoBehaviour
{
    private float movementSpeed = 0.4f;
    private Vector3 direction = new Vector3(-1.0f, -1.0f, 0.0f);

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SetRandomSpeed", 0, 0.2f);
        InvokeRepeating("SetRandomDir", 0, 0.2f);
    }
    float SetRandomSpeed()
    {
        return movementSpeed * (Random.Range(1.0f, 10.0f + UI.levelCounter));
    }
    Vector3 SetRandomDir()
    {
        Vector3 temp = new Vector3(-Mathf.Sin(Time.time)/1.5f, 0.0f, 0.0f);
        Vector3 ran = new Vector3(Random.Range(-0.05f,0.05f), Random.Range(-0.05f, 0.05f), 0.0f) ;
        temp = Quaternion.AngleAxis(-45, Vector3.up) * temp;
        return direction + temp + ran;
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Translate(SetRandomDir() * SetRandomSpeed() * Time.deltaTime);
    }
}
