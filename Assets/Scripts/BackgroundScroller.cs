﻿using UnityEngine;
/* Daniel Cumbor 2018 */
public class BackgroundScroller : MonoBehaviour
{
	public GameObject[] tiles;
	public Vector3 startPos;
	public Vector3 endPos;
	
	void Start ()
	{
		startPos = tiles[1].transform.position;
	}
	
	void FixedUpdate () 
	{
		for (int i = 0; i < tiles.Length; i++)
		{
			if (tiles[i].transform.position.y < endPos.y)
			{
				tiles[i].transform.position = startPos;
			}
			else
			{
				tiles[i].transform.Translate(Vector2.down * StateManager.manager.speed * Time.deltaTime);
            }
		}
	}
}
