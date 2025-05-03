using UnityEngine;
using UnityEngine.SceneManagement;

public class DelayedSceneChanger : MonoBehaviour
{
    public string sceneName;
    public float delayInSeconds = 3f;

    void Start()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            Invoke(nameof(ChangeScene), delayInSeconds);
        }
    }

    void ChangeScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
