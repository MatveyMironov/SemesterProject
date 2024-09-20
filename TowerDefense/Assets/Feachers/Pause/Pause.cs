using UnityEngine;

public class Pause : MonoBehaviour
{
    public bool IsGamePaused { get; private set; }

    private void Awake()
    {
        ResumeGame();
    }

    public void PauseGame()
    {
        Time.timeScale = 0f;

        IsGamePaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1.0f;

        IsGamePaused = false;
    }
}
