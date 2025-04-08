using UnityEngine;
using UnityEngine.UI;

public class BossHPViewer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private BossHP bossHP;
    private Slider sliderHP;

    private void Awake()
    {
        sliderHP = GetComponent<Slider>();
    }

    private void Update()
    {
        sliderHP.value = bossHP.CurrentHP / bossHP.MaxHP;
    }
}
