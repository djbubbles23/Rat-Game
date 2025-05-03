using UnityEngine;
using Unity.Cinemachine;

public class CinemachineFOVChanger : MonoBehaviour
{
    public CinemachineCamera virtualCamera;
    public float targetFOV = 40f;
    public float duration = 2f;
    public float delayBeforeStart = 0f;
    public bool playOnStart = true;

    private float startFOV;
    private float elapsed = 0f;
    private bool isDelaying = false;
    private bool isChanging = false;

    void Start()
    {
        if (playOnStart && virtualCamera != null)
        {
            StartFOVChange();
        }
    }

    void Update()
    {
        if (isDelaying)
        {
            delayBeforeStart -= Time.deltaTime;
            if (delayBeforeStart <= 0f)
            {
                isDelaying = false;
                isChanging = true;
            }
        }

        if (isChanging && virtualCamera != null)
        {
            elapsed += Time.deltaTime;
            float t = Mathf.Clamp01(elapsed / duration);
            virtualCamera.Lens.FieldOfView = Mathf.Lerp(startFOV, targetFOV, t);

            if (t >= 1f)
            {
                isChanging = false;
            }
        }
    }

    public void StartFOVChange()
    {
        if (virtualCamera != null)
        {
            startFOV = virtualCamera.Lens.FieldOfView;
            elapsed = 0f;

            if (delayBeforeStart > 0f)
            {
                isDelaying = true;
                isChanging = false;
            }
            else
            {
                isChanging = true;
                isDelaying = false;
            }
        }
    }
}
