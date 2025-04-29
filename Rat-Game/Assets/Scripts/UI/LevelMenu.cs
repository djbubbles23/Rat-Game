using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Unity.Cinemachine;

public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public int[] levelUnlockMapping;
    public CinemachineCamera vcam1;

    private string[] zoneTypes = { "Entrance", "Combat", "Shop" }; // The zones you want to teleport to

    private bool isVcam1Active = false;

    private void Awake()
    {
        int unlockedLevel = PlayerPrefs.GetInt("UnlockedLevel", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = (i == 0);
        }

        for (int i = 0; i < levelUnlockMapping.Length; i++)
        {
            if (levelUnlockMapping[i] < unlockedLevel)
            {
                buttons[i].interactable = true;
            }
        }
    }

    private void Update()
    {
        bool isCurrentlyVcam1Active = vcam1.Priority > 0;

        if (isCurrentlyVcam1Active != isVcam1Active)
        {
            isVcam1Active = isCurrentlyVcam1Active;
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetUnlockedLevel();
        }
    }

    public void OpenLevel(int levelId)
    {
        if (!isVcam1Active && levelId >= 0 && levelId < zoneTypes.Length)
        {
            // Save the zone name BEFORE loading the scene
            StateControllerScript.currLevel = zoneTypes[levelId];

            // Load the SurfacePipesComp scene
            SceneManager.LoadScene("surfacepipescomp");
        }
    }

    private void ResetUnlockedLevel()
    {
        PlayerPrefs.SetInt("UnlockedLevel", 1);

        for (int i = 0; i < buttons.Length; i++)
        {
            buttons[i].interactable = (i == 0);
        }
    }
}
