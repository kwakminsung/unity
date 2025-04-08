using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField]
    private int damage = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<EnemyHP>().TakeDamage(damage);
            Destroy(gameObject);
        }
        else if ( collision.CompareTag("Boss"))
        {
            collision.GetComponent<BossHP>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
