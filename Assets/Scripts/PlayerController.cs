using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject focalPoint;
    private float _speed = 5;

    private Vector3 _starPos;
    private Rigidbody _playerRb;
    

    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        _starPos = transform.position;
    }
    
    private void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        _playerRb.AddForce(focalPoint.transform.forward * (forwardInput * _speed * Time.deltaTime * 100));

        // GameOver
        if (transform.position.y < -15)
        {
            transform.position = _starPos;
            _playerRb.velocity = Vector3.zero;
            UI.ResetGame();
        }
        
    }
}