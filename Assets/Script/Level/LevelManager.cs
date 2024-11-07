using System.Collections;
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

            // Register scene load callback
            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            SceneManager.sceneLoaded -= OnSceneLoaded;  // Unregister callback
        }
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"Scene loaded: {scene.name}");
        // You can add additional code here to initialize settings or objects for the new scene.
    }

    public void LoadNextLevel()
    {
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            Debug.Log($"Loading next scene: {nextSceneName}");
            StartCoroutine(LoadSceneAsync(nextSceneName));
        }
        else
        {
            Debug.LogWarning("Next scene name is not set.");
        }
    }

    public void LoadLevelByName(string levelName)
    {
        if (Application.CanStreamedLevelBeLoaded(levelName))
        {
            Debug.Log($"Loading scene: {levelName}");
            StartCoroutine(LoadSceneAsync(levelName));
        }
        else
        {
            Debug.LogError($"Scene '{levelName}' cannot be loaded. Please check the scene name.");
        }
    }

    public void LoadLevelByIndex(int index)
    {
        if (index >= 0 && index < SceneManager.sceneCountInBuildSettings)
        {
            Debug.Log($"Loading scene by index: {index}");
            StartCoroutine(LoadSceneAsync(index));
        }
        else
        {
            Debug.LogError($"Scene index '{index}' is out of range.");
        }
    }

    private IEnumerator LoadSceneAsync(string sceneName)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            // Optionally, you can report progress here
            yield return null;
        }
    }

    private IEnumerator LoadSceneAsync(int sceneIndex)
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneIndex);
        while (!asyncLoad.isDone)
        {
            // Optionally, you can report progress here
            yield return null;
        }
    }
}
