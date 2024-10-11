using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance { get; private set; }  // Singleton Instance

    [SerializeField] private string nextSceneName = "House";  // Default next scene

    private void Awake()
    {
        // Ensure there is only one instance of LevelManager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Make the LevelManager persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNextLevel()
    {
        Debug.Log($"Loading next scene: {nextSceneName}");
        SceneManager.LoadScene(nextSceneName);
    }

    public void LoadLevelByName(string levelName)
    {
        Debug.Log($"Loading scene: {levelName}");
        SceneManager.LoadScene(levelName);
    }
}
