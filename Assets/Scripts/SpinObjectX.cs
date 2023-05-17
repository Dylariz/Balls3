using UnityEngine;

public class SpinObjectX : MonoBehaviour
{
    public float speed;

    private void Update()
    {
        transform.Rotate(Vector3.up, Time.deltaTime * speed);
    }
}
