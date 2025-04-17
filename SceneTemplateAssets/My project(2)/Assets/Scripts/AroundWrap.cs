using UnityEngine;

public class AroundWrap : MonoBehaviour
{
 
    public float minX = -6f, maxX = 6f;
    public float minY = -6f, maxY = 6f;

    public void HandleWarp()
    {
        Vector3 pos = transform.position;

        if (pos.x > maxX) pos.x = minX;
        else if (pos.x < minX) pos.x = maxX;

        if (pos.y > maxY) pos.y = minY;
        else if (pos.y < minY) pos.y = maxY;

        transform.position = pos;
    }
}
