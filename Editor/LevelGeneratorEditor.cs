using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(LevelGenerator))]
public class LevelGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelGenerator myScript = (LevelGenerator)target;
        if (GUILayout.Button("Create Level"))
        {
            myScript.GenerateLevel();
        }

        if(GUILayout.Button("Destroy Level"))
        {
            myScript.DestroyLevel();
        }
    }
}
