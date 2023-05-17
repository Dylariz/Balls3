using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject strongEnemyPrefab;
    public GameObject bossPrefab;
    public GameObject powerupPrefab;

    private GameObject bossInstance;
    
    private float spawnRange = 9;
    private int enemyCount;
    private int waveNumber;

    private void Start()
    {
        UI.GameOver += OnResetGame;
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        if (waveNumber % 10 == 0)
        {
            bossInstance = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
            bossInstance.GetComponent<Enemy>().Died += OnBossDied;
            enemyCount = 1;
        }
        else
        {
            enemyCount = enemiesToSpawn;
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                var enemy = Instantiate(Random.Range(0, 10) < 8 ? enemyPrefab : strongEnemyPrefab,
                    GenerateSpawnPosition(), enemyPrefab.transform.rotation);
                enemy.GetComponent<Enemy>().Died += OnEnemyDied;
            }
        }
    }

    private void OnEnemyDied()
    {
        enemyCount--;
    }

    private void OnBossDied()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var t in enemies)
        {
            Destroy(t);
        }
        enemyCount--;
    }

    private void Update()
    {
        // Enemy Defeat
        if (enemyCount == 0)
        {
            waveNumber++;
            UI.score = waveNumber - 1;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
    }

    private void OnResetGame()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var t in enemies)
            Destroy(t);

        var powerUps = GameObject.FindGameObjectsWithTag("Powerup");
        foreach (var t in powerUps)
            Destroy(t);
        waveNumber = 0;
        enemyCount = 0;
    }

    private Vector3 GenerateSpawnPosition()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        Vector3 pos;
        while (true)
        {
            pos = new Vector3(Random.Range(-spawnRange, spawnRange), 0, Random.Range(-spawnRange, spawnRange));
            if ((player.transform.position - pos).magnitude > 6)
            {
                break;
            }
        }

        return pos;
    }
}