using UnityEngine;


public class LightRanomizer : MonoBehaviour
{
    FlickeringLight[] lights;
    
    int numToChange;

    void Start()
    {
        lights = gameObject.GetComponentsInChildren<FlickeringLight>();
        foreach (FlickeringLight f in lights) {
            f.enabled = false;
        }
        System.Random rand = new System.Random();

        numToChange = rand.Next(0 , lights.Length / 3);

        int i = 0;
        while (i < numToChange) {
            int nextIdx = rand.Next(0,lights.Length);
            lights[nextIdx].enabled = !lights[nextIdx].enabled;
            i++;
        }
    }
}
