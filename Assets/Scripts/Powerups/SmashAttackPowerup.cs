using System.Collections;
using UnityEngine;

public class SmashAttackPowerup : Powerup
{
    private Rigidbody _playerRb;
    private bool _inAir;

    private void Start()
    {
        _playerRb = GetComponent<Rigidbody>();
        StartCoroutine(SmashAttackWithRoutine());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (_inAir)
        {
            _inAir = false;
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
            _playerRb.AddForce(Vector3.up * powerupSettings.smashAttackVerticalImpulse, ForceMode.Impulse);
            _inAir = true;
            yield return new WaitForSeconds(0.8f);
            _playerRb.AddForce(Vector3.down * (powerupSettings.smashAttackVerticalImpulse * 5), ForceMode.Impulse);
            yield return new WaitForSeconds(powerupSettings.powerupLifeTime / powerupSettings.countOfSmashAttacks - 0.8f);
        }
    }
}