using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.SceneManagement;

//방향키로 플레이어가 움직일 때 벽을 감지하고 자연스럽게 움직이기
public class PlayerControll : MonoBehaviour
{
    [SerializeField]
    private int tileLayer; //벽이 있는 레이어를 감지하기 위한 레이어 마스크
    private float rayDistance = 1.0f; // 플레이어 앞 방향으로 Ray를 쏘는 거리 (벽 감지 범위)
    private Vector2 moveDirection = Vector2.right; //기본 이동 방향: 오른쪽
    private Movement2D movement2D; //이동을 처리할 movement2D 컴포넌트 참조
    private AroundWrapPlayer aroundWrap; //맵 외곽을 넘을 때 위치를 반대쪽으로 연결해주는 컴포넌트
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
        tileLayer = LayerMask.GetMask("Tile"); //"Tile" 레이어 마스크를 얻음("벽 감지에 사용")
        movement2D = GetComponent<Movement2D>(); //현재 오브젝트에서 Movement2D 컴포넌트 가져오기
        aroundWrap = GetComponent<AroundWrapPlayer>(); //현재 오브젝트에서 AroundWrap 컴포넌트 가져오기
        spriteRenderer = GetComponent<SpriteRenderer>();
        playerHP = GetComponent<PlayerHPControll>();
        GameObject tilemapObj = GameObject.Find("Tilemapitem");
        if (tilemapObj != null)
        {
            Tilemap = tilemapObj.GetComponent<Tilemap>();
            if (Tilemap == null)
            {
                Debug.LogError("Tilemapitem에 Tilemap 오브젝트가 없습니다.");
            }
        }
        else
        {
            Debug.LogError("Tilemapitem 오브젝트를 찾을 수 없습니다.");
        }
    }

    private void Update()
    {
        //방향키 입력에 따라 moveDirection(이동방향) 설정
        if (Input.GetKeyDown(KeyCode.UpArrow)) //위 방향키 누르면 위로 이동
        {
            moveDirection = Vector2.up;
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow)) //왼쪽 방향키 누르면 아래로 이동
        {
            moveDirection = Vector2.left;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow)) //오른쪽 방향키 누르면 오른쪽으로 이동
        {
            moveDirection = Vector2.right;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow)) //아래 방향키 누르면 아래로 이동
        {
            moveDirection = Vector2.down;
        }

        // Raycast로 벽 감지 = Ray를 쏴서 벽이 있는지 확인 (현재 위치에서 moveDirection 방향으로 rayDistance만큼)
        RaycastHit2D hit = Physics2D.Raycast(transform.position, moveDirection, rayDistance, tileLayer);
        if (hit.collider == null) //벽이 없으면 이동 처리
        {
            movement2D.MoveTo(moveDirection); //방향으로 이동
            RotatePlayer(); //방향에 따라 캐릭터 회전
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
                Debug.Log("모든 아이템을 먹었습니다. 다음 씬으로 이동합니다");
                SceneManager.LoadScene(NextSceneName);
                yield break;
            }
        }
    }
    //아이템과 충돌했을 때 처리
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("Item") && Tilemap != null) //충돌한 오브젝트의 태그가 "Item"이면 해당 오브젝트 삭제 
        {
            Vector3 hitPosition = collision.ClosestPoint(transform.position);
            DestroyTile(hitPosition);
            Destroy(collision.gameObject);
            PlayerScore.AddScore();
            
        }

        if ( collision.CompareTag("Enemy"))
        {
            // Tilemap의 특정 타일만 파괴하려면?
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
        //이동 방향에 따라 플레이어 회전 처리
        Vector3 newRotation = Vector3.zero;

        if (moveDirection == Vector2.right)  //오른쪽 방향이면 회전 없음
        {
            newRotation.z = 0f;
        }
        else if (moveDirection == Vector2.left)  //왼쪽 방향이면 좌우 반전
        {
            newRotation.z = 180f;
        }
        else if (moveDirection == Vector2.up)  //위 방향이면 z축 90도
        {
            newRotation.z = 90f;
        }
        else if (moveDirection == Vector2.down)  //아래 방향이면 z축 -90도
        {
            newRotation.z = -90f;
        }
        //회전 적용
        transform.eulerAngles = newRotation;
    }
}
