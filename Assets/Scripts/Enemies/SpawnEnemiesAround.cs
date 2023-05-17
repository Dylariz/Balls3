using System.Collections;
using UnityEngine;

public class SpawnEnemiesAround : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float _spawnCooldown = 2;

    private void Start()
    {
        StartCoroutine(SpawnBallsAround());
    }

    private IEnumerator SpawnBallsAround()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnCooldown);
            Instantiate(enemyPrefab, transform.position + new Vector3(Random.Range(-1, 2) * Random.Range(3f, 5f), 0.1f, Random.Range(-1, 2) * Random.Range(3f, 5f)), enemyPrefab.transform.rotation);
        }
    }
}
