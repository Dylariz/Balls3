using UnityEngine;

public class RotateCamera : MonoBehaviour
{
    public float rotationSpeed;

    private void LateUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.up, Time.deltaTime * rotationSpeed * horizontalInput);
    }
}