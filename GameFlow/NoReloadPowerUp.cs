using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoReloadPowerUp : PowerUp
{
    public override void OnCollected()
    {
        PlayerManager.Instance.SetReloadTime(0.1f);
    }

    public override void Stop()
    {
        PlayerManager.Instance.SetReloadTime(1f);
    }
}
