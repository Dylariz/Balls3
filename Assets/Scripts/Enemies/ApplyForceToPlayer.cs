using UnityEngine;

public class ApplyForceToPlayer : MonoBehaviour
{
    public float pushStrength = 10;

    private void OnCollisionEnter(Collision collision)
    {
        var powerup = collision.gameObject.GetComponent<PowerupHandler>();
        if (powerup != null)
        {
            if (powerup.hasPowerup && powerup.currentAction == Powerup.Actions.Push) return;

            var playerRb = collision.gameObject.GetComponent<Rigidbody>();
            var awayFromEnemy = (collision.gameObject.transform.position - transform.position).normalized;
            playerRb.AddForce(awayFromEnemy * pushStrength, ForceMode.Impulse);
        }
    }
}