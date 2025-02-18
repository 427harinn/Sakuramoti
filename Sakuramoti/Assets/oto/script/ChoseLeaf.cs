using UnityEngine;
using UnityEngine.UI;

public class ChoseLeaf : MonoBehaviour
{
public MotiSelect motiGame; // MotiGameの参照をInspectorでセット
    public string leafType; // sakura, domyo, kashiwa のどれか

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnButtonClick);
    }

    void OnButtonClick()
    {
        if (motiGame != null)
        {
            motiGame.CheckAnswer(leafType);
        }
        else
        {
            Debug.LogError("MochiGame が設定されていません！");
        }
    }
}
