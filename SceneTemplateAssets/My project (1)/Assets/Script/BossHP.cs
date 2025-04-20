using System.Collections;
using Unity.Hierarchy;
using UnityEngine;

public class BossHP : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float maxHP = 1000;
    private float currentHP;
    private SpriteRenderer spriteRenderer;
    public GameObject nextSceneButton;
    private Boss boss;

    public float MaxHP => maxHP;
    public float CurrentHP => currentHP;



    private void Start()
    {
        nextSceneButton.SetActive(false);
    }

    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
        boss = GetComponent<Boss>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if (currentHP <= 0 )
        {
            Die();
            boss.OnDie();
        }
    }

    void Die()
    {
        Debug.Log("���� ���");
        nextSceneButton.SetActive(true);
        gameObject.SetActive(false);
    }
    private IEnumerator HitColorAnimation()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.05f);
        spriteRenderer.color = Color.white;
    }
}
