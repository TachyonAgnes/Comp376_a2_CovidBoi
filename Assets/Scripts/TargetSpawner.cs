using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] target;
    [SerializeField] private GameObject[] mob;
    [SerializeField] private GameObject originalPoint;

    private GameObject newTarget;
    private GameObject newMob;
    private GameObject playerObj = null;
    private float randomXposition, randomYposition;
    private Vector3 spawnPosition;
    private Vector2 currentPosOnCenterLine;
    float mTimer;

    // Start is called before the first frame update
    void Start()
    {   
        InvokeRepeating("SpawnNewTarget", 5f, 1.0f);
        InvokeRepeating("SpawnNewMob", 10.0f, 15.0f);
        if (playerObj == null)
            playerObj = GameObject.Find("Avatar");
    }

    Vector2 currentPosition(GameObject gObj)
    {
        //calculate a normal vector pointing from origin and spliting road into 2;
        Vector2 centerLine = new Vector2(10.0f - originalPoint.transform.position.x, 
                                          10.0f - originalPoint.transform.position.y);
        centerLine = centerLine.normalized;
        //calculate a vector pointing from origin to the avatar
        Vector2 pointToPlayer = new Vector2(gObj.transform.position.x - originalPoint.transform.position.x,
                                            gObj.transform.position.y - originalPoint.transform.position.y);
        //magnitude of vector pointToPlayer
        float m_pointToPlayer = pointToPlayer.magnitude;
        //angle of vector pointToPlayer
        float angle = Vector2.Angle(centerLine, pointToPlayer);
        //lenth of projected vector d
        float d_length = m_pointToPlayer * Mathf.Cos(Mathf.Deg2Rad*angle);
        //return vector d
        return d_length * centerLine;
    }

    private void SpawnNewTarget()
    {

        mTimer += Time.deltaTime;
        if (mTimer >= Random.Range(0, 2))
        {
            mTimer = 0;
            randomXposition = currentPosOnCenterLine.x + 10f + Random.Range(-2, 2);
            randomYposition = currentPosOnCenterLine.y + 10f + Random.Range(-2, 2);
            spawnPosition = new Vector3(randomXposition, randomYposition, 0f);
            newTarget = Instantiate(target[Random.Range(0, target.Length)], spawnPosition, Quaternion.identity);
            Destroy(newTarget, 15);
        }
    }
    private void SpawnNewMob()
    {   
        if (Random.Range(0, 4 + UI.levelCounter) > 2)
        {
            randomXposition = currentPosOnCenterLine.x + 10f;
            randomYposition = currentPosOnCenterLine.y + 14f;
            spawnPosition = new Vector3(randomXposition, randomYposition, 0f);
            newMob = Instantiate(mob[Random.Range(0, mob.Length)], spawnPosition, Quaternion.identity);
            Destroy(newMob, 15);
        }
    }
    // Update is called once per frame

    void Update()
    {
        currentPosOnCenterLine = currentPosition(playerObj);
    }
}
