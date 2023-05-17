using UnityEngine;

public class ApplyForceToPlayer : MonoBehaviour
{
    public float _pushStrength = 10;
    private void OnCollisionEnter(Collision collision)
    {
        var controller = collision.gameObject.GetComponent<PlayerController>();
        if (controller != null)
        {
            if (controller.hasPowerup && controller.currentAction == PowerupActions.Push) return;
            
            var playerRb = collision.gameObject.GetComponent<Rigidbody>();
            var awayFromEnemy = (collision.gameObject.transform.position - transform.position).normalized;
            playerRb.AddForce(awayFromEnemy * _pushStrength, ForceMode.Impulse);
        }
    }
}
