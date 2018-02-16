using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMover : MonoBehaviour
{
	public float speed;
	private bool slowDown;
	private float otherCarSpeed;

	private void Start()
	{
		speed = Random.Range(0.25f, 1.0f);
		slowDown = false;
	}

	private void Update()
	{
		if (slowDown && speed > otherCarSpeed)
		{
			speed -= 0.05f;
		}
		else if (speed <= otherCarSpeed)
		{
			slowDown = false;
		}
	}

	private void FixedUpdate()
	{
		transform.Translate(Vector2.down * speed * Time.deltaTime);
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (speed > other.transform.gameObject.GetComponent<CarMover>().speed)
		{
			slowDown = true;
			otherCarSpeed = other.transform.gameObject.GetComponent<CarMover>().speed;
		}
	}
}
