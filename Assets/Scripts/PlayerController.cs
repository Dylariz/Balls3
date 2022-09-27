using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float speed = 5;
    private bool hasPowerup = false;
    private float powerupStrength = 15;
    private float powerupLifeTime = 8;
    private Vector3 starPos;
    private Rigidbody playerRb;
    public GameObject focalPoint;
    public GameObject rocketPrefab;
    public GameObject powerupIndicator;
    
    
    private PowerupActions currentAction;
    private float rocketCooldown = 0.8f;
    private float smashAttackVerticalImpulse = 10;
    private float smashAttackImpulse = 20;
    private float countOfSmashAttacks = 4;
    private bool inAir = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        starPos = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float forwardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * forwardInput * speed * Time.deltaTime * 100);
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.55f, 0);

        if (hasPowerup && currentAction == PowerupActions.Rockets)
        {
            if (rocketCooldown < 0)
            {
                SpawnHomingRockets();
                rocketCooldown = 0.6f;
            }

            rocketCooldown -= Time.deltaTime;
        }

        if (transform.position.y < -15)
        {
            UI.gameOver = true;
            transform.position = starPos;
            playerRb.velocity = Vector3.zero;
            StopAllCoroutines();
            hasPowerup = false;
            powerupIndicator.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Powerup"))
        {
            currentAction = other.gameObject.GetComponent<RandomPowerupAction>().actionOfPowerup;
            hasPowerup = true;
            powerupIndicator.gameObject.SetActive(true);
            var materialOfIndicator = powerupIndicator.gameObject.GetComponent<Renderer>().material;
            
            StopAllCoroutines();
            StartCoroutine(PowerupCountdownRoutine());
            
            switch (currentAction)
            {
                case PowerupActions.Push:
                    materialOfIndicator.SetColor("_Color", Color.HSVToRGB(33/360f, 85/100f, 1));
                    break;
                case PowerupActions.Rockets:
                    materialOfIndicator.SetColor("_Color", Color.HSVToRGB(230/360f, 75/100f, 1));
                    break;
                case PowerupActions.SmashAttack:
                    materialOfIndicator.SetColor("_Color", Color.HSVToRGB(0, 95/100f, 1));
                    StartCoroutine(SmashAttackWithRoutine());
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    private IEnumerator PowerupCountdownRoutine()
    {
        yield return new WaitForSeconds(powerupLifeTime);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }
    
    private IEnumerator SmashAttackWithRoutine()
    {
        for (int i = 0; i < countOfSmashAttacks; i++)
        {
            playerRb.AddForce(Vector3.up * smashAttackVerticalImpulse, ForceMode.Impulse);
            inAir = true;
            yield return new WaitForSeconds(0.8f);
            playerRb.AddForce(Vector3.down * smashAttackVerticalImpulse * 5, ForceMode.Impulse);
            yield return new WaitForSeconds(powerupLifeTime / countOfSmashAttacks - 0.8f);
        }
    }

    private void SpawnHomingRockets()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var t in enemies)
        {
            var rocket = Instantiate(rocketPrefab, transform.position + new Vector3(0, 1.05f, 0), rocketPrefab.transform.rotation);
            rocket.gameObject.GetComponent<HomingRocketController>().target = t;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup && currentAction == PowerupActions.Push)
        {
            var enemyRb = collision.gameObject.GetComponent<Rigidbody>();
            var awayFromPlayer = (collision.gameObject.transform.position - transform.position).normalized;

            enemyRb.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }

        if (hasPowerup && inAir)
        {
            inAir = false;
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");

            for (int i = 0; i < enemies.Length; i++)
            {
                var enemyPlayerVector = enemies[i].gameObject.transform.position - transform.position;
                var enemyRb = enemies[i].gameObject.GetComponent<Rigidbody>();
                enemyRb.AddForce(enemyPlayerVector.normalized * smashAttackImpulse * 5 / enemyPlayerVector.magnitude, ForceMode.Impulse);
            }
        }
    }
}