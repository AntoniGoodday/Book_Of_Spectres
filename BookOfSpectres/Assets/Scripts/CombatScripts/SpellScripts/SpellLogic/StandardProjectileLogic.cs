using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
[CreateAssetMenu(menuName = "SpellLogic/StandardProjectile")]
public class StandardProjectileLogic : SpellLogic
{
    ObjectPooler objectPooler;

    [SerializeField]
    GameObject projectile;

    PlayerScript playerScript;


    public override void Execute(CombatMiniatureProperties _properties, GameObject _user, EntityStatus _userStatus, Transform _shotOrigin)
    {
        playerScript = PlayerScript.Instance;
        
        SpellCard _tempSpell = _properties.gameObject.GetComponent<SpellVisuals>().spell;

        if (objectPooler == null)
        {
            objectPooler = ObjectPooler.Instance;
        }

        ProjectileScript _pScript = objectPooler.SpawnFromPool(projectile.name, _shotOrigin.position, Quaternion.Euler(0, 0, 90), _user.transform, projectile).GetComponent<ProjectileScript>();

        _pScript.damageDealt = _properties.power;

        if (_userStatus.directionFacing == Facing.Left)
        {
            _pScript.speed = System.Math.Abs(_pScript.speed) * -1;
        }
        else if (_userStatus.directionFacing == Facing.Right)
        {
            _pScript.speed = System.Math.Abs(_pScript.speed);
        }


        if (_tempSpell.exileOnUse == false)
        {
            _properties.combatMenu.MoveCardToDestination(_tempSpell, CardDestination.Combat, CardDestination.Graveyard);
        }
        else
        {
            _properties.combatMenu.MoveCardToDestination(_tempSpell, CardDestination.Combat, CardDestination.Exile);
        }
        _properties.cardHolder.RemoveFirst();
    }
}
