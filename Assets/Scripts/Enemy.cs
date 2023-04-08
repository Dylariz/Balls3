using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    public bool isStrongEnemy = false;
    public bool isBoss = false;
    public GameObject enemyPrefab;
    public float speed = 4;
    
    private GameObject _player;
    private Rigidbody _enemyRb;
    private float _strongEnemyStrength = 10;
    private float _spawnBallsCooldown = 2;

    public event Action Died;


    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemyRb = GetComponent<Rigidbody>();
        if (isBoss)
        {
            StartCoroutine(SpawnBallsAround());
        }
    }

    // Update is called once per frame
    void Update()
    {
        var lookDirection = (_player.transform.position - transform.position).normalized;
        _enemyRb.AddForce(lookDirection * (speed * Time.deltaTime * 100));

        if (transform.position.y < -10 && !isBoss)
        {
            Died?.Invoke();
            Destroy(gameObject);
        }
        else if (transform.position.y < -20)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var t in enemies)
            {
                Destroy(t);
            }
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isStrongEnemy && collision.gameObject.CompareTag("Player") && collision.gameObject.GetComponent<PlayerController>().hasPowerup == false && collision.gameObject.GetComponent<PlayerController>().currentAction != PowerupActions.Push)
        {
            var playerRb = collision.gameObject.GetComponent<Rigidbody>();
            var awayFromEnemy = (collision.gameObject.transform.position - transform.position).normalized;

            playerRb.AddForce(awayFromEnemy * _strongEnemyStrength, ForceMode.Impulse);
        }
    }

    private IEnumerator SpawnBallsAround()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnBallsCooldown);
            Instantiate(enemyPrefab, transform.position + new Vector3(getIntRandom(-1, 1, new []{0}) * Random.Range(3f, 5f), 0.1f, getIntRandom(-1, 1, new []{0}) * Random.Range(3f, 5f)), enemyPrefab.transform.rotation);
        }
    }
    
    private int getIntRandom(int min, int max, params int[] exclude)
    {
        int result = 0;
        do
        {
            result = Random.Range(min, max + 1);
        }
        while (exclude.Contains(result));
        return result;
    }
}