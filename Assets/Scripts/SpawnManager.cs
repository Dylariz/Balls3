using UnityEngine;
using Random = UnityEngine.Random;

public class SpawnManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public GameObject strongEnemyPrefab;
    public GameObject bossPrefab;
    public GameObject powerupPrefab;

    private GameObject _bossInstance;
    
    private float _spawnRange = 9;
    private int _enemyCount;
    private int _waveNumber;

    private void Start()
    {
        UI.GameOver += OnResetGame;
    }

    private void SpawnEnemyWave(int enemiesToSpawn)
    {
        if (_waveNumber % 10 == 0)
        {
            _bossInstance = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
            _bossInstance.GetComponent<Enemy>().Died += OnBossDied;
            _enemyCount = 1;
        }
        else
        {
            _enemyCount = enemiesToSpawn;
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
        _enemyCount--;
    }

    private void OnBossDied()
    {
        var enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (var t in enemies)
        {
            Destroy(t);
        }
        _enemyCount--;
    }

    private void Update()
    {
        // Enemy Defeat
        if (_enemyCount == 0)
        {
            _waveNumber++;
            UI.score = _waveNumber - 1;
            SpawnEnemyWave(_waveNumber);
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
        _waveNumber = 0;
        _enemyCount = 0;
    }

    private Vector3 GenerateSpawnPosition()
    {
        var player = GameObject.FindGameObjectWithTag("Player");
        Vector3 pos;
        while (true)
        {
            pos = new Vector3(Random.Range(-_spawnRange, _spawnRange), 0, Random.Range(-_spawnRange, _spawnRange));
            if ((player.transform.position - pos).magnitude > 6)
            {
                break;
            }
        }

        return pos;
    }
}