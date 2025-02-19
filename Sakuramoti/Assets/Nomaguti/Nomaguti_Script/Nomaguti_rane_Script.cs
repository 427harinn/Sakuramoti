using UnityEngine;

public class Nomaguti_rane_Script : MonoBehaviour
{
    Nomaguti_SceneManager_Script manager;
    private Vector3 pos;
    private float speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject gameobject = GameObject.Find("Nomaguti_SceneManager");
        manager = gameobject.GetComponent<Nomaguti_SceneManager_Script>();
        speed = (17.5f / manager.MoveSpeed) / 5;
        if (speed > 20f)
        {
            speed = 20f;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.gamePlayingFlag)
        {
            pos = this.transform.position;
            pos.x -= speed * Time.deltaTime;
            this.transform.position = pos;
        }
    }
}
