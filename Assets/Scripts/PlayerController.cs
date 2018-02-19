using UnityEngine;
/* Daniel Cumbor 2018 */
public class PlayerController : MonoBehaviour
{
    public float speed;
    public bool useTilt;
	
	void Update () 
    {
		if (transform.position.y <= -5) 
		{
			StateManager.manager.gameOver = true;
			Destroy (gameObject);
		}

        if(transform.position.x >= 3.8)
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if (transform.position.x <= -3)
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }
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
                    //GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed);
                    transform.Translate(Vector2.left * speed * Time.deltaTime);
                }
                else if (touch.phase == TouchPhase.Began && touch.position.x > Screen.width / 2)
                {
                }
                else if (touch.phase == TouchPhase.Stationary && touch.position.x > Screen.width / 2)
                {
                    //GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed);
                    transform.Translate(Vector2.right * speed * Time.deltaTime);
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    //transform.Translate(Vector2.zero);
                }
            }
        }

        if(Input.GetKey(KeyCode.A))
        {
            transform.Translate(Vector2.left * speed * Time.deltaTime);
        }
        if(Input.GetKey(KeyCode.D))
        {
            transform.Translate(Vector2.right * speed * Time.deltaTime);
        }

        if(useTilt)
        {
            Vector3 move = new Vector3(Input.acceleration.x, 0, 0);
            transform.Translate(move * speed * Time.deltaTime);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        StateManager.manager.gameOver = true;
        Destroy(gameObject);
        Destroy(other.gameObject);
    }
}
