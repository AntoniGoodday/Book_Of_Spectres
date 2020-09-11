using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class AxolotlCombat : EnemyScript, IEnemyCombatMove
{
    [SerializeField]
    int movementRange = 1;
    [SerializeField]
    float movementSpeed = 0.16f;
    [SerializeField]
    float heightAboveGround = -1.4f;

    public Tween moveTween;



    public int MovementRange { get => movementRange; set => movementRange = value; }
    public float MovementSpeed { get => movementSpeed; set => movementSpeed = value; }
    public float HeightAboveGround { get => heightAboveGround; set => heightAboveGround = value; }



    public void Move()
    {
        float time = movementSpeed;

        previousTile.GetComponent<TileClass>().occupied = false;
        currentTileClass.occupied = true;

        Vector3 pT = new Vector3(previousTile.transform.position.x, previousTile.transform.position.y, heightAboveGround);
        Vector3 cT = new Vector3(currentTile.transform.position.x, currentTile.transform.position.y, heightAboveGround);

        moveTween = DOTween.To(() => transform.position, x => transform.position = x, cT, time)
            .SetEase(Ease.OutBack)
            //.OnStart(() => playerScript.IsLerping = true)
            .OnComplete(() => { IsMoving = false; });
    }

    
}
