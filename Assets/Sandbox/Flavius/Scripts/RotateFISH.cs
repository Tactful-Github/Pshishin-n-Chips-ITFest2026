using UnityEngine;

public class RotateCoinSideways : MonoBehaviour
{
    public float rotationSpeed = 180f; // degrees per second

    void Update()
    {
        // Rotate around Y axis for horizontal spin
        transform.Rotate(0f, rotationSpeed * Time.deltaTime, 0f);
    }
}