using UnityEngine;

public class FlickeringLight : MonoBehaviour
{

    private Light l;
    private float flickerTime = 1f;
    private float delta = 0f;

    void Start()
    {
        l = gameObject.GetComponent<Light>();
    }

    
    void Update()
    {
        delta += Time.deltaTime;
        if (delta >= flickerTime) {
            l.enabled = !l.enabled;
            delta = 0;
            flickerTime = Random.Range(.1f, 1f);
        }
    }
}
