using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager _instance;
    public static GameManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null)
            Destroy(this);
        _instance = this;
    }
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.buildIndex == 1)
        {
            Cursor.visible = false;
            PlayerManager.Instance.InstantiatePlayer();
            SpawnManager.Instance.Spawn<Player>(PlayerManager.Instance.Player);
        }
    }

    public void GameOver() { }

}
