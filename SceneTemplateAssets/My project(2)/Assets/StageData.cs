using UnityEngine;

[CreateAssetMenu]
public class StageData : ScriptableObject
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Vector2 limitMin;
    [SerializeField] 
    private Vector2 limitMax;

    public Vector2 LimitMin => limitMin;
    public Vector2 LimitMax => limitMax;
}
