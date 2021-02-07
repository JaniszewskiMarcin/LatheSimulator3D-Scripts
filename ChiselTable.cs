using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "ChiselTable", menuName = "ScriptableObjects/ChiselTable", order = 2)]
public class ChiselTable : ScriptableObject
{
    public ChiselInfo FindChiselInfoWithName(string name)
    {
        for (int i = 0; i < chiselInfos.Count; i++)
        {
            if (name == chiselInfos[i].name)
                return chiselInfos[i]; 
        }

        return null; 
    }

    public ChiselInfo GetChiselInfoAtIndex(int idx)
    {
        return chiselInfos[idx];
    }

    public int Count
    {
        get { return chiselInfos.Count; }
    }

    [SerializeField] private List<ChiselInfo> chiselInfos;
}

#if UNITY_EDITOR
[CustomEditor(typeof(TurningController))]
public class TurningSubjectEditor : Editor
{
    public TurningController subject;

    public void OnEnable()
    {
        subject = target as TurningController;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Save mesh"))
        {
            var mesh = subject.GetComponent<MeshFilter>().sharedMesh;

            AssetDatabase.CreateAsset(mesh, "Assets/Meshes/" + "WoodMesh.asset"); // saves to "assets/"
        }
    }
}
#endif
