//using System;
//using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MotiSelect : MonoBehaviour
{
    [SerializeField] private Image MotiImage;
    [SerializeField] private Sprite[] MotiSprites;
    [SerializeField] private Sprite[] ResultSprites;
    [SerializeField] private GameObject resultImage;
    [SerializeField] private Sprite correctSprite;
    [SerializeField] private Sprite failSprite;
    [SerializeField] private GameObject otehonImage;
    [SerializeField] private Sprite sakuraSprite;
    [SerializeField] private Sprite domyoziSprite;
    [SerializeField] private Sprite kashiwaSprite;
    [SerializeField] private AudioSource audioSource;  // AudioSource コンポーネント
    [SerializeField] private AudioClip successClip;    // 正解時の音
    [SerializeField] private AudioClip failureClip;    // 不正解時の音

    private int currentMochiIndex;
    private int remainingMochiCount;

    private int sakuraCount;
    private int domyoziCount;
    private int kashiwaCount;

    private int sakuraSuccess = 0;
    private int domyoziSuccess = 0;
    private int kashiwaSuccess = 0;

    void Start()
    {
        //餅のスコアを取得取得
        sakuraCount = GManager.instance.sakuramotiScore;
        domyoziCount = GManager.instance.DomyouziScore;
        kashiwaCount = GManager.instance.kasiwamotiScore;

        //残り回数残り回数
        remainingMochiCount = sakuraCount + domyoziCount + kashiwaCount;

        if (remainingMochiCount > 0)//残り回数まで実行
        {
            SetRandomMochi();
        }
        else
        {
        }
    }

    public void SetRandomMochi()
    {
        if (remainingMochiCount <= 0)//残りの餅がなくなったら終わり
        {
            ShowFinalResults();
            return;
        }

        int totalScore = sakuraCount + domyoziCount + kashiwaCount;
        int randomValue = Random.Range(0, totalScore);

        if (randomValue < sakuraCount)
        {
            otehonImage.GetComponent<Image>().sprite = sakuraSprite;
            currentMochiIndex = 0; // 桜餅
            sakuraCount--;
        }
        else if (randomValue < sakuraCount + domyoziCount)
        {
            otehonImage.GetComponent<Image>().sprite = domyoziSprite;
            currentMochiIndex = 1; // 道明餅
            domyoziCount--;
        }
        else
        {
            otehonImage.GetComponent<Image>().sprite = kashiwaSprite;
            currentMochiIndex = 2; // かしわ餅
            kashiwaCount--;
        }

        remainingMochiCount--;//残りの餅をカウント
        MotiImage.sprite = MotiSprites[currentMochiIndex];//餅の画像を選択
        //resultText.text = ""; // 結果をリセット
        //remainingText.text = "残り：" + remainingMochiCount;
    }

    public void CheckAnswer(string chosenLeaf)
    {
        bool isCorrect = false;

        if (chosenLeaf == "sakura" && (currentMochiIndex == 0 || currentMochiIndex == 1))//桜餅の葉っぱを選んだら
        {
            isCorrect = true;
            if (currentMochiIndex == 0) sakuraSuccess++; // 道明餅成功
            else domyoziSuccess++; // 桜餅成功
        }
        else if (chosenLeaf == "kashiwa" && currentMochiIndex == 2)//かしわとと道明の葉っぱを選んだら選んだら
        {
            isCorrect = true;
            if (currentMochiIndex == 2) kashiwaSuccess++; // 道明餅成功

        }

        if (isCorrect)//正しい葉っぱを選んだ時
        {
            // 成功音を再生
            audioSource.PlayOneShot(successClip);
            resultImage.SetActive(true);
            resultImage.GetComponent<Image>().sprite = correctSprite;
            //resultText.color = Color.green;
            MotiImage.sprite = ResultSprites[currentMochiIndex];//正しい葉っぱを選んだ餅の画像を入れる
        }
        else
        {
            // 失敗音を再生
            audioSource.PlayOneShot(failureClip);
            resultImage.SetActive(true);
            resultImage.GetComponent<Image>().sprite = failSprite;
            //resultText.color = Color.red;
        }

        StartCoroutine(WaitAndSetNextMochi());
    }

    private IEnumerator WaitAndSetNextMochi()
    {
        yield return new WaitForSeconds(0.5f);

        if (remainingMochiCount > 0)//残りの餅があれば続ける
        {
            resultImage.SetActive(false);
            SetRandomMochi();
        }
        else
        {
            ShowFinalResults();
        }
    }

    private void ShowFinalResults()
    {

        // 成功回数を GManager に保存
        GManager.instance.sakuramotiScore = sakuraSuccess;
        GManager.instance.DomyouziScore = domyoziSuccess;
        GManager.instance.kasiwamotiScore = kashiwaSuccess;
        Debug.Log(GManager.instance.sakuramotiScore);
        Debug.Log(GManager.instance.DomyouziScore);
        Debug.Log(GManager.instance.kasiwamotiScore);
    }

}
