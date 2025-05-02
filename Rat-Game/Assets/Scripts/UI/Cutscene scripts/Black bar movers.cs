using UnityEngine;

public class Blackbarmover: MonoBehaviour
{
    public RectTransform targetImage;
    public float targetY = 200f;
    public float moveDuration = 2f;

    private Vector2 startPos;
    private Vector2 endPos;
    private float timer = 0f;
    private bool moving = false;

    void Start()
    {
        if (targetImage != null)
        {
            startPos = targetImage.anchoredPosition;
            endPos = new Vector2(startPos.x, targetY);
            moving = true;
        }
    }

    void Update()
    {
        if (!moving || moveDuration <= 0f) return;

        timer += Time.deltaTime;
        float t = Mathf.Clamp01(timer / moveDuration);
        targetImage.anchoredPosition = Vector2.Lerp(startPos, endPos, t);

        if (t >= 1f) moving = false;
    }
}
