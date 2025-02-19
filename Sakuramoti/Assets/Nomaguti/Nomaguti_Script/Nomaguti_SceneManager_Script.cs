using NUnit.Framework;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class Nomaguti_SceneManager_Script : MonoBehaviour
{
    private float MoveTime;//タイムデルタタイムを加算していくもの
    private float laneTime;
    [HideInInspector] public float MoveSpeed;//移動速度　30/合計数
    [SerializeField]private GameObject[] gameobject= new GameObject[3];
    private int[] beforscene_score = new int[3];
    [SerializeField] GameObject anko;
    [SerializeField] GameObject lane;
    public Sprite[] newkawa = new Sprite[3];

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        beforscene_score[0] = GManager.instance.sakuramotiScore;
        beforscene_score[1] = GManager.instance.DomyouziScore;
        beforscene_score[2] = GManager.instance.kasiwamotiScore;

        MoveTime = 0;
        laneTime = 0;

        MoveSpeed = 13f / (GManager.instance.sakuramotiScore + GManager.instance.DomyouziScore + GManager.instance.kasiwamotiScore);
        if(MoveSpeed >= 1)
        {
            MoveSpeed = 1;
        }

        GameObject obj = Instantiate(lane);
        obj.transform.position = new Vector3(0f, 2f, 0f);//レーンのイラストによって変わる
        lanecreate();
    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.gamePlayingFlag)
        {
            MoveTime += Time.deltaTime;
            laneTime += Time.deltaTime;

            if (MoveTime >= MoveSpeed)
            {
                if (beforscene_score != null)
                {
                    randomcreat();
                }
                MoveTime = 0;
            }

            if (Input.GetMouseButtonDown(0))
            {
                GameObject an = Instantiate(anko);
                an.transform.position = new Vector3(-5.0f, 2.0f, 0f);
            }

            if (laneTime >= (MoveSpeed) * 2)
            {
                lanecreate();
                laneTime = 0;
            }
        }
    }

    private void randomcreat()
    {
        int num = 0;
        if (beforscene_score[0] != 0 || beforscene_score[1] != 0 || beforscene_score[2] != 0)
        {
            do
            {
                num = Random.Range(0, beforscene_score.Length);
            } while (beforscene_score[num] == 0);

            GameObject obj = Instantiate(gameobject[num]);
            obj.transform.position = new Vector3(8f, -1.0f, 0f);
            beforscene_score[num]--;
        }
    }


    private void lanecreate()
    {
        GameObject obj = Instantiate(lane);
        obj.transform.position = new Vector3(13.36f, 2f, 0f);//レーンのイラストによって変わる
    }
}