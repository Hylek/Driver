using System.Collections;
using UnityEngine;
/* Daniel Cumbor 2018 */
public class CarController : MonoBehaviour 
{
	// Main Variables
	public GameObject carPrefab;
	
	// Spawning Variables
	private bool isSpawning = false;
	public float minTime = 1.0f;
	public float maxTime = 5.0f;

    // Location points for each lane on the x axis
    private float laneOne = -2.4f;
    private float laneTwo = -0.8f;
    private float laneThree = 0.95f;
    private float laneFour = 2.6f;

    private void Update () 
	{
		Spawn();
        Intervals();
	}

    private void Intervals()
    {
        if(StateManager.manager.t >= 30 && StateManager.manager.t <= 60)
        {
            minTime = 3.5f;
            maxTime = 8f;
        }
        if (StateManager.manager.t >= 60 && StateManager.manager.t <= 120)
        {
            minTime = 4f;
            maxTime = 6f;
        }
        if (StateManager.manager.t >= 120)
        {
            minTime = 2f;
            maxTime = 4f;
        }
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
		Instantiate(carPrefab, new Vector3(LanePicker(), transform.position.y + 4, transform.position.z - 0.3f), transform.rotation);
		
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
