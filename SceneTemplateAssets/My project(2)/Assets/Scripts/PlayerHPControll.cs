using Unity.Collections;
using UnityEngine;

public class PlayerHPControll : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private int hp = 5;


    public int HP
    {
        set => hp = Mathf.Max(0, value);
        get => hp;
    }

    public void StartBoom()
    {
        if ( hp  > 0 )
        {
            hp--;
        }
    }
}
