using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StateManager : MonoBehaviour
{
    public CanvasGroup cover;
    public bool mainGame;
    private string scene;
    private bool changeScene = false;
    private bool uncover = false;


    void Start()
    {
        uncover = true;
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
        if (mainGame)
        {
            CountDown();
        }

	}

    private void CountDown()
    {

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
