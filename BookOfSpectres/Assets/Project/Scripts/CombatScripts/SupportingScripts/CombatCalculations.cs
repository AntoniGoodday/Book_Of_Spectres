using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CombatCalculations
{
    public static Vector3 EntityPostition(GameObject _entityPos, float _heightAboveGround)
    {
        return new Vector3(_entityPos.transform.position.x, _entityPos.transform.position.y, _entityPos.transform.position.z + _heightAboveGround);
    }

    public static Vector3 EntityPostition(Vector2 _entityPos, float _heightAboveGround)
    {
        return new Vector3(_entityPos.x, _entityPos.y, _heightAboveGround);
    }
}
