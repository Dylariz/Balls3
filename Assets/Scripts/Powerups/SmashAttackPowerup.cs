using System.Collections;
using UnityEngine;

public class SmashAttackPowerup : Powerup
{
    private Rigidbody playerRb;
    private bool inAir;

    private void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        StartCoroutine(SmashAttackWithRoutine());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (inAir)
        {
            inAir = false;
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < enemies.Length; i++)
            {
                var enemyPlayerVector = enemies[i].gameObject.transform.position - transform.position;
                var enemyRb = enemies[i].gameObject.GetComponent<Rigidbody>();
                enemyRb.AddForce(enemyPlayerVector.normalized * powerupSettings.smashAttackImpulse * 5 / enemyPlayerVector.magnitude, ForceMode.Impulse);
            }
        }
    }
    
    private IEnumerator SmashAttackWithRoutine()
    {
        for (int i = 0; i < powerupSettings.countOfSmashAttacks; i++)
        {
            playerRb.AddForce(Vector3.up * powerupSettings.smashAttackVerticalImpulse, ForceMode.Impulse);
            inAir = true;
            yield return new WaitForSeconds(0.8f);
            playerRb.AddForce(Vector3.down * (powerupSettings.smashAttackVerticalImpulse * 5), ForceMode.Impulse);
            yield return new WaitForSeconds(powerupSettings.powerupLifeTime / powerupSettings.countOfSmashAttacks - 0.8f);
        }
    }
}