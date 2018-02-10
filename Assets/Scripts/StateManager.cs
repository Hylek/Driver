using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
	public static StateManager manager = null;
	public Image blurCover;
    public CanvasGroup cover;
	public CanvasGroup gameOverText;
	public Button restart;
    public bool mainGame;
	public bool gameOver;
	public Text timer;
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
	}

    void Start()
    {
        uncover = true;
		t = 0;
		start = 0;
		minutes = null;
		seconds = null;
		gameOver = false;
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

	}

	private void GameOver()
	{
		Vector3 start = blurCover.transform.position;
		Vector3 target = new Vector3(0,160,0);

		if(start.y < target.y)
		{
			blurCover.transform.Translate(Vector3.up * 4f);
		}

		if (gameOverText.alpha < 1) 
		{
			gameOverText.alpha += 0.05f;
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
