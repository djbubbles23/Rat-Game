using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{
    public Animator transition;
    public float transitionTime = 1.0f;
    private bool isLoading = false;

    void OnEnable()
    {
        // Subscribe to the sceneLoaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        // Unsubscribe from the sceneLoaded event when the object is destroyed or disabled
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Ensure that the transition happens only after the scene is loaded
        StartCoroutine(PlayTransitionAfterSceneLoad());
    }

    private IEnumerator PlayTransitionAfterSceneLoad()
    {
        // Delay the transition until after the scene has fully loaded
        yield return new WaitForEndOfFrame();

        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
    }

    public void LoadNextLevel()
    {
        // Load the next scene when this method is called
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1));
    }

    private IEnumerator LoadLevel(int levelIndex)
    {
        isLoading = true; // Prevent transition stacking
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);

        // Load the next scene
        SceneManager.LoadScene(levelIndex);
    }
}
