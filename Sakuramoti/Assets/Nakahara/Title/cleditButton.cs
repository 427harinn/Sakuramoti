using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cleditButton : MonoBehaviour
{

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void onClicked_cledit()
    {
        this.gameObject.GetComponent<AudioSource>().Play();
        Invoke("titlescene", 0.3f);
    }

    public void titlescene()
    {

        SceneManager.LoadScene("Title_NK");
    }
}
