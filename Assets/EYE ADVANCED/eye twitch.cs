using UnityEngine;

public class SurrealEye : MonoBehaviour
{
    public Transform playerHead;
    public float sensitivity = 5.0f;
    public float jitterAmount = 0.1f;
    public float jitterSpeed = 2.0f;

    void Start()
    {
        // If you forget to assign the player, it tries to find the VR Main Camera
        if (playerHead == null)
            playerHead = Camera.main.transform;
    }

    void Update()
    {
        if (playerHead == null) return;

        // 1. Calculate the target rotation to look at the player
        Vector3 direction = playerHead.position - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(direction);

        // 2. Add a surreal jitter using Perlin Noise
        float noiseX = Mathf.PerlinNoise(Time.time * jitterSpeed, 0) - 0.5f;
        float noiseY = Mathf.PerlinNoise(0, Time.time * jitterSpeed) - 0.5f;
        targetRotation *= Quaternion.Euler(noiseX * jitterAmount, noiseY * jitterAmount, 0);

        // 3. Smoothly rotate towards the target
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * sensitivity);
    }
}