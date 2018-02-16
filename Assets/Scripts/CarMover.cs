using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMover : MonoBehaviour
{
	public float speed;
	private bool slowDown;
	private float otherCarSpeed;
	private int currentLane;
	private float cooldown;
	private bool blocked;
	
	
	// Location points for each lane on the x axis
	private float laneOne = -2.15f;
	private float laneTwo = -0.53f;
	private float laneThree = 1.18f;
	private float laneFour = 2.86f;

	private void Start()
	{
		speed = Random.Range(0.25f, 1.0f);
		slowDown = false;
		currentLane = CheckLane();
		Debug.Log(currentLane);
		cooldown = 4;
		blocked = false;
	}

	private void Update()
	{
		cooldown -= Time.deltaTime;
		AdjustSpeed();

		if (currentLane != 4 && CheckAdjacent() == 1 && cooldown <= 0)
		{
			blocked = true;
		}
		else
		{
			blocked = false;
		}
		
	}

	private int CheckLane()
	{
		int lane = 0;
		
		if (Mathf.Abs(transform.position.x - laneOne) < 0.1)
		{
			lane = 1;
		}
		if (Mathf.Abs(transform.position.x - laneTwo) < 0.1)
		{
			lane = 2;
		}
		if (Mathf.Abs(transform.position.x - laneThree) < 0.1)
		{
			lane = 3;
		}
		if (Mathf.Abs(transform.position.x - laneFour) < 0.1)
		{
			lane = 4;
		}
		return lane;
	}

	private void AdjustSpeed()
	{	
		if (CheckInFront() && speed > otherCarSpeed)
		{
			speed -= 0.05f;
		}
		else if (speed <= otherCarSpeed)
		{
			slowDown = false;
		}
	}

	private int CheckAdjacent()
	{
		
		if (currentLane != 4)
		{
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + 0.5f, transform.position.y), Vector2.right, 2.0f);
			Debug.DrawRay(new Vector2(transform.position.x + 0.5f, transform.position.y), new Vector2(2.0f, 0.0f), Color.green);

			if (hit.collider != null)
			{
				return 2;
				cooldown = 4;
			}
		}
		if (currentLane != 1)
		{
			RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - 0.5f, transform.position.y), Vector2.left, 2.0f);
			Debug.DrawRay(new Vector2(transform.position.x - 0.5f, transform.position.y), new Vector2(-2.0f, 0.0f), Color.cyan);
			
			if (hit.collider != null)
			{
				return 1;
				cooldown = 4;
			}
		}
		return 0;
	}

	private bool CheckInFront()
	{
		RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y - 0.5f), Vector2.down, 2.0f);
		Debug.DrawRay(new Vector2(transform.position.x, transform.position.y - 0.5f), new Vector2(0.0f, -2.0f), Color.magenta);
		if (hit.collider != null)
		{
			otherCarSpeed = hit.transform.gameObject.GetComponent<CarMover>().speed;
			return true;
		}
		return false;
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
