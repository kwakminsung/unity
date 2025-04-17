using UnityEngine;
using System.Collections;

// 이동 방향 열거형 정의
public enum Direction
{
    up,
    down,
    left,
    right
}

public class Enemy1 : MonoBehaviour
{
    [SerializeField] private int tileLayer; // 벽 레이어 마스크
    private float rayDistance = 1.0f; // 벽 감지 거리
    [SerializeField] private Sprite[] images; // 방향별 이미지 배열
    private Direction direction = Direction.right; // 현재 방향
    [SerializeField] private float delayTime = 3.0f; // 방향 전환 딜레이
    [SerializeField] private StageData stageData; // 외곽 제한 좌표 정보

    private Vector2 moveDirection = Vector2.right; // 실제 이동 방향 벡터
    private Movement2D movement2D; // 이동 처리 스크립트
    private AroundWrap aroundWrap; // 맵 외곽에서 연결 처리하는 스크립트
    private SpriteRenderer spriteRenderer; // 이미지 변경용 컴포넌트
    private Direction nextDirection = (Direction)(-1); // 다음 방향 후보 (초기값은 무효)

    private void Awake()
    {
        tileLayer = LayerMask.GetMask("Tile");
        movement2D = GetComponent<Movement2D>();
        aroundWrap = GetComponent<AroundWrap>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        StartCoroutine(AutoMoveRoutine()); // 자동 방향 전환 루프 시작
    }

    private void Update()
    {
        aroundWrap.HandleWarp(); // 외곽에서 반대쪽으로 이동

        // 이동 방향으로 벽 감지
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);

        if (hit.collider == null)
        {
            movement2D.MoveTo(moveDirection); // 벽이 없으면 이동
        }
        else
        {
            SetMoveDirectionByRandom(); // 벽이 있으면 방향 전환
        }
    }

    // 특정 방향으로 이동 설정
    private void SetMoveDirection(Direction direction)
    {
        this.direction = direction;
        moveDirection = Vector3FromEnum(direction);

        int index = (int)this.direction;
        if (index >= 0 && index < images.Length)
        {
            spriteRenderer.sprite = images[index]; // 이미지 변경
        }

        StopAllCoroutines(); // 이전 코루틴 정지
        StartCoroutine(SetMoveDirectionByTime()); // 일정 시간 후 방향 바꾸기 시도
    }

    // 랜덤 방향 설정
    private void SetMoveDirectionByRandom()
    {
        direction = (Direction)Random.Range(0, 4); // 0~3까지 랜덤
        SetMoveDirection(direction);
    }

    // 일정 시간 후에 새로운 방향 전환 시도
    private IEnumerator SetMoveDirectionByTime()
    {
        yield return new WaitForSeconds(delayTime);
        int dir = Random.Range(0, 2); // 반대 방향 계산
        nextDirection = (Direction)(dir * 2 + 1 - (int)direction % 2);
        StartCoroutine(CheckBlockedNextMoveDirection());
    }

    // 다음 방향이 이동 가능한지 감지
    private IEnumerator CheckBlockedNextMoveDirection()
    {
        while (true)
        {
            Vector3[] directions = new Vector3[3];
            bool[] isPossibleMoves = new bool[3];

            directions[0] = Vector3FromEnum(nextDirection); // 중심 방향

            // 양 옆 방향 정의
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

                if (!isPossibleMoves[i]) // Ray에 부딪히지 않으면 true
                {
                    possibleCount++;
                }
            }

            // 모든 방향이 이동 가능할 때 전환
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

    // 아이템과 충돌 시 제거
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Item"))
        {
            Destroy(collision.gameObject);
        }
    }

    // 자동 방향 변경 루프
    private IEnumerator AutoMoveRoutine()
    {
        while (true)
        {
            SetMoveDirectionByRandom();
            yield return new WaitForSeconds(1f);
        }
    }

    // 랜덤 방향 하나 선택 (Vector2로 반환)
    private Vector2 ChooseRandomDirection()
    {
        int rand = Random.Range(0, 4);
        return Vector3FromEnum((Direction)rand);
    }

    // Direction 열거형을 Vector2로 변환
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

