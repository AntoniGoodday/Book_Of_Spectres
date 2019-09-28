using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargedPlayerShot : MonoBehaviour
{
    public GameObject shotProjectile;
    [SerializeField]
    ObjectPooler objectPooler;
    public void ShootCharged(Transform shotOrigin, GameObject shooter)
    {
        if (objectPooler == null)
        {
            objectPooler = ObjectPooler.Instance;
        }

        objectPooler.SpawnFromPool(shotProjectile.name, shotOrigin.position, Quaternion.Euler(0, 0, 90), shooter.transform);
    }
}
