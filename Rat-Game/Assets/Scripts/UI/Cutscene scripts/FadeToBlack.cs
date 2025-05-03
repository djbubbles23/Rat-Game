using UnityEngine;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    public Image targetImage;
    public float fadeDuration = 2f;
    public float delayBeforeFade = 1f;

    private float timer = 0f;
    private Color originalColor;
    private bool fadeStarted = false;

    void Start()
    {
        if (targetImage != null)
        {
            originalColor = targetImage.color;
            targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, 0f);
        }
    }

    void Update()
    {
        if (targetImage == null || targetImage.color.a >= 1f) return;

        if (!fadeStarted)
        {
            timer += Time.deltaTime;
            if (timer >= delayBeforeFade)
            {
                fadeStarted = true;
                timer = 0f; // reset for fade timing
            }
        }
        else
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            targetImage.color = new Color(originalColor.r, originalColor.g, originalColor.b, alpha);
        }
    }
}
