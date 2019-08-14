using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy Wave", menuName = "EnemyWave")]
public class EnemyWave : ScriptableObject
{

    public List<GameObject> enemies = new List<GameObject>();

    public List<Vector2> enemyPosition = new List<Vector2>();
    /*public List<float> enemyTileX = new List<float>();

    public List<float> enemyTileY = new List<float>();*/

}
