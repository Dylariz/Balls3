using System;
using UnityEngine;

public sealed class HomingRocketsPowerup : Powerup
{
    
    public void Update()
    {
        if (powerupSettings.rocketCooldown < 0)
        {
            SpawnHomingRockets();
            powerupSettings.rocketCooldown = 0.6f;
        }

        powerupSettings.rocketCooldown -= Time.deltaTime;
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