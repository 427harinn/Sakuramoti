using JetBrains.Annotations;
using UnityEngine;

public class TimerScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StartTimer()
    {
        GManager.instance.gameStopFlag = true;
    }

    public void StopTimer()
    {
        Destroy(this.gameObject);
        GManager.instance.gameStopFlag = false;
    }
}
