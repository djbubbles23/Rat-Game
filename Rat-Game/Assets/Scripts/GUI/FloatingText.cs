using UnityEngine;

public class FloatingText : MonoBehaviour
{
    public float DestroyTime = .24f; //Time before the floating text is destroyed

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, DestroyTime);
    }

}
