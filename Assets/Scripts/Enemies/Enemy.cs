using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 4;
    public float deathHeight = -10;
    
    private GameObject _player;
    private Rigidbody _enemyRb;
    public event Action Died;
    
    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _enemyRb = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        var lookDirection = (_player.transform.position - transform.position).normalized;
        _enemyRb.AddForce(lookDirection * (speed * Time.deltaTime * 100));

        if (transform.position.y < deathHeight)
        {
            Died?.Invoke();
            Destroy(gameObject);
        }
    }
}