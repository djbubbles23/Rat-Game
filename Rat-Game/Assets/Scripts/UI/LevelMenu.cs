using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    // Store level names based on level ID
    private string[] levelNames = { "A-Workspace", "C-Workspace", "J-Workspace" };

    public void OpenLevel(int levelId)
    {
        if (levelId >= 0 && levelId < levelNames.Length) // Ensure valid index
        {
            SceneManager.LoadScene(levelNames[levelId]);
        }
        else
        {
            Debug.LogError("Invalid level ID: " + levelId);
        }
    }
}
