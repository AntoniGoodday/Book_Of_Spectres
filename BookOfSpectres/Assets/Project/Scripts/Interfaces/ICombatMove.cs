using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICombatMove 
{

    int MovementRange { get; set; }
    float MovementSpeed { get; set; }
    float HeightAboveGround { get; set; }

    void RayCheck(Ray ray);
    void Move();
    void SetSortingOrder(int i);
    void UpdateBattlefield(int x = 0, int y = 0);
    void UpdatePlayer();

}
