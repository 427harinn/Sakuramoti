using UnityEngine;

public class StartScript : MonoBehaviour
{
    [SerializeField] private GameObject timer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void start()
    {
        timer.SetActive(true);
        Destroy(gameObject);
    }

    public void startse()
    {
        GetComponent<AudioSource>().Play();
    }
}
