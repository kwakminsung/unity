using UnityEngine;
using TMPro;

public class PlayerScoreViewer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private PlayerScore PlayerScoreAdd;
    [SerializeField]
    private TextMeshProUGUI textScore;
    
    private void Awake()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        textScore.text = "score"  + PlayerScoreAdd.Score;    
    }
}
