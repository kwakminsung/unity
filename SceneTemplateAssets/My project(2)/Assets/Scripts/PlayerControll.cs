using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

//����Ű�� �÷��̾ ������ �� ���� �����ϰ� �ڿ������� �����̱�
public class PlayerControll : MonoBehaviour
{
    [SerializeField]
    private int tileLayer; //���� �ִ� ���̾ �����ϱ� ���� ���̾� ����ũ
    private float rayDistance = 1.0f; // �÷��̾� �� �������� Ray�� ��� �Ÿ� (�� ���� ����)
    private Vector2 moveDirection = Vector2.right; //�⺻ �̵� ����: ������
    private Movement2D movement2D; //�̵��� ó���� movement2D ������Ʈ ����
    private AroundWrapPlayer aroundWrap; //�� �ܰ��� ���� �� ��ġ�� �ݴ������� �������ִ� ������Ʈ
    private SpriteRenderer spriteRenderer; //spriteRenderer
    private Tilemap Tilemap;
    private PlayerHPControll playerHP;
    [SerializeField]
    private PlayerScore PlayerScore;
    [SerializeField]
    private float checkInterval = 0.5f;
    [SerializeField]
    private string NextSceneName = "restart";

    private void Start()
    {
        StartCoroutine(CheckItemRoutine());
    }
    private void Awake()
    {
        tileLayer = LayerMask.GetMask("Tile"); //"Tile" ���̾� ����ũ�� ����("�� ������ ���")
        movement2D = GetComponent<Movement2D>(); //���� ������Ʈ���� Movement2D ������Ʈ ��������
        aroundWrap = GetComponent<AroundWrapPlayer>(); //���� ������Ʈ���� AroundWrap ������Ʈ ��������
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHP = GetComponent<PlayerHPControll>();
        GameObject tilemapObj = GameObject.Find("Tilemapitem");
        if (tilemapObj != null)
        {
            Tilemap = tilemapObj.GetComponent<Tilemap>();
            if (Tilemap == null)
            {
                Debug.LogError("Tilemapitem�� Tilemap ������Ʈ�� �����ϴ�.");
            }
        }
        else
        {
            Debug.LogError("Tilemapitem ������Ʈ�� ã�� �� �����ϴ�.");
        }
    }

    private void Update()
    {
        //����Ű �Է¿� ���� moveDirection(�̵�����) ����
        if (Input.GetKeyDown(KeyCode.UpArrow)) //�� ����Ű ������ ���� �̵�
        {
            moveDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) //���� ����Ű ������ �Ʒ��� �̵�
        {
            moveDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) //������ ����Ű ������ ���������� �̵�
        {
            moveDirection = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) //�Ʒ� ����Ű ������ �Ʒ��� �̵�
        {
            moveDirection = Vector2.down;
        }

        // Raycast�� �� ���� = Ray�� ���� ���� �ִ��� Ȯ�� (���� ��ġ���� moveDirection �������� rayDistance��ŭ)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);
        if (hit.collider == null) //���� ������ �̵� ó��
        {
            movement2D.MoveTo(moveDirection); //�������� �̵�
            RotatePlayer(); //���⿡ ���� ĳ���� ȸ��
            aroundWrap.UpdateAroundWrap();
        }
    }

    private System.Collections.IEnumerator CheckItemRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(checkInterval);

            GameObject[] items = GameObject.FindGameObjectsWithTag("Item");

            if (items.Length == 0)
            {
                Debug.Log("��� �������� �Ծ����ϴ�. ���� ������ �̵��մϴ�");
                SceneManager.LoadScene(NextSceneName);
                yield break;
            }
        }
    }
    //�����۰� �浹���� �� ó��
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("Item") && Tilemap != null) //�浹�� ������Ʈ�� �±װ� "Item"�̸� �ش� ������Ʈ ���� 
        {
            Vector3 hitPosition = collision.ClosestPoint(transform.position);
            DestroyTile(hitPosition);
            Destroy(collision.gameObject);
            PlayerScore.AddScore();
            
        }

        if ( collision.CompareTag("Enemy"))
        {
            // Tilemap�� Ư�� Ÿ�ϸ� �ı��Ϸ���?
            StopCoroutine("OnHit");
            StartCoroutine("OnHit");
            Destroy(collision.gameObject);
            playerHP.StartBoom();
            PlayerScore.AddScore();
            
        }
    }

    private void DestroyTile(Vector3 position)
    {
        if (Tilemap == null) return;

        Vector3Int cellPosition = Tilemap.WorldToCell(position + Vector3.one * 0.5f);
        if (Tilemap.GetTile(cellPosition) != null)
        {
            Tilemap.SetTile(cellPosition, null);
        }
    }

    private IEnumerator OnHit()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }

    private void RotatePlayer()
    {
        //�̵� ���⿡ ���� �÷��̾� ȸ�� ó��
        Vector3 newRotation = Vector3.zero;

        if (moveDirection == Vector2.right)  //������ �����̸� ȸ�� ����
        {
            newRotation.z = 0f;
        }
        else if (moveDirection == Vector2.left)  //���� �����̸� �¿� ����
        {
            newRotation.z = 180f;
        }
        else if (moveDirection == Vector2.up)  //�� �����̸� z�� 90��
        {
            newRotation.z = 90f;
        }
        else if (moveDirection == Vector2.down)  //�Ʒ� �����̸� z�� -90��
        {
            newRotation.z = -90f;
        }
        //ȸ�� ����
        transform.eulerAngles = newRotation;
    }
}
