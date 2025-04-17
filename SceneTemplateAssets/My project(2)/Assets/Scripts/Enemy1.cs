using UnityEngine;
using System.Collections;

// �̵� ���� ������ ����
public enum Direction
{
    up,
    down,
    left,
    right
}

public class Enemy1 : MonoBehaviour
{
    [SerializeField] private int tileLayer; // �� ���̾� ����ũ
    private float rayDistance = 1.0f; // �� ���� �Ÿ�
    [SerializeField] private Sprite[] images; // ���⺰ �̹��� �迭
    private Direction direction = Direction.right; // ���� ����
    [SerializeField] private float delayTime = 3.0f; // ���� ��ȯ ������
    [SerializeField] private StageData stageData; // �ܰ� ���� ��ǥ ����

    private Vector2 moveDirection = Vector2.right; // ���� �̵� ���� ����
    private Movement2D movement2D; // �̵� ó�� ��ũ��Ʈ
    private AroundWrap aroundWrap; // �� �ܰ����� ���� ó���ϴ� ��ũ��Ʈ
    private SpriteRenderer spriteRenderer; // �̹��� ����� ������Ʈ
    private Direction nextDirection = (Direction)(-1); // ���� ���� �ĺ� (�ʱⰪ�� ��ȿ)

    private void Awake()
    {
        tileLayer = LayerMask.GetMask("Tile");
        movement2D = GetComponent<Movement2D>();
        aroundWrap = GetComponent<AroundWrap>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(AutoMoveRoutine()); // �ڵ� ���� ��ȯ ���� ����
    }

    private void Update()
    {
        aroundWrap.HandleWarp(); // �ܰ����� �ݴ������� �̵�

        // �̵� �������� �� ����
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);

        if (hit.collider == null)
        {
            movement2D.MoveTo(moveDirection); // ���� ������ �̵�
        }
        else
        {
            SetMoveDirectionByRandom(); // ���� ������ ���� ��ȯ
        }
    }

    // Ư�� �������� �̵� ����
    private void SetMoveDirection(Direction direction)
    {
        this.direction = direction;
        moveDirection = Vector3FromEnum(direction);

        int index = (int)this.direction;
        if (index >= 0 && index < images.Length)
        {
            spriteRenderer.sprite = images[index]; // �̹��� ����
        }

        StopAllCoroutines(); // ���� �ڷ�ƾ ����
        StartCoroutine(SetMoveDirectionByTime()); // ���� �ð� �� ���� �ٲٱ� �õ�
    }

    // ���� ���� ����
    private void SetMoveDirectionByRandom()
    {
        direction = (Direction)Random.Range(0, 4); // 0~3���� ����
        SetMoveDirection(direction);
    }

    // ���� �ð� �Ŀ� ���ο� ���� ��ȯ �õ�
    private IEnumerator SetMoveDirectionByTime()
    {
        yield return new WaitForSeconds(delayTime);
        int dir = Random.Range(0, 2); // �ݴ� ���� ���
        nextDirection = (Direction)(dir * 2 + 1 - (int)direction % 2);
        StartCoroutine(CheckBlockedNextMoveDirection());
    }

    // ���� ������ �̵� �������� ����
    private IEnumerator CheckBlockedNextMoveDirection()
    {
        while (true)
        {
            Vector3[] directions = new Vector3[3];
            bool[] isPossibleMoves = new bool[3];

            directions[0] = Vector3FromEnum(nextDirection); // �߽� ����

            // �� �� ���� ����
            if (directions[0].x != 0)
            {
                directions[1] = directions[0] + new Vector3(0, 0.65f, 0);
                directions[2] = directions[0] + new Vector3(0, -0.65f, 0);
            }
            else if (directions[0].y != 0)
            {
                directions[1] = directions[0] + new Vector3(-0.65f, 0, 0);
                directions[2] = directions[0] + new Vector3(0.65f, 0, 0);
            }

            int possibleCount = 0;
            for (int i = 0; i < 3; i++)
            {
                float length = (i == 0) ? 0.5f : 0.7f;
                isPossibleMoves[i] = Physics2D.Raycast(transform.position, directions[i], length, tileLayer);
                Debug.DrawLine(transform.position, transform.position + directions[i] * length, Color.yellow);

                if (!isPossibleMoves[i]) // Ray�� �ε����� ������ true
                {
                    possibleCount++;
                }
            }

            // ��� ������ �̵� ������ �� ��ȯ
            if (possibleCount == 3)
            {
                if (transform.position.x > stageData.LimitMin.x && transform.position.x < stageData.LimitMax.x &&
                    transform.position.y > stageData.LimitMin.y && transform.position.y < stageData.LimitMax.y)
                {
                    SetMoveDirection(nextDirection);
                    nextDirection = (Direction)(-1);
                    break;
                }
            }
            yield return null;
        }
    }

    // �����۰� �浹 �� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
        }
    }

    // �ڵ� ���� ���� ����
    private IEnumerator AutoMoveRoutine()
    {
        while (true)
        {
            SetMoveDirectionByRandom();
            yield return new WaitForSeconds(1f);
        }
    }

    // ���� ���� �ϳ� ���� (Vector2�� ��ȯ)
    private Vector2 ChooseRandomDirection()
    {
        int rand = Random.Range(0, 4);
        return Vector3FromEnum((Direction)rand);
    }

    // Direction �������� Vector2�� ��ȯ
    private Vector2 Vector3FromEnum(Direction dir)
    {
        switch (dir)
        {
            case Direction.up: return Vector2.up;
            case Direction.down: return Vector2.down;
            case Direction.left: return Vector2.left;
            case Direction.right: return Vector2.right;
            default: return Vector2.zero;
        }
    }
}

