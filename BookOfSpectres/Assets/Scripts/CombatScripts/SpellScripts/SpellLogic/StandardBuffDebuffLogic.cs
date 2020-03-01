using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EnumScript;
[CreateAssetMenu(menuName = "SpellLogic/StandardBuffDebuff")]
public class StandardBuffDebuffLogic : SpellLogic
{
    ObjectPooler objectPooler;

    public BuffType[] buffs;

    PlayerScript playerScript;


    public override void Execute(CombatMiniatureProperties _properties, GameObject _user, EntityStatus _userStatus, Transform _shotOrigin)
    {
        playerScript = PlayerScript.Instance;
        
        SpellCard _tempSpell = _properties.gameObject.GetComponent<SpellVisuals>().spell;

        int _currentBuffNumber = 0;

        foreach (BuffType b in buffs)
        {
            
            switch (b)
            {
                case BuffType.Health:
                    if (power[_currentBuffNumber] < 0)
                    {
                        _userStatus.DealDamage(-power[_currentBuffNumber], -1.4f, 0.05f, BulletAlignement.Friendly);
                    }
                    else
                    {
                        if (_userStatus.hp + power[_currentBuffNumber] < _userStatus.maxHp)
                        {
                            _userStatus.hp += power[_currentBuffNumber];
                        }
                        
                    }
                    if(_userStatus.hp > _userStatus.maxHp)
                    {
                        _userStatus.hp = _userStatus.maxHp;
                    }
                    _userStatus.UpdateUI();

                    break;
                case BuffType.MaxHealth:
                    if (power[_currentBuffNumber] > 0)
                    {
                        _userStatus.maxHp += power[_currentBuffNumber];
                    }
                    else
                    {

                        if (_userStatus.maxHp + power[_currentBuffNumber] <= _userStatus.hp)
                        {
                            int _dmg = (int)(_userStatus.maxHp - _userStatus.hp) + power[_currentBuffNumber];
                            _userStatus.maxHp += power[_currentBuffNumber];
                           
                            
                            _userStatus.DealDamage(-_dmg, -1.4f, 0.05f, BulletAlignement.Friendly);
                            

                        }
                        else
                        {
                            _userStatus.maxHp += power[_currentBuffNumber];
                            _userStatus.DealDamage(0, -1.4f, 0.05f, BulletAlignement.Friendly);
                        }
                    }
                    _userStatus.UpdateUI();
                    break;
            }
            _currentBuffNumber++;
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
