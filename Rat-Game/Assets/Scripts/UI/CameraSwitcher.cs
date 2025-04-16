using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.Cinemachine;

public class CameraSwitcher : MonoBehaviour
{
    public CinemachineCamera[] virtualCameras;
    public Button nextButton;
    public Button previousButton;
    public Button mainMenuButton;

    private int currentCameraIndex = 1; // start button control at VCam2 (index 1)

    void Start()
    {
        if (virtualCameras.Length > 0)
        {
            // Start with VCam1 for intro
            SwitchToCamera(0);

            // Auto-switch to VCam2 
            if (virtualCameras.Length > 1)
            {
                StartCoroutine(AutoSwitchToSecondCamera());
            }
        }

        if (nextButton != null)
            nextButton.onClick.AddListener(SwitchToNextCamera);

        if (previousButton != null)
            previousButton.onClick.AddListener(SwitchToPreviousCamera);

        if (mainMenuButton != null)
            mainMenuButton.onClick.AddListener(LoadMainMenu);
    }

    IEnumerator AutoSwitchToSecondCamera()
    {
        yield return new WaitForSeconds(1f); // <--- seconds until cam 1 switches to cam 2
        SwitchToCamera(1); // VCam2
    }

    public void SwitchToNextCamera()
    {
        if (currentCameraIndex < 3) // Limit to VCam4 (index 3)
        {
            SwitchToCamera(currentCameraIndex + 1);
        }
    }

    public void SwitchToPreviousCamera()
    {
        if (currentCameraIndex > 1) // Don’t allow going back to VCam1 (index 0)
        {
            SwitchToCamera(currentCameraIndex - 1);
        }
    }

    void SwitchToCamera(int index)
    {
        for (int i = 0; i < virtualCameras.Length; i++)
        {
            virtualCameras[i].Priority = (i == index) ? 10 : 0;
        }

        currentCameraIndex = index;
        UpdateButtonInteractability();
    }

    void UpdateButtonInteractability()
    {
        if (previousButton != null)
            previousButton.interactable = currentCameraIndex > 1;

        if (nextButton != null)
            nextButton.interactable = currentCameraIndex < 3;
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
}
