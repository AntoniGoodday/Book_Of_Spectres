using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnemyAI))]
public class EditorEnemyAi : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        EnemyAI aScript = (EnemyAI)target;

        if(GUILayout.Button("Assign States"))
        {
            State[] _tempStates = aScript.stateParent.GetComponentsInChildren<State>();

            for (int i = 0; i < _tempStates.Length; i++)
            {
                _tempStates[i].ai = aScript;
            }
        }
    }
}
