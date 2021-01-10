using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CombatMove : MonoBehaviour 
{

    int MovementRange { get; set; }
    float MovementSpeed { get; set; }
    float HeightAboveGround { get; set; }
    GameObject CurrentTile { get; set; }
    GameObject PreviousTile { get; set; }

    //void RayCheck(Ray ray);
    public abstract void Move(int x = 0, int y = 0);

    public abstract void SetSortingOrder(int i);
    public abstract void UpdateBattlefield(int x = 0, int y = 0);
    public abstract void UpdateEntity();

}
