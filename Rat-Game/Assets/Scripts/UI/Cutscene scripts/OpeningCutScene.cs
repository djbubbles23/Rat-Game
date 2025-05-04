using UnityEngine;
using System.Collections;

public class OpeningCutscene : MonoBehaviour
{
    [Tooltip("Drag your slide GameObjects here.")]
    public GameObject[] slides;

    [Tooltip("Time (in seconds) between each slide switch.")]
    public float switchInterval = 2f;

    private int currentIndex = 0;

    void Start()
    {
        // Deactivate all except the first
        for (int i = 0; i < slides.Length; i++)
        {
            slides[i].SetActive(i == 0);
        }

        StartCoroutine(SlideshowLoop());
    }

    private IEnumerator SlideshowLoop()
    {
        while (true)
        {
            yield return new WaitForSeconds(switchInterval);

            slides[currentIndex].SetActive(false);
            currentIndex = (currentIndex + 1) % slides.Length;
            slides[currentIndex].SetActive(true);
        }
    }
}
