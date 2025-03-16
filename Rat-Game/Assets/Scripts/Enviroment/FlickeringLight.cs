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
            if (l.enabled) {
                flickerTime = Random.Range(.1f, .3f);
            }
            else {
                flickerTime = Random.Range(.5f, 1f);
            }
            l.enabled = !l.enabled;
            delta = 0;
        }
    }
}
