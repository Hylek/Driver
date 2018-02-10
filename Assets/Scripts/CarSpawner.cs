using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour 
{
    bool isSpawning = false;
    public float minTime = 5.0f;
    public float maxTime = 15.0f;
    public GameObject[] enemies;

    private IEnumerator SpawnObject(int index, float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Instantiate(enemies[index], new Vector3(transform.position.x, transform.position.y, transform.position.z - 0.3f), transform.rotation);

        isSpawning = false;
    }

    private void Update()
    {
        if (!isSpawning)
        {
            isSpawning = true;
            int index = Random.Range(0, enemies.Length);
            StartCoroutine(SpawnObject(index, Random.Range(minTime, maxTime)));
        }
    }
}
