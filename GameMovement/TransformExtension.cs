using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TransformExtension
{
    public static void SetSpawnCoordinates(this Transform transform, Transform spawnPoint)
    {
        transform.SetPositionAndRotation(spawnPoint.position, spawnPoint.rotation);
    }
}
