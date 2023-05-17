using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GameObject focalPoint;
    private float speed = 5;

    private Vector3 starPos;
    private Rigidbody playerRb;
    

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        starPos = transform.position;
    }
    
    private void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * (forwardInput * speed * Time.deltaTime * 100));

        // GameOver
        if (transform.position.y < -15)
        {
            transform.position = starPos;
            playerRb.velocity = Vector3.zero;
            UI.ResetGame();
        }
        
    }
}