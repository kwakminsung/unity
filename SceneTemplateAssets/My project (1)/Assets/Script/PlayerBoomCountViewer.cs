using UnityEngine;
using TMPro;

public class PlayerBoomCountViewer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private weapon weapon;
    private TextMeshProUGUI textBoomCount;

    private void Awake()
    {
        textBoomCount = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        textBoomCount.text = "X " + weapon.BoomCount;
    }
}
