using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
public class StandardPlayerShot : MonoBehaviour
{
    public GameObject shotProjectile;
    [SerializeField]
    ObjectPooler objectPooler;
    EntityStatus status;

    private void Start()
    {
        status = GetComponent<EntityStatus>();
    }
    public void Shoot(Transform shotOrigin, GameObject shooter)
    {
        if(objectPooler == null)
        {
            objectPooler = ObjectPooler.Instance;
        }

        
       

        ProjectileScript _pScript = objectPooler.SpawnFromPool(shotProjectile.name, shotOrigin.position, Quaternion.Euler(0, 0, 90), shooter.transform).GetComponent<ProjectileScript>();
        if (status.directionFacing == Facing.Left)
        {
            _pScript.speed = System.Math.Abs(_pScript.speed) * -1;
        }
        else if (status.directionFacing == Facing.Right)
        {
            _pScript.speed = System.Math.Abs(_pScript.speed);
        }
    }
}
