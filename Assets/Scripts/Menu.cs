using UnityEngine.SceneManagement;
using UnityEngine;

public class Menu : MonoBehaviour {

    public int buildIndex = 1;

    public void StartGame()
    {
        SceneManager.LoadScene(buildIndex);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
