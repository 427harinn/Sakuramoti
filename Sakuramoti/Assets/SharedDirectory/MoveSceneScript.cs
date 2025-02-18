using UnityEngine;
using UnityEngine.SceneManagement;

public class MoveSceneScript : MonoBehaviour
{
    [SerializeField] string moveSceneName;

    public void ButtonClick()
    {
        SceneManager.LoadScene(moveSceneName);
    }


}
