using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleButton : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void onClicked_start()
    {
        GManager.instance.sakuramotiScore = 0;
        GManager.instance.DomyouziScore = 0;
        GManager.instance.kasiwamotiScore = 0;
        this.gameObject.GetComponent<AudioSource>().Play();
        Invoke("startscene", 0.5f);
    }

    public void onClicked_credit()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        Invoke("creditscene", 0.5f);
    }

    public void startscene()
    {
        SceneManager.LoadScene("Rui_Start");
    }

    public void creditscene()
    {
        SceneManager.LoadScene("CreditScene");
    }
}
