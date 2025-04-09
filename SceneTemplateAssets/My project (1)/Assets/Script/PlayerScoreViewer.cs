using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerScoreViewer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private PlayerControll PlayerControll;
    private TextMeshProUGUI textScore;

    private void Awake()
    {
        textScore = GetComponent<TextMeshProUGUI>();
    }
    // Update is called once per frame
    private void Update()
    {
        textScore.text = "Score " + PlayerControll.Score;
    }

    private void SceneLeader(string sceneName)
    {
        if (PlayerControll.Score > 3000)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
