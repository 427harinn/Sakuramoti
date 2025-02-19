using UnityEngine;

public class Nomaguti_screen_Script : MonoBehaviour
{

    [SerializeField] AudioClip hyun;
    [SerializeField] AudioSource audioSource;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "anko")
        {
            Destroy(collision.gameObject);
            audioSource.PlayOneShot(hyun);

        }

        if (collision.gameObject.tag == "rane" || collision.gameObject.tag == "tume")
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "kawa")
        {
            //‚Ð‚ã‚ñ‚Á‚Ä‰¹‚ð–Â‚ç‚·‚Æ‚È‚¨‚æ‚¢
            Destroy(collision.gameObject);
            switch (collision.gameObject.name)
            {
                case "sakuramoti(Clone)":
                    if (GManager.instance.sakuramotiScore > 0)
                    {
                        GManager.instance.sakuramotiScore--;
                    }
                    break;

                case "doumyouji(Clone)":
                    if (GManager.instance.DomyouziScore > 0)
                    {
                        GManager.instance.DomyouziScore--;
                    }
                    break;

                case "kasiwamoti(Clone)":
                    if (GManager.instance.kasiwamotiScore > 0)
                    {
                        GManager.instance.kasiwamotiScore--;
                    }
                    break;
            }
        }
    }
}
