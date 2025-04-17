using UnityEngine;
using UnityEditor;
using UnityEditor.Tilemaps;

[CustomGridBrush(false, true, false, "Prefab Brush")]
public class PrefabBrush : GridBrush
{
    public GameObject prefab;

    public override void Paint(GridLayout grid, GameObject brushTarget, Vector3Int position)
    {
        if (prefab == null || brushTarget == null) return;

        GameObject instance = (GameObject)PrefabUtility.InstantiatePrefab(prefab);
        if (instance != null)
        {
            Undo.RegisterCreatedObjectUndo(instance, "Paint Prefab");
            instance.transform.SetParent(brushTarget.transform);
            instance.transform.position = grid.CellToWorld(position);
        }
    }

    public override void Erase(GridLayout grid, GameObject brushTarget, Vector3Int position)
    {
        foreach (Transform child in brushTarget.transform)
        {
            Vector3Int childPos = grid.WorldToCell(child.position);
            if (childPos == position)
            {
                Undo.DestroyObjectImmediate(child.gameObject);
                return;
            }
        }
    }
}

