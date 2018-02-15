using System.Collections;
using UnityEngine;
/* Daniel Cumbor 2018 */
public class CarSpawner : MonoBehaviour 
{
    bool isSpawning = false;
    public float minTime = 5.0f;
    public float maxTime = 15.0f;
    public GameObject car;

    private IEnumerator SpawnObject(float seconds)
    {
		yield return new WaitForSeconds(seconds);
		Instantiate(car, new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.3f), transform.rotation);
		
		isSpawning = false;
    }

    private void Update()
    {
		if (!isSpawning)
        {
            isSpawning = true;
            StartCoroutine(SpawnObject(Random.Range(minTime, maxTime)));
        }

		if (StateManager.manager.gameOver) 
		{
			Destroy (gameObject);
		}
    }
}
