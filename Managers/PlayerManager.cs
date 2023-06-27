using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    [SerializeField] GameObject _playerPrefab;
    private GameObject _player;
    public GameObject Player { get { return _player; } }

    private static PlayerManager _instance;
    public static PlayerManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null)
            Destroy(this);
        _instance = this;
    }

    public void InstantiatePlayer()
    {
        _player = Instantiate(_playerPrefab);
    }
}
