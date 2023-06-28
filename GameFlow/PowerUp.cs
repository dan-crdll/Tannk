using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour
{
    private void OnTriggerEnter(Collider collider)
    {
        OnCollected();
        typeof(PowerUpManager).GetMethod("Collected").MakeGenericMethod(this.GetType())
            .Invoke(PowerUpManager.Instance, new object[] { this.gameObject });
    }

    public abstract void OnCollected();
    public abstract void Stop();
}
