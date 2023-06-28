using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private static SpawnManager _instance;
    public static SpawnManager Instance { get { return _instance; } }

    [SerializeField] private Transform[] _enemySpawnPoints;
    [SerializeField] private Transform[] _powerUpsSpawnPoints;
    [SerializeField] private Transform _playerSpawnPoint;

    public Transform[] EnemySpawnPoints { get { return _enemySpawnPoints; } }
    public Transform PlayerSpawnPoint { get { return _playerSpawnPoint; } }
    public Transform[] PowerUpSpawnPoints { get { return _powerUpsSpawnPoints; } }

    private delegate void SpawnFunction(GameObject gameObject);
    private Dictionary<System.Type, SpawnFunction> _spawnDict = new Dictionary<System.Type, SpawnFunction>()
    {
        {typeof(Player), SpawnPlayer },
        {typeof(Enemy), SpawnEnemy },
        {typeof(PowerUp), SpawnPowerUp },
    };

    private void Awake()
    {
        if (_instance != null)
            Destroy(this);
        _instance = this;
    }

    public void Spawn<T>(GameObject characterToSpawn)
    {
        _spawnDict[typeof(T)](characterToSpawn);
        characterToSpawn.SetActive(true);
    }

    private static void SpawnPlayer(GameObject player)
    {
        Transform spawnPoint = SpawnManager.Instance.PlayerSpawnPoint;

        player.transform.SetSpawnCoordinates(spawnPoint);
    }

    private static void SpawnEnemy(GameObject enemy)
    {
        Transform spawnPoint = SpawnManager.Instance.EnemySpawnPoints[Random.Range(0, SpawnManager.Instance.EnemySpawnPoints.Length)];

        enemy.transform.SetSpawnCoordinates(spawnPoint);
    }

    private static void SpawnPowerUp(GameObject powerUp)
    {
        Transform spawnPoint = SpawnManager.Instance.PowerUpSpawnPoints[Random.Range(0, SpawnManager.Instance.PowerUpSpawnPoints.Length)];
        
        powerUp.transform.SetSpawnCoordinates(spawnPoint);
    }
}
