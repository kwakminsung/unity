using System.Collections;
using UnityEngine;

//2D grid ȯ�濡�� ������Ʈ�� �� ĭ ������ �ε巴�� �̵���Ű�� ��ũ��Ʈ
public class Movement2D : MonoBehaviour
{
    [SerializeField]
    private float moveTime = 0.2f; //��ĭ �̵��� �ɸ��� �ð� (�� ����)   
    private bool isMove = false; //���� �̵� ������ ���θ� ��Ÿ���� ����

    //�ܺο��� ȣ���ϴ� �̵� �Լ�
    //moveDirection�� ���� ���� (��: Vector3.up, Vector3.left ��)
    public bool MoveTo(Vector3 moveDirection)
    {
        if (isMove) //�̵� ���̶�� �߰� ����� ����
        {
            return false;
        }
        
        StartCoroutine(SmoothGridMovement(transform.position + moveDirection)); // ������ġ�κ��� �̵��������� 1ĭ �̵��� ��ġ�� ��ǥ�� �ڷ�ƾ ����

        return true;
    }

    //�ε巴�� �� ĭ �̵��ϴ� �ڷ�ƾ �Լ�
    private IEnumerator SmoothGridMovement(Vector2 endPosition)
    {
        Vector2 startPosition = transform.position; //���� ��ġ
        float percent = 0f; //�̵� �����(0 ~ 1) 
        isMove = true; //�̵� �� ���·� ����

        while (percent < moveTime) //���� �ð�(moveTime) ���� �ݺ��ؼ� ��ġ�� ���������� ����
        {
            percent += Time.deltaTime;
            transform.position = Vector2.Lerp(startPosition, endPosition, percent / moveTime); //���� ��ġ�� ���۰� �� ���̸� ������ ��ġ�� ���� (���� ����)
            yield return null; //�� ������ ���
        }
        isMove = false; //�̵� �Ϸ� �� ���� �ʱ�ȭ
    }
}
