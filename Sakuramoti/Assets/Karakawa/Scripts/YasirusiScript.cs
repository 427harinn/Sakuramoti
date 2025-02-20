using UnityEngine;

public class YasirusiScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (GManager.instance.gamePlayingFlag)
        {
            Invoke("destroy", 1.0f);
        }
    }
    public void destroy()
    {
        Destroy(this.gameObject);
    }
}
