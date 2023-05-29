using System.Collections;
using UnityEngine;

public class SpawnEnemiesAround : MonoBehaviour
{
    public GameObject enemyPrefab;
    private float _spawnCooldown = 2;

    private void Start()
    {
        StartCoroutine(SpawnBallsAroundRoutine());
    }

    private IEnumerator SpawnBallsAroundRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(_spawnCooldown);
            Instantiate(enemyPrefab,
                transform.position + new Vector3(Random.Range(0, 2) == 1 ? 1 : -1, -0.3f,
                    Random.Range(0, 2) == 1 ? 1 : -1), enemyPrefab.transform.rotation);
        }
    }
}