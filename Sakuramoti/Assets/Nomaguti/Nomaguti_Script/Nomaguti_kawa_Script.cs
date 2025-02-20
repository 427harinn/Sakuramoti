using UnityEngine;

public class Nomaguti_kawa_Script : MonoBehaviour
{
    Nomaguti_SceneManager_Script manager;
    private Vector3 pos;
    private float speed;
    SpriteRenderer spriterenderer;
    [SerializeField] AudioClip seikai;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        GameObject gameobject = GameObject.Find("Nomaguti_SceneManager");
        manager = gameobject.GetComponent<Nomaguti_SceneManager_Script>();
        spriterenderer = this.GetComponent<SpriteRenderer>();
        audioSource = gameobject.GetComponent<AudioSource>();
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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "anko" && this.gameObject.tag == "kawa")
        {

            this.gameObject.tag = "tume";
            audioSource.PlayOneShot(seikai);
            switch (this.gameObject.name)
            {
                case "sakuramoti(Clone)":
                    manager.nowscene_score[0]++;
                    spriterenderer.sprite = manager.newkawa[0];
                    this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    Invoke("resetscale", 0.5f);
                    break;

                case "doumyouji(Clone)":
                    manager.nowscene_score[1]++;
                    spriterenderer.sprite = manager.newkawa[1];
                    this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    Invoke("resetscale", 0.5f);
                    break;

                case "kasiwamoti(Clone)":
                    manager.nowscene_score[2]++;
                    spriterenderer.sprite = manager.newkawa[2];
                    this.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    Invoke("resetscale", 0.5f);
                    break;
            }
        }
    }

    private void resetscale()
    {
        this.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }
}