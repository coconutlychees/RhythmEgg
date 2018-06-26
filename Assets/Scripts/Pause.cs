using UnityEngine;

public class Pause : MonoBehaviour {

    public GameObject pauseMenu;

    bool paused = false;
    AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                audioSource.UnPause();
                Time.timeScale = 1;
            }
            else
            {
                audioSource.Pause();
                Time.timeScale = 0;
            }
            paused = !paused;
            pauseMenu.SetActive(paused);
        }
    }

    public void Resume()
    {
        audioSource.UnPause();
        Time.timeScale = 1;
        paused = false;
        pauseMenu.SetActive(false);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
