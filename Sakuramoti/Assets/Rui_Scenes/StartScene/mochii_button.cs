using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class mochii_button : MonoBehaviour
{
    [SerializeField] GameObject usuobj;
    Animator usuanim;
    public Image wagashiImage; // UIに表示する和菓子の画像
    public Sprite beforeSakuraSprite, beforeDomyojiSprite, beforeKashiwaSprite; // 正解前各和菓子のスプライト
    public Sprite sakuramochiSprite, domyojiSprite, kashiwaMochiSprite; // 正解後各和菓子のスプライト

    private int requiredTaps = 0; // 必要なタップ回数
    private int currentTaps = 0; // 現在のタップ回数
    private bool isGameActive = true;
    private bool isClearing = false; // 画像を変更中かどうかのフラグ

    public AudioSource tapAudioSource;   // タップ時の音
    public AudioSource clearAudioSource; // クリア時の音

    public GameObject kinuobj;

    int wagashiType = 0;

    bool noTapping = false;
    void Start()
    {
        usuanim = usuobj.GetComponent<Animator>();
        SetRandomWagashi(); // 最初の和菓子を設定
    }

    void Update()
    {
        if (GManager.instance.gamePlayingFlag)
        {
            if (isGameActive && Input.GetMouseButtonDown(0)) // タップ検知
            {
                if (noTapping) return;
                kinuobj.GetComponent<Animator>().SetTrigger("KIne_Click");
                currentTaps++;

                if (tapAudioSource != null)
                    tapAudioSource.Play();

                if (currentTaps >= requiredTaps) // 必要な回数タップしたら次の和菓子へ
                {
                    ClearWagashi(); // 1秒後に画像を変更
                }
            }
        }
    }

    public void ClearWagashi()
    {
        // クリア音を再生
        if (clearAudioSource != null)
            clearAudioSource.Play();
        if (wagashiType == 0)
        {
            wagashiImage.sprite = sakuramochiSprite;
            GManager.instance.sakuramotiScore++;
        }
        else if (wagashiType == 1)
        {
            wagashiImage.sprite = domyojiSprite;
            GManager.instance.DomyouziScore++;
        }
        else if (wagashiType == 2)
        {
            wagashiImage.sprite = kashiwaMochiSprite;
            GManager.instance.kasiwamotiScore++;
        }
        usuanim.SetTrigger("success");
        noTapping = true;
        Invoke("SetRandomWagashi", 0.3f);

        isClearing = false;
    }

    // 和菓子をランダムに選択し、必要なタップ回数を設定
    void SetRandomWagashi()
    {
        noTapping = false;
        wagashiType = Random.Range(0, 3); // 0: 桜餅, 1: 道明寺餅, 2: 柏餅

        switch (wagashiType)
        {
            case 0:
                wagashiImage.sprite = beforeSakuraSprite;
                requiredTaps = 5;
                break;
            case 1:
                wagashiImage.sprite = beforeDomyojiSprite;
                requiredTaps = 5;
                break;
            case 2:
                wagashiImage.sprite = beforeKashiwaSprite;
                requiredTaps = 5;
                break;
        }

        currentTaps = 0; // タップカウントをリセット
    }

}
