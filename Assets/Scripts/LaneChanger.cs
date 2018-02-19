using UnityEngine;
/* Daniel Cumbor 2018 */
public class LaneChanger : MonoBehaviour
{
    public float speed;
    public float otherCarSpeed;
    public int currentLane;
    public bool changeLanes;
    public float distance;
    public Vector3 otherCarPos;
    
    // Experimental cone shape raycast system
/*    private int rayAmount = 20;
    private float currentAngle = 5f;
    private RaycastHit[] frontalRays;*/
    
    // Location points for each lane on the x axis
    private float laneOne = -2.4f;
    private float laneTwo = -0.8f;
    private float laneThree = 0.95f;
    private float laneFour = 2.6f;

    private void Start()
    {
        speed = Random.Range(0.25f, 1.0f);
        changeLanes = false;
        currentLane = CheckCurrentLane();
        Debug.Log(currentLane);
        distance = 3;
    }

    private void Update()
    {
        if (CheckFront())
        {
            changeLanes = true;
            distance = Vector3.Distance(transform.position, otherCarPos);
        }
        else
        {
            distance = 3;
        }
        if (distance <= 2)
        {
            speed = otherCarSpeed;
        }
        if (speed <= otherCarSpeed && changeLanes)
        {
            FindGap();   
        }

        if (gameObject.transform.position.y < -7.0f)
        {
            Destroy(gameObject);
        }
    }

    private void FindGap()
    {
        // Lane 4
        if (currentLane == 4 && !CheckLeft())
        {
            MoveLeft(3);
        }
        if (currentLane == 4 && CheckLeft())
        {
            speed = otherCarSpeed;
        }
        
        // Lane 3
        if (currentLane == 3 && !CheckLeft() && !CheckRight())
        {
            MoveLeft(2);
        }
        else if (currentLane == 3 && !CheckLeft() && CheckRight())
        {
            MoveLeft(2);
        }
        else if (currentLane == 3 && !CheckRight() && CheckLeft())
        {
            MoveRight(4);
        }
        
        // Lane 2
        if (currentLane == 2 && !CheckLeft() && !CheckRight())
        {
            MoveRight(3);
        }
        else if (currentLane == 2 && !CheckLeft() && CheckRight())
        {
            MoveLeft(1);
        }
        else if (currentLane == 2 && !CheckRight() && CheckLeft())
        {
            MoveRight(3);
        }

        // Lane 1
        if (currentLane == 1 && !CheckRight())
        {
            MoveRight(2);
        }
        if (currentLane == 1 && CheckRight())
        {
            speed = otherCarSpeed;
        }
    }

    private void MoveLeft(float targetLane)
    {
        if (CheckCurrentLane() != targetLane)
        {
            transform.Translate(Vector2.left * speed / 1.5f * Time.deltaTime);
        }
        else if(CheckCurrentLane() == targetLane)
        {
            changeLanes = false;
            currentLane = CheckCurrentLane();
        }
    }

    private void MoveRight(float targetLane)
    {
        if (CheckCurrentLane() != targetLane)
        {
            transform.Translate(Vector2.right * speed / 1.5f * Time.deltaTime);
        }
        else if(CheckCurrentLane() == targetLane)
        {
            changeLanes = false;
            currentLane = CheckCurrentLane();
        }
    }

    private void FixedUpdate()
    {
        transform.Translate(Vector2.down * speed * Time.deltaTime);
    }

    private int CheckCurrentLane()
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

    private bool CheckFront()
    {
        RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x, transform.position.y + 1.1f),Vector2.up, 2.0f);
        Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 1.1f), new Vector2(0.0f, 2.0f),Color.magenta);
/*        for(float i = 0; i < rayAmount; i += currentAngle)
        {
            float radian = i * Mathf.Deg2Rad;
            Vector2 desiredAngle = new Vector2(Mathf.Cos(radian), Mathf.Sin(radian)) * 2.5f;
            Ray ray = new Ray(new Vector2(transform.position.x, transform.position.y + 0.5f), desiredAngle);
            frontalRays = Physics.RaycastAll(ray);
            Debug.DrawRay(new Vector2(transform.position.x, transform.position.y + 0.5f), desiredAngle, Color.magenta);
            for(int j = 0; j < frontalRays.Length; j++)
            {
                if(frontalRays[j].collider != null)
                {
                    otherCarSpeed = frontalRays[j].transform.gameObject.GetComponent<LaneChanger>().speed;
                    otherCarPos = frontalRays[j].transform.position;
                    return true;
                }
            }
        }*/
        if (hit.collider != null)
        {
            otherCarSpeed = hit.transform.gameObject.GetComponent<LaneChanger>().speed;
            otherCarPos = hit.transform.position;
            return true;
        }
        return false;
    }

    private bool CheckLeft()
    {
        if (currentLane != 1)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x - 0.8f, transform.position.y + 0.8f), Vector2.left, 1.3f);
            RaycastHit2D hitBot = Physics2D.Raycast(new Vector2(transform.position.x - 0.8f, transform.position.y - 0.8f), Vector2.left, 1.3f);
            RaycastHit2D hitAngle = Physics2D.Raycast(new Vector2(transform.position.x - 0.8f, transform.position.y + 0.8f), new Vector2(-1, 1), 1.0f);
            
            Debug.DrawRay(new Vector2(transform.position.x - 0.5f, transform.position.y + 0.8f), new Vector2(-1.3f, 0.0f), Color.cyan);
            Debug.DrawRay(new Vector2(transform.position.x - 0.5f, transform.position.y - 0.8f), new Vector2(-1.3f, 0.0f), Color.cyan);
            Debug.DrawRay(new Vector2(transform.position.x - 0.5f, transform.position.y + 0.8f), new Vector2(-1.0f, 1.0f), Color.cyan);

            if (hit.collider != null || hitBot.collider != null || hitAngle.collider != null)
            {
                return true;
            }
        }
        return false;
    }

    private bool CheckRight()
    {
        if (currentLane != 4)
        {
            RaycastHit2D hit = Physics2D.Raycast(new Vector2(transform.position.x + 0.8f, transform.position.y + 0.8f),Vector2.right, 1.3f);
            RaycastHit2D hitBot =Physics2D.Raycast(new Vector2(transform.position.x + 0.8f, transform.position.y - 0.8f), Vector2.right,1.3f);
            RaycastHit2D hitAngle =Physics2D.Raycast(new Vector2(transform.position.x + 0.8f, transform.position.y + 0.8f), Vector2.one,1.0f);
            
            Debug.DrawRay(new Vector2(transform.position.x + 0.5f, transform.position.y + 0.8f), new Vector2(1.3f, 0.0f), Color.green);
            Debug.DrawRay(new Vector2(transform.position.x + 0.5f, transform.position.y - 0.8f), new Vector2(1.3f, 0.0f), Color.green);
            Debug.DrawRay(new Vector2(transform.position.x + 0.5f, transform.position.y + 0.8f), new Vector2(1.0f, 1.0f), Color.green);

            if (hit.collider != null || hitBot.collider != null || hitAngle.collider != null)
            {
                return true;
            }
        }
        return false;
    }
}
