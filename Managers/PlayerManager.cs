using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerManager : MonoBehaviour
{
    int health = 3;

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

    public void TakeDamage()
    {
        health--;
        if (health <= 0)
        {
            GameManager.Instance.GameOver();
            return;
        }
        OverlayManager.Instance.RemoveHeart();
    }

    public void ReloadLife()
    {
        health = 3;
        OverlayManager.Instance.ReloadHearts();
    }

    public void SetReloadTime(float time)
    {
        Player.GetComponent<Player>().reloadTime = time;
    }
}
