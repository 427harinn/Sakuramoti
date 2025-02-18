using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;

    public int sakuramotiScore = 0;
    public int DomyouziScore = 0;

    public int kasiwamotiScore = 0;

    public bool gameStopFlag = false;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }


}