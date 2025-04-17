using UnityEditor;
using UnityEngine;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public class PrefabBrushAssetMenu
    {
        [MenuItem("Assets/Create/Timemap/Brushes/Prefab Brush")]
        public static void CreateBrushAsset()
        {
            var brush = ScriptableObject.CreateInstance<PrefabBrush>();
            AssetDatabase.CreateAsset(brush, "Assets/PrefabBrush.asset");
            AssetDatabase.SaveAssets();
            EditorUtility.FocusProjectWindow();
            Selection.activeObject = brush;

        }
    }
