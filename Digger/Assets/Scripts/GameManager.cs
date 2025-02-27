using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    private string nextScene = "MainMenuScene";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    public void LoadGameplay()
    {
        nextScene = "GameplayScene";
        SceneManager.LoadScene("SceneLoader");
    }

    public void LoadMainMenu()
    {
        nextScene = "MainMenuScene";
        SceneManager.LoadScene("SceneLoader");
    }

    public string GetNextScene()
    {
        return nextScene;
    }
}
