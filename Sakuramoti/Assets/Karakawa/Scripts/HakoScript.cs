using UnityEngine;

public class HakoScript : MonoBehaviour
{
    void Start()
    {
        gameObject.layer = LayerMask.NameToLayer("Hako"); // 箱の Layer を設定
    }
}
