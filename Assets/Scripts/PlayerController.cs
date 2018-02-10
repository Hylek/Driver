﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

	void Start () 
    {
		
	}
	
	void Update () 
    {
		
	}

    void FixedUpdate()
    {
        if (Input.touchCount > 0)
        {
            foreach (Touch touch in Input.touches)
            {
                if (touch.phase == TouchPhase.Began && touch.position.x < Screen.width / 2)
                {

                }
                else if (touch.phase == TouchPhase.Stationary && touch.position.x < Screen.width / 2)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed);
                }
                else if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2)
                {
                }
                else if (touch.phase == TouchPhase.Stationary && touch.position.x > Screen.width / 2)
                {
                    GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    //transform.Translate(Vector2.zero);
                }
            }
        }

        if(Input.GetKey(KeyCode.A))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed);
        }
        if(Input.GetKey(KeyCode.D))
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
        }
    }
}
