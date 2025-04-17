using System.Collections;
using UnityEngine;

//2D grid 환경에서 오브젝트를 한 칸 단위로 부드럽게 이동시키는 스크립트
public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 0.2f; //한칸 이동에 걸리는 시간 (초 단위)   
    private bool isMove = false; //현재 이동 중인지 여부를 나타내는 변수

    //외부에서 호출하는 이동 함수
    //moveDirection은 방향 벡터 (예: Vector3.up, Vector3.left 등)
    public bool MoveTo(Vector3 moveDirection)
    {
        if (isMove) //이동 중이라면 추가 명령을 무시
        {
            return false;
        }
        
        StartCoroutine(SmoothGridMovement(transform.position + moveDirection)); // 현재위치로부터 이동방향으로 1칸 이동한 위치를 목표로 코루틴 실행

        return true;
    }

    //부드럽게 한 칸 이동하는 코루틴 함수
    private IEnumerator SmoothGridMovement(Vector2 endPosition)
    {
        Vector2 startPosition = transform.position; //시작 위치
        float percent = 0f; //이동 진행률(0 ~ 1) 
        isMove = true; //이동 중 상태로 변경

        while (percent < moveTime) //일정 시간(moveTime) 동안 반복해서 위치를 점진적으로 보간
        {
            percent += Time.deltaTime;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent / moveTime); //현재 위치를 시작과 끝 사이를 보간한 위치로 설정 (선형 보간)
            yield return null; //한 프레임 대기
        }
        isMove = false; //이동 완료 후 상태 초기화
    }
}
