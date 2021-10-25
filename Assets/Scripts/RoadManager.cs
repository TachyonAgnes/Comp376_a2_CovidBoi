using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadManager : MonoBehaviour {

	public GameObject[] prefabs;//road prefabs
	public Transform startPos;
	float nextStepX;
	float nextStepY;
	Vector3 rotationVector = new Vector3(0, 0, 45);
	void Start()
	{
		InvokeRepeating("instRoad", 0f, 3.0f);
	}

	private void instRoad()
    {
		nextStepY += 44.1f;
		nextStepX += 44.1f;
		Instantiate(prefabs[Random.Range(0, prefabs.Length)], 
				new Vector3(startPos.position.x + nextStepX, startPos.position.y + nextStepY, startPos.position.z),
				Quaternion.Euler(rotationVector));
    }


}
