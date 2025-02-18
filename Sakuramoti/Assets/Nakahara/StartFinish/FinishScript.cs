using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishScript : MonoBehaviour
{
    //インスペクターから読み込むシーンを指定したい
    [SerializeField] private SceneAsset sceneobj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void finish()
    {
        SceneManager.LoadScene(sceneobj.name);
    }

    public void finishse()
    {
        GetComponent<AudioSource>().Play();
    }
}
