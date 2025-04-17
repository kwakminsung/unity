using UnityEngine;

public class AroundWrapPlayer : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private StageData stageData;

    public void UpdateAroundWrap()
    {
        Vector3 position = transform.position;

        if ( position.x < stageData.LimitMin.x || position.x > stageData.LimitMax.x )
        {
            position.x *= -1;
        }

        if ( position.y < stageData.LimitMin.y || position.y > stageData.LimitMax.y )
        {
            position.y *= -1;   
        }

        transform.position = position;
    }
}
