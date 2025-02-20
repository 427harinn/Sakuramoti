using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public class MotiDrag : MonoBehaviour
{
    private bool isDragging = false;
    private HashSet<string> insideBoxes = new HashSet<string>(); // どの箱に入っているかを管理
    private HashSet<GameObject> insideBoxesobj = new HashSet<GameObject>(); // どの箱に入っているかを管理
    private bool previousGamePlayingFlag = false; // 前回のゲーム状態を保存

    // 各種類の成功カウント（すべてのもちで共有）
    private static int sakuraSuccessCount = 0;
    private static int domyoSuccessCount = 0;
    private static int kasiwaSuccessCount = 0;
    private static int catSuccessCount = 0;

    private AudioSource seaudio;
    public AudioClip clear;
    public AudioClip nya;

    public AudioClip fall;



    void Start()
    {
        seaudio = this.gameObject.transform.parent.GetComponent<AudioSource>();
        if (sakuraSuccessCount < 0)
        {
            sakuraSuccessCount = 0;
            domyoSuccessCount = 0;
            kasiwaSuccessCount = 0;
            catSuccessCount = 0;
        }
    }

    void Update()
    {
        // ゲームの開始を検知（false → true）
        if (!previousGamePlayingFlag && GManager.instance.gamePlayingFlag)
        {
            previousGamePlayingFlag = true;
            // Debug.Log("ゲーム開始");
        }

        // ゲームの終了を検知（true → false）
        if (previousGamePlayingFlag && !GManager.instance.gamePlayingFlag)
        {
            UpdateFinalScore();
            ResetScore();
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

            // 正しい箱に入っている場合のみ成功カウントを加算
            if (insideBoxes.Contains(this.gameObject.tag))
            {
                if (this.gameObject.tag == "sakura")
                {
                    seaudio.PlayOneShot(clear);
                    foreach (var obj in insideBoxesobj)
                    {
                        if (obj.tag == "sakura")
                        {
                            obj.GetComponent<Animator>().SetTrigger("success");
                        }
                    }
                    sakuraSuccessCount++;
                }
                else if (this.gameObject.tag == "domyo")
                {
                    seaudio.PlayOneShot(clear);
                    foreach (var obj in insideBoxesobj)
                    {
                        if (obj.tag == "domyo")
                        {
                            obj.GetComponent<Animator>().SetTrigger("success");
                        }
                    }
                    domyoSuccessCount++;
                }
                else if (this.gameObject.tag == "kasiwa")
                {
                    seaudio.PlayOneShot(clear);
                    foreach (var obj in insideBoxesobj)
                    {
                        if (obj.tag == "kasiwa")
                        {
                            obj.GetComponent<Animator>().SetTrigger("success");
                        }
                    }
                    kasiwaSuccessCount++;
                }
                else if (this.gameObject.tag == "cat")
                {
                    seaudio.PlayOneShot(nya);
                    foreach (var obj in insideBoxesobj)
                    {
                        if (obj.tag == "cat")
                        {
                            obj.GetComponent<Animator>().SetTrigger("success");
                        }
                    }
                    catSuccessCount++;
                }
            }
            else if (insideBoxes.Count == 0)
            {
                //何にも触れてない音
                seaudio.PlayOneShot(fall);
            }
            else
            {
                //間違えた箱に入れた音

                //最後に入ってるオブジェクトのアニメーションを再生
                insideBoxesobj.ElementAt(0).GetComponent<Animator>().SetTrigger("fail");
            }

            // もちを削除
            Destroy(this.gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("入った: " + other.gameObject.tag);

        // 触れたオブジェクトが箱なら、リストに追加
        if (other.gameObject.CompareTag("sakura") ||
            other.gameObject.CompareTag("domyo") ||
            other.gameObject.CompareTag("kasiwa") ||
            other.gameObject.CompareTag("cat"))
        {
            insideBoxes.Add(other.gameObject.tag);
            insideBoxesobj.Add(other.transform.GetChild(0).gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Debug.Log("出た: " + other.gameObject.tag);

        // 退出した箱をリストから削除
        if (insideBoxes.Contains(other.gameObject.tag))
        {
            insideBoxes.Remove(other.gameObject.tag);
        }
    }

    // ゲーム終了時に成功したスコアをGManagerに上書き
    private void UpdateFinalScore()
    {
        if (sakuraSuccessCount > 0)
        {
            GManager.instance.sakuramotiScore = sakuraSuccessCount;
            GManager.instance.DomyouziScore = domyoSuccessCount;
            GManager.instance.kasiwamotiScore = kasiwaSuccessCount;
            GManager.instance.catScore = catSuccessCount;
        }

    }

    private void ResetScore()
    {
        Debug.Log("スコアがリセットされました");
        sakuraSuccessCount = -1;
        domyoSuccessCount = -1;
        kasiwaSuccessCount = -1;
        catSuccessCount = -1;
    }
}

