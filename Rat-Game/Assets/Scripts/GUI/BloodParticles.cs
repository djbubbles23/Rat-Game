using System;
using UnityEngine;

public class BloodParticles : MonoBehaviour
{
    public float lifetime = 1.0f;   // how long blood should last

    private void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
        }
    }
}
