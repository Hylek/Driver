using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class CarMover : MonoBehaviour 
{
    public float carSpeed;
    public float transitSpeed;
    private float direction;
    private Vector2 rayPoint;
    private Vector2 rayStart;
    private Vector2 rayStart2;
    private Vector2 rayPoint2;
    private float startTime;
    private bool isChanging;
    private float changeTimer;
    
    void Start()
    {
        carSpeed = Random.Range(2.0f, 6.0f);
        transitSpeed = Random.Range(1.0f, 3.5f);
        startTime = 0;
        isChanging = false;
        changeTimer = 3;

    }

    void Update()
    {

        startTime += Time.deltaTime;
        
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

        rayStart = new Vector2(transform.position.x + 0.4f, transform.position.y + 0.5f);
        rayPoint = new Vector2(transform.position.x + 2.5f, transform.position.y + 0.5f);
        
        rayStart2 = new Vector2(transform.position.x + 0.4f, transform.position.y - 0.5f);
        rayPoint2 = new Vector2(transform.position.x + 2.5f, transform.position.y - 0.5f);

        if (!ChangeLanes() && startTime > 5)
        {
            isChanging = true;
        }

        if (isChanging)
        {
            GetComponent<Rigidbody2D>().AddForce(Vector2.right * transitSpeed);
            
            changeTimer -= Time.deltaTime;
            if (changeTimer <= 0)
            {
                isChanging = false;
                changeTimer = 3;
            }
        }
    }

	void FixedUpdate () 
    {
        GetComponent<Rigidbody2D>().AddForce(Vector2.down * carSpeed);
	}

    private bool ChangeLanes()
    {
        
        Debug.DrawLine(rayStart, rayPoint, Color.green);
        Debug.DrawLine(rayStart2, rayPoint2, Color.green);
        bool topRight = Physics2D.Linecast(rayStart, rayPoint);
        bool botRight = Physics2D.Linecast(rayStart2, rayPoint2);

        if (topRight || botRight)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.name == "Car(Clone)")
        {
            if(carSpeed > other.gameObject.GetComponent<CarMover>().carSpeed)
            {
                carSpeed = other.gameObject.GetComponent<CarMover>().carSpeed;
            }
        }
    }
}
