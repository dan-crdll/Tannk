using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacter
{
    public abstract IEnumerator Move();
    public abstract IEnumerator Attack();
    public abstract void Stop();
}
