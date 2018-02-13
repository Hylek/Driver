using UnityEngine;

public class CarMover : MonoBehaviour 
{
    public float carSpeed;
    private float direction;

    void Start()
    {
        carSpeed = Random.Range(2.0f, 3.0f);
    }

    void Update()
    {
        if (transform.position.y <= -8)
        {
            Destroy(gameObject);
        }

        if (StateManager.manager.t > 30 && StateManager.manager.t < 60)
        {
            SpeedBoost(2);
        }
        if (StateManager.manager.t > 60 && StateManager.manager.t < 90)
        {
            SpeedBoost(4);
        }
    }

	void FixedUpdate () 
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * carSpeed);
	}

    private void SpeedBoost(float speedBoost)
    {
        carSpeed = speedBoost;
    }
    
}
