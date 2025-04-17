using UnityEngine;
using UnityEditor;
using static UnityEngine.GraphicsBuffer;
using UnityEditor.Tilemaps;

[CustomEditor(typeof(PrefabBrush))]
public class PrefabBrushEditor : GridBrushEditor
{
    private new PrefabBrush brush => target as PrefabBrush;

    public override void OnPaintInspectorGUI()
    {
        brush.prefab = (GameObject)EditorGUILayout.ObjectField("Prefab", brush.prefab, typeof(GameObject), false);
    }
}

