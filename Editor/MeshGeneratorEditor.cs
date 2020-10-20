using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(MeshGenerator))]
public class MeshGeneratorEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        MeshGenerator myScript = (MeshGenerator)target;
        if(GUILayout.Button("Create Object"))
        {
            myScript.CreateObject();
        }
    }
}
