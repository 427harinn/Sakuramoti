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

    public int catScore = 3;

    public bool gamePlayingFlag = false;




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