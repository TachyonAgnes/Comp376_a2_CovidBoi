using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class infectionSpawner : MonoBehaviour
{
	public GameObject[] prefabs;//road prefabs
	float nextStepX;
	float nextStepY;
	Vector3 rotationVector = new Vector3(0, 0, 0);
	GameObject infection;
	void Start()
	{
		InvokeRepeating("createInfectionSurface", 0f, 3f);
	}

	private void createInfectionSurface()
	{
		nextStepY += Random.Range(-1, 1);
		nextStepX += Random.Range(-1, 1);
		if(Random.Range(0, 4)<1){
			infection = Instantiate(prefabs[Random.Range(0, prefabs.Length)],
					new Vector3(transform.position.x + nextStepX, transform.position.y + nextStepY, transform.position.z),
					Quaternion.Euler(rotationVector));
			Destroy(infection, 20.0f);
		}
    }
}
