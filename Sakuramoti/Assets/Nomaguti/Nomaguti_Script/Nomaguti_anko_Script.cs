using UnityEngine;

public class Nomaguti_anko_Script : MonoBehaviour
{
    const float DOWN = -10f;
    private Vector3 pos;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
    }
}
