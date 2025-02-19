using UnityEngine;

public class Nomaguti_anko_Script : MonoBehaviour
{
    const float DOWN = -10f;
    private Vector3 pos;
    [SerializeField] AudioClip hyun;
    private AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameObject gameobject = GameObject.Find("Nomaguti_SceneManager");
        audioSource = gameobject.GetComponent<AudioSource>();
        pos = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        pos.y += DOWN * Time.deltaTime;
        this.transform.position = pos;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "kawa")
        {
            Destroy(this.gameObject);
        }

        if (collision.gameObject.tag == "rane")
        {
            Destroy(this.gameObject);
            audioSource.PlayOneShot(hyun);
        }
    }
}