using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject strongEnemyPrefab;
    public GameObject bossPrefab;
    public GameObject powerupPrefab;
    private GameObject bossInstance;
    private bool bossIsHere = false;
    private float spawnRange = 9;
    private int enemyCount;
    private int waveNumber = 0;
    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        if (waveNumber % 10 == 0)
        {
            bossInstance = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
            bossIsHere = true;
        }
        else
        {
            enemyCount = enemiesToSpawn;
            for (int i = 0; i < enemiesToSpawn; i++)
            {
                var obj = Instantiate(Random.Range(0, 10) < 8 ? enemyPrefab : strongEnemyPrefab,
                    GenerateSpawnPosition(), enemyPrefab.transform.rotation);
                obj.GetComponent<Enemy>().Died += OnEnemyDied;
            }
        }
    }

    private void OnEnemyDied()
    {
        enemyCount--;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemyCount == 0 && !bossIsHere)
        {
            waveNumber++;
            UI.score = waveNumber - 1;
            SpawnEnemyWave(waveNumber);
            Instantiate(powerupPrefab, GenerateSpawnPosition(), powerupPrefab.transform.rotation);
        }
        else if (!bossInstance)
        {
            bossIsHere = false;
        }

        if (UI.gameOver)
        {
            var enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (var t in enemies)
                Destroy(t);
            
            var powerUps = GameObject.FindGameObjectsWithTag("Powerup");
            foreach (var t in powerUps)
                Destroy(t);
            waveNumber = 0;
            enemyCount = 0;
            UI.gameOver = false;
        }
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
