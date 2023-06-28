using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreezePowerUp : PowerUp
{
    public override void OnCollected()
    {
        EnemyManager.Instance.SetEnemiesSpeed(0);
    }

    public override void Stop()
    {
        EnemyManager.Instance.SetEnemiesSpeed(1);
    }
}
