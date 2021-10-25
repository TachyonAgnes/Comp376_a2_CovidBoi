using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objManager : MonoBehaviour
{
	public GameObject[] prefabs;//road prefabs
	public Transform startPos;
	float nextStepX;
	float nextStepY;
	Vector3 rotationVector = new Vector3(0, 0, 0);
	void Start()
	{
		InvokeRepeating("instObj", 0f, 1.0f);
	}

	// Update is called once per frame
	private void instObj()
	{
			nextStepY += 15f;
			nextStepX += 15f;
			Instantiate(prefabs[Random.Range(0, prefabs.Length)], 
				new Vector3(startPos.position.x + nextStepX, startPos.position.y + nextStepY, startPos.position.z), 
				Quaternion.Euler(rotationVector));

	}
}
