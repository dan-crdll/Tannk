using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealoadLifePowerUp : PowerUp
{
    public override void OnCollected()
    {
        PlayerManager.Instance.ReloadLife();
    }

    public override void Stop()
    {
        throw new NotImplementedException();
    }
}
