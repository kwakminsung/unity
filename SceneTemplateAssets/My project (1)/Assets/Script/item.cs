using UnityEngine;

public enum ItemType { PowerUp = 0, Boom, HP }
public class item : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private ItemType itemType;
    private Movement2D movement2D;

    private void Awake()
    {
        movement2D = GetComponent<Movement2D>();

        float x = Random.Range(-1.0f, 1.0f);
        float y = Random.Range(-1.0f, 1.0f);

        movement2D.MoveTo(new Vector3(x, y, 0));
    }

    private void OnTriggerEnter2D(Collider2D collisiton)
    {
        if (collisiton.CompareTag("Player"))
        {
            Use(collisiton.gameObject);
            Destroy(gameObject);
        }
    }

    public void Use(GameObject player)
    {
        switch (itemType)
        {
            case ItemType.PowerUp:
                player.GetComponent<weapon>().AttackLevel++;
                break;
            case ItemType.Boom:
                player.GetComponent<weapon>().BoomCount++;
                break;
            case ItemType.HP:
                player.GetComponent<PlayerHP>().CurrentHP += 30;
                break;
        }
    }
}
