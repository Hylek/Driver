using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
/* Daniel Cumbor 2018 */
public class StateManager : MonoBehaviour
{
	public static StateManager manager = null;
    public CanvasGroup cover;
	public CanvasGroup gameOverText;
	public Button restart;
    public bool startGame;
    public bool mainGame;
	public bool gameOver;
	public Text timer;
    public Text countDownTimer;
	public float speed;
	
    private string scene;
    private bool changeScene = false;
    private bool uncover = false;
	private float start;
	public float t;
	private string minutes;
	private string seconds;
	

	private void Awake()
	{
		if (manager == null) manager = this;
		else if (manager != this) Destroy(gameObject);

        // Ensure the screen never sleeps when the game is being played
        Screen.sleepTimeout = SleepTimeout.NeverSleep;
	}

    void Start()
    {
        uncover = true;
		t = 0;
		start = 0;
		minutes = null;
		seconds = null;
		gameOver = false;
        startGame = true;
    }
	
	void Update ()
    {
        // When the menu starts, remove the cover.
        if (uncover)
        {
            cover.alpha -= 0.05f;
        }

        // If there is a cover and change scene has been requested, continue.
        if (cover != null && changeScene)
        {
            uncover = false;
            cover.blocksRaycasts = true;
            if (changeScene)
            {
                // Scene change has been requested, raise the cover.
                cover.alpha += 0.05f;
            }
            if (cover.alpha >= 1)
            {
                // Cover is up, scene can be loaded now.
                SceneManager.LoadScene(scene, LoadSceneMode.Single);
                changeScene = false;
            }
        }
		if (mainGame && !gameOver)
		{
			Count();
		}

		if (gameOver) {
			GameOver();
		}

	    if (t >= 5 && t <= 30)
	    {
		    SpeedUp(1);
	    }
        //if (t >= 30 && t <= 60)
        //{
        //    SpeedUp(2);
        //}
    }

	private void SpeedUp(int state)
	{
		if (state == 1)
		{
			if (speed <= 10)
			{
				speed += 0.005f;
			}
		}
        if (state == 2)
        {
            if (speed <= 14)
            {
                speed += 0.005f;
            }
        }
    }

	private void GameOver()
	{
        uncover = false;

		if (gameOverText.alpha < 1) 
		{
			gameOverText.alpha += 0.05f;
            cover.alpha += 0.05f;
		}
		restart.interactable = true;
	}

    private void Count()
    {
		t = start += Time.deltaTime;

		minutes = ((int)t / 60).ToString();
		seconds = (t % 60).ToString("f2");

		timer.text = minutes + ":" + seconds;
    }

    public void LoadScene(string sceneName)
    {
        // The scene cannot change here as it needs to change when canvas alpha = 1.
        // So the inputted scene is taken instead.
        scene = sceneName;
        changeScene = true;
    }

    public void QuitGame()
    {
        // Exit the game!
        Application.Quit();
    }
}
