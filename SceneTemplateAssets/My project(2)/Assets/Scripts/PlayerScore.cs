using UnityEngine;
using Unity.Collections;

public class PlayerScore : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int score = 0;

    public int Score
    {
        set => score = Mathf.Max(0, value);
        get => score;
    }
    public void AddScore()
    {
        score += 1000;
    }
}
