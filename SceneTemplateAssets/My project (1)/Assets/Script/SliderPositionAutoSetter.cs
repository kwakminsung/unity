using UnityEngine;

public class SliderPositionAutoSetter : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField]
    private Vector3 distance = Vector3.down * 10.0f;
    private Transform targetTransform;
    private RectTransform rectTransform;

    public void Setup(Transform target)
    {
        targetTransform = target;
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        if (targetTransform == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 scrrenPosition = Camera.main.WorldToScreenPoint(targetTransform.position);
        rectTransform.position = scrrenPosition + distance;
    }
}
