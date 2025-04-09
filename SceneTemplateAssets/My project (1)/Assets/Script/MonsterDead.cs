using UnityEngine;

public class MonsterDead : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject nextSceneButton;
    private int health = 100;

    private void Start()
    {
        nextSceneButton.SetActive(false);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("¸ó½ºÅÍ »ç¸Á");
        nextSceneButton.SetActive(true);
        gameObject.SetActive(false);
    }

}
