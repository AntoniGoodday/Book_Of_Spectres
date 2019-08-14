using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(BattlefieldScript))]
public class EditorCreateLevel : Editor
{
    float minLimit = 0;
    float maxLimit = 10;

    float minValPlayer = 4;
    float maxValPlayer = 6;

    

    public override void OnInspectorGUI()
   {
        DrawDefaultInspector();

        BattlefieldScript bScript = (BattlefieldScript)target;

        //maxVal = (bScript.xMax / 2) - 1;
        maxLimit = bScript.xMax-1;
        int _tempMinVal = (int)minValPlayer+1;
        int _tempMaxVal = 10 - (int)maxValPlayer+1;
        EditorGUILayout.LabelField("Player Columns:", _tempMinVal.ToString());
        EditorGUILayout.LabelField("Enemy Columns:", _tempMaxVal.ToString());
        EditorGUILayout.MinMaxSlider(ref minValPlayer, ref maxValPlayer, minLimit, maxLimit);
        if (GUILayout.Button("Create Battlefield"))
        {
            bScript.PlayerColumns = (int)minValPlayer;
            bScript.EnemyColumns = (int)maxValPlayer-2;
            bScript.SpawnTiles();
        }
        if (GUILayout.Button("Clear Battlefield"))
        {
            bScript.ClearBattlefield();
        }
    }
    
    
}
