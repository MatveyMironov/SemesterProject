using UnityEngine;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindow;

    [Space]
    [SerializeField] private Button pauseButton;
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button quitButton;

    [Space]
    [SerializeField] private Pause pause;
    [SerializeField] private Restart restart;
    [SerializeField] private Quit quit;

    private void Awake()
    {
        CloseMenu();

        pauseButton.onClick.AddListener(PauseGame);
        resumeButton.onClick.AddListener(ResumeGame);
        restartButton.onClick.AddListener(OnRestartButton);
        quitButton.onClick.AddListener(OnQuitButton);
    }

    private void TogglePause()
    {
        if(pause.IsGamePaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }

    private void PauseGame()
    {
        pause.PauseGame();
        OpenMenu();
    }

    private void ResumeGame()
    {
        pause.ResumeGame();
        CloseMenu();
    }

    private void OpenMenu()
    {
        menuWindow.SetActive(true);
        pauseButton.gameObject.SetActive(false);
    }

    private void CloseMenu()
    {
        menuWindow.SetActive(false);
        pauseButton?.gameObject.SetActive(true);
    }

    private void OnRestartButton()
    {
        restart.RestartGame();
    }

    private void OnQuitButton()
    {
        quit.QuitGame();
    }

    private void OnEnable()
    {
        InputListener.OnPauseToggled += TogglePause;
    }

    private void OnDisable()
    {
        InputListener.OnPauseToggled -= TogglePause;
    }
}
