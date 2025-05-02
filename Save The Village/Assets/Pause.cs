using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject pauseScreen;
    private bool paused;
    public void PauseGame()
    {
        if(paused)
        {
            pauseScreen.SetActive(false);
            Time.timeScale = 1;
        }
        else
        {
            pauseScreen.SetActive(true);
            Time.timeScale = 0;
        }

        paused = !paused;
    }
}
