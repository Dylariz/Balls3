using UnityEngine;

public sealed class PushPowerup : Powerup
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            var enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            var awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;

            enemyRb.AddForce(awayFromPlayer * powerupSettings.pushStrength, ForceMode.Impulse);
        }
    }
}