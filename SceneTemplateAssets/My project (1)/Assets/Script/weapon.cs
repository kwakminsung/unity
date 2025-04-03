using System.Collections;
using UnityEngine;

public class weapon : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private GameObject projectilePrefabs;
    [SerializeField]
    private float attackRate = 0.1f;

    public void StartFiring()
    {
        StartCoroutine("TryAttack");
    }

    public void StopFiring()
    {
        StopCoroutine("TryAttack");
    }

    private IEnumerator TryAttack()
    {
        while (true)
        {
            Instantiate(projectilePrefabs, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(attackRate);
        }
    }
}
