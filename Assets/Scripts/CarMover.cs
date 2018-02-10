using UnityEngine;

public class CarMover : MonoBehaviour 
{
    public float carSpeed;

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
    }

	void FixedUpdate () 
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * carSpeed);
	}
}
