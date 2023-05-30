using System;
using UnityEngine;

public sealed class HomingRocketsPowerup : Powerup
{
    private float _rocketSpawnTimer;
    public void Update()
    {
        if (_rocketSpawnTimer < 0)
        {
            SpawnHomingRockets();
            _rocketSpawnTimer = powerupSettings.rocketCooldown;
        }

        _rocketSpawnTimer -= Time.deltaTime;
    }

    private void SpawnHomingRockets()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        Array.Sort(enemies, (o1, o2) => Vector3.Distance(o1.transform.position, transform.position) >=
                                        Vector3.Distance(o2.transform.position, transform.position)
            ? 1
            : -1);

        for (int i = 0;
             i < (enemies.Length < powerupSettings.countOfRocketsPerAttack
                 ? enemies.Length
                 : powerupSettings.countOfRocketsPerAttack);
             i++)
        {
            var rocket = Instantiate(powerupSettings.rocketPrefab, transform.position + new Vector3(0, 1.05f, 0),
                powerupSettings.rocketPrefab.transform.rotation);
            rocket.gameObject.GetComponent<HomingRocketController>().target = enemies[i];
        }
    }
}