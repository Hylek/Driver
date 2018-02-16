using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class CarController : MonoBehaviour 
{
	// Main Variables
	public GameObject carPrefab;
	public List<GameObject> cars;
	private int lane = 0;
	
	// Spawning Variables
	private bool isSpawning = false;
	public float minTime = 1.0f;
	public float maxTime = 5.0f;
	
	// Location points for each lane on the x axis
	private float laneOne = -2.15f;
	private float laneTwo = -0.53f;
	private float laneThree = 1.18f;
	private float laneFour = 2.86f;
	
	private void Start () 
	{
		
	}
	
	private void Update () 
	{
		Spawn();

		for (int i = 0; i < cars.Count; i++)
		{
			if (this == cars[i]) continue;

			int currentLane = CheckLanes();
		}
	}

	private int CheckLanes()
	{	
		for (int i = 0; i < cars.Count; i++)
		{
			if (this == cars[i]) continue;

			if (Mathf.Abs(cars[i].transform.position.x - laneOne) < 0.1)
			{
				lane = 1;
			}
			if (Mathf.Abs(cars[i].transform.position.x - laneTwo) < 0.1)
			{
				lane = 2;
			}
			if (Mathf.Abs(cars[i].transform.position.x - laneThree) < 0.1)
			{
				lane = 3;
			}
			if (Mathf.Abs(cars[i].transform.position.x - laneFour) < 0.1)
			{
				lane = 4;
			}
			
		}
		return lane;
	}

	private void Spawn()
	{
		if (!isSpawning)
		{
			isSpawning = true;
			StartCoroutine(SpawnCar(Random.Range(minTime, maxTime)));
		}
	}
	
	private IEnumerator SpawnCar(float seconds)
	{
		yield return new WaitForSeconds(seconds);
		cars.Add(Instantiate(carPrefab, new Vector3(LanePicker(), transform.position.y, transform.position.z - 0.3f), transform.rotation));
		
		isSpawning = false;
	}
	
	private float LanePicker()
	{
		int random = Random.Range(1, 5);
		float chosenLane = 0;

		switch (random)
		{
			case 1: chosenLane = laneOne;
				break;
			case 2: chosenLane = laneTwo;
				break;
			case 3: chosenLane = laneThree;
				break;
			case 4: chosenLane = laneFour;
				break;	
		}
		return chosenLane;
	}
}
