using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GridGenerator))]
public class GridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        if (GUILayout.Button("Build Grid")) {
            GridGenerator grid = (GridGenerator)target;
            grid.DestroyGrid();
            grid.BuildMesh();
            grid.BuildGrid();
        }
    }
}

