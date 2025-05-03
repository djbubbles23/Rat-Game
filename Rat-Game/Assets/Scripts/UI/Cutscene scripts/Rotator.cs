using UnityEngine;

public class YRotationToggle : MonoBehaviour
{
    public float delayBeforeRotate = 2f;
    public float holdDuration = 1f;
    public float rotationAngle = 90f;
    public float rotationSpeed = 90f; // degrees per second

    private Quaternion originalRotation;
    private Quaternion targetRotation;
    private float timer = 0f;
    private bool rotatingForward = false;
    private bool rotatingBack = false;
    private bool hasRotated = false;

    void Start()
    {
        originalRotation = transform.rotation;
        targetRotation = Quaternion.Euler(transform.eulerAngles.x, transform.eulerAngles.y + rotationAngle, transform.eulerAngles.z);
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!hasRotated && timer >= delayBeforeRotate)
        {
            rotatingForward = true;
        }

        if (rotatingForward)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, targetRotation) < 0.1f)
            {
                transform.rotation = targetRotation;
                rotatingForward = false;
                hasRotated = true;
                timer = 0f;
            }
        }

        if (hasRotated && timer >= holdDuration && !rotatingBack)
        {
            rotatingBack = true;
        }

        if (rotatingBack)
        {
            transform.rotation = Quaternion.RotateTowards(transform.rotation, originalRotation, rotationSpeed * Time.deltaTime);

            if (Quaternion.Angle(transform.rotation, originalRotation) < 0.1f)
            {
                transform.rotation = originalRotation;
                rotatingBack = false;
            }
        }
    }
}
