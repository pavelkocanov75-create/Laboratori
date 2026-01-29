using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public void SelectScene(int scene_id)
    {
        SceneManager.LoadScene(scene_id);
    }
}
