using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class ClearMotiCount : MonoBehaviour
{
    [SerializeField] private Image[] MotiImage;  //画像が出る場所
    [SerializeField] private Sprite[] MotiSprites; //餅の画像
    [SerializeField]private Image NoMotiImage;
    [SerializeField]private Image BackGroundImage;
    [SerializeField]private Text MotiCountText;
    [SerializeField]private Image DishImage;
    private int displayMotiConut = 0;
    
    private List<int> motiQueue = new List<int>(); //餅の順番のリスト
    [SerializeField]private AudioSource audioSource;  // AudioSource コンポーネント
    [SerializeField]private AudioClip MotiClip;
    [SerializeField]private AudioClip finishClip;
    [SerializeField]private AudioClip NoMotiClip;

    void Start()
    {
        
        foreach (var image in MotiImage)//全部のimageを見えなくする
        {
            image.gameObject.SetActive(false);
        }
        // GManager からスコアを取得
        int sakuraCount = GManager.instance.sakuramotiScore;
        int domyoziCount = GManager.instance.DomyouziScore;
        int kashiwaCount = GManager.instance.kasiwamotiScore;
        int catScoreCount = GManager.instance.catScore;

        // 餅をリストに追加（ランダム順に並べる）
        List<int> tempMotiList = new List<int>();
        for (int i = 0; i < sakuraCount; i++) tempMotiList.Add(0);  // 桜餅 (0)
        for (int i = 0; i < domyoziCount; i++) tempMotiList.Add(1); // 道明餅 (1)
        for (int i = 0; i < kashiwaCount; i++) tempMotiList.Add(2); // かしわ餅 (2)
        for (int i =0; i < catScoreCount; i++) tempMotiList.Add(3);//猫の餅(3)

        MotiCountText.text =  "0";
        // リストをランダムにシャッフル
        ShuffleList(tempMotiList);
        motiQueue = tempMotiList;
        if(motiQueue.Count == 0){
            audioSource.PlayOneShot(NoMotiClip);
            DishImage.gameObject.SetActive(false);
            NoMotiImage.gameObject.SetActive(true);
        }
        //一定時間に餅を出す
        StartCoroutine(ShowMochiGradually());
    }

    // 餅を下から順番に表示するコルーチン
    private IEnumerator ShowMochiGradually()
    {
        for (int i = 0; i < motiQueue.Count; i++)
        {
            displayMotiConut++;
            MotiCountText.text = ""+displayMotiConut;
            int mochiIndex = motiQueue[i]; // 次に表示する餅の種類
            MotiImage[i].sprite = MotiSprites[mochiIndex]; // 画像をセット
            MotiImage[i].gameObject.SetActive(true); // 画像を表示
            audioSource.PlayOneShot(MotiClip);
            if(motiQueue.Count > 10){
                yield return new WaitForSeconds(0.2f);//十個以上だと早くなる
            }
            else{
            yield return new WaitForSeconds(0.5f); // 0.5秒ごとに表示
            }
        }
        if(motiQueue.Count != 0 && motiQueue.Count == displayMotiConut ){
            audioSource.PlayOneShot(finishClip);
        }
    }

    // リストをシャッフルする関数
    private void ShuffleList(List<int> list)
    {
        for (int i = list.Count - 1; i > 0; i--)
        {
            int randomIndex = Random.Range(0, i + 1);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}
