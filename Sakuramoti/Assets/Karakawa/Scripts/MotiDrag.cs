using UnityEngine;

public class MotiDrag : MonoBehaviour
{
    private bool isDragging = false;
    private bool isInsideBox = false; // もちが箱の中にいるか
    private string boxTag = ""; // 接触している箱のタグ
    private bool previousGamePlayingFlag = false; // 前回のゲーム状態を保存

    // 各種類の成功カウント（すべてのもちで共有）
    private static int sakuraSuccessCount = 0;
    private static int domyoSuccessCount = 0;
    private static int kasiwaSuccessCount = 0;
    private static int catSuccessCount = 0;

    void Update()
    {
        Debug.Log("sakuraSuccessCount: " + sakuraSuccessCount);
        Debug.Log("domyoSuccessCount: " + domyoSuccessCount);
        Debug.Log("kasiwaSuccessCount: " + kasiwaSuccessCount);
        Debug.Log("catSuccessCount: " + catSuccessCount);
        // ゲームの開始を検知（false → true）
        if (!previousGamePlayingFlag && GManager.instance.gamePlayingFlag)
        {
            Debug.Log("ゲーム開始");
        }

        // ゲームの終了を検知（true → false）
        if (previousGamePlayingFlag && !GManager.instance.gamePlayingFlag)
        {
            UpdateFinalScore();
        }

        // 現在のフラグ状態を保存（次のフレームで判定できるように）
        previousGamePlayingFlag = GManager.instance.gamePlayingFlag;

        if (GManager.instance.gamePlayingFlag)
        {
            if (isDragging)
            {
                Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                mousePos.z = 0;
                this.transform.position = mousePos;
            }
        }
    }

    void OnMouseDown()
    {
        if (GManager.instance.gamePlayingFlag)
        {
            isDragging = true;
        }
    }

    void OnMouseUp()
    {
        if (GManager.instance.gamePlayingFlag && isDragging)
        {
            isDragging = false;

            // もちが箱の中にいる && もちのタグと箱のタグが一致する場合に成功カウント
            if (isInsideBox && this.gameObject.tag == boxTag)
            {
                if (this.gameObject.tag == "sakura")
                {
                    sakuraSuccessCount++;
                }
                else if (this.gameObject.tag == "domyo")
                {
                    domyoSuccessCount++;
                }
                else if (this.gameObject.tag == "kasiwa")
                {
                    kasiwaSuccessCount++;
                }
                else if (this.gameObject.tag == "cat")
                {
                    catSuccessCount++;
                }
            }

            // もちを削除
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // 触れたオブジェクトが箱なら、情報を記録
        if (other.gameObject.CompareTag("sakura") ||
            other.gameObject.CompareTag("domyo") ||
            other.gameObject.CompareTag("kasiwa") ||
            other.gameObject.CompareTag("cat"))
        {
            isInsideBox = true;
            boxTag = other.gameObject.tag; // 触れた箱のタグを記録
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        // 箱から出たらリセット
        if (other.gameObject.tag == boxTag)
        {
            isInsideBox = false;
            boxTag = "";
        }
    }

    // ゲーム終了時に成功したスコアをGManagerに上書き
    private void UpdateFinalScore()
    {
        GManager.instance.sakuramotiScore = sakuraSuccessCount;
        GManager.instance.DomyouziScore = domyoSuccessCount;
        GManager.instance.kasiwamotiScore = kasiwaSuccessCount;
        GManager.instance.catScore = catSuccessCount;
    }
}
