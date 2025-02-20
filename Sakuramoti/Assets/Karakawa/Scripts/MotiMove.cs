using UnityEngine;

public class MotiMove : MonoBehaviour
{
    public float motiMoveSpeed = 10f; // 基本の速度

    void Start()
    {
        // GManager からもちの合計数を取得
        int totalMotiCount = GManager.instance.sakuramotiScore +
                             GManager.instance.DomyouziScore +
                             GManager.instance.kasiwamotiScore + 3;

        // 速度をスコアに応じて調整
        float speedMultiplier = 1.0f + (totalMotiCount * 0.1f); // もちの数が多いほど速く
        motiMoveSpeed *= speedMultiplier;
    }

    void Update()
    {
        if (GManager.instance.gamePlayingFlag)
        {
            transform.position += new Vector3(-motiMoveSpeed * Time.deltaTime, 0, 0);

            if (this.transform.position.x < -150)
            {
                Destroy(this.gameObject);
            }
        }
    }
}
