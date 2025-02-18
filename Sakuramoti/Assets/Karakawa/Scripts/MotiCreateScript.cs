using UnityEngine;
using System.Collections.Generic;

public class MotiCreateScript : MonoBehaviour
{
    [SerializeField] private GameObject[] motiPrefab; // 0: 桜餅, 1: 道明寺餅, 2: 柏餅, 3: 猫もち
    [SerializeField] private float timeLimit = 10.0f; // 制限時間

    private float spawnInterval; // もちを生成する間隔
    private float lastSpawnTime; // 最後にもちを生成した時間
    private Vector3 spawnPosition; // もちの生成位置
    private List<int> motiQueue = new List<int>(); // 生成するもちの順番リスト
    private int spawnedMotiCount = 0; // すでに生成したもちの数
    private bool previousGamePlayingFlag = false; // 前回のゲーム状態を保存

    void Update()
    {
        // ゲームの開始を検知（false → true）
        if (!previousGamePlayingFlag && GManager.instance.gamePlayingFlag)
        {
            Debug.Log("ゲーム開始 - もちの生成を開始");
            InitializeMotiQueue(); // もちのキューをセットアップ
            SpawnMoti(); // 最初のもちを生成
        }

        // ゲームが進行中ならもちを生成
        if (GManager.instance.gamePlayingFlag && spawnedMotiCount < motiQueue.Count)
        {
            if (Time.time - lastSpawnTime >= spawnInterval)
            {
                SpawnMoti();
            }
        }

        // 現在のフラグ状態を保存
        previousGamePlayingFlag = GManager.instance.gamePlayingFlag;
    }

    void InitializeMotiQueue()
    {
        // もちの固定生成位置を設定
        spawnPosition = transform.position;

        // もちの合計数を計算し、キューに追加
        motiQueue.Clear(); // ゲームがリスタートしたときにキューをリセット
        AddMotiToQueue(0, GManager.instance.sakuramotiScore);  // 桜餅
        AddMotiToQueue(1, GManager.instance.DomyouziScore);   // 道明寺餅
        AddMotiToQueue(2, GManager.instance.kasiwamotiScore); // 柏餅
        AddMotiToQueue(3, 3); // 猫もち

        // もちの合計数
        int totalMotiCount = motiQueue.Count;

        // もちが 0 個の場合は生成しない
        if (totalMotiCount > 0)
        {
            // リストをシャッフルしてランダムな順番にする
            ShuffleMotiQueue();

            // もちを生成する間隔を計算（制限時間内に均等に生成）
            spawnInterval = timeLimit / totalMotiCount;

            // 生成数リセット
            spawnedMotiCount = 0;
        }
    }

    void SpawnMoti()
    {
        if (spawnedMotiCount < motiQueue.Count)
        {
            // キューから順番にもちを取り出す
            int motiIndex = motiQueue[spawnedMotiCount];

            // もちを固定位置で生成
            Instantiate(motiPrefab[motiIndex], spawnPosition, Quaternion.identity, transform);

            // 生成時間を記録
            lastSpawnTime = Time.time;

            // 生成したもちのカウントを増やす
            spawnedMotiCount++;
        }
    }

    // 指定された種類のもちをリストに追加
    void AddMotiToQueue(int motiType, int count)
    {
        for (int i = 0; i < count; i++)
        {
            motiQueue.Add(motiType);
        }
    }

    // もちのリストをシャッフル（Fisher-Yatesアルゴリズム）
    void ShuffleMotiQueue()
    {
        for (int i = motiQueue.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = motiQueue[i];
            motiQueue[i] = motiQueue[randomIndex];
            motiQueue[randomIndex] = temp;
        }
    }
}
