using UnityEngine;
using TMPro;

public class ResultScoreViewer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private TextMeshProUGUI textResultScore;

    private void Awake()
    {
        textResultScore = GetComponent<TextMeshProUGUI>();
        int score = PlayerPrefs.GetInt("Score");
        textResultScore.text = "Result Score " + score;
    }
}
