using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private static EnemyManager _instance;
    public static EnemyManager Instance { get { return _instance; } }

    private void Awake()
    {
        if (_instance != null)
            Destroy(this);
        _instance = this;
    }

    public void SetEnemiesSpeed(float speed) { }
}
