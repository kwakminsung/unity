using System.Collections;
using Unity.Hierarchy;
using UnityEngine;

public class PlayerHP : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private float maxHP = 10;
    private float currentHP;
    private SpriteRenderer spriteRenderer;

    private void Awake()
    {
        currentHP = maxHP;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(float damage)
    {
        currentHP -= damage;

        StopCoroutine("HitColorAnimation");
        StartCoroutine("HitColorAnimation");

        if (currentHP <= 0)
        {
            Debug.Log("Player HP : 0.. Die");
        }
    }

    //private IEnumerator HitColorAnimation()
    //{
    //    spriteRenderer.color = Color.red;

    //    yield return new WaitForSeconds(0, 1f);

    //    spriteRenderer.color = Color.white;
    //}
}
