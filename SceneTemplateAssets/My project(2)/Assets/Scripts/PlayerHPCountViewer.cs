using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class PlayerHPCountViewer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    [SerializeField]
    private PlayerHPControll playerHP;
    private TextMeshProUGUI textHPCount;

    private void Awake()
    {
        textHPCount = GetComponent<TextMeshProUGUI>();

    }

    private void Update()
    {
        textHPCount.text = "X" + playerHP.HP;
    }
}
