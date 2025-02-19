using UnityEngine;
using UnityEngine.SceneManagement;

public class ResultButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClicked_retry()
    {
        GManager.instance.sakuramotiScore = 0;
        GManager.instance.DomyouziScore = 0;
        GManager.instance.kasiwamotiScore = 0;
        GManager.instance.catScore = 0;
        SceneManager.LoadScene("Rui_Start");
    }
    public void onClicked_title()
    {
        GManager.instance.sakuramotiScore = 0;
        GManager.instance.DomyouziScore = 0;
        GManager.instance.kasiwamotiScore = 0;
        GManager.instance.catScore = 0;
        SceneManager.LoadScene("Title_NK");
    }
}
