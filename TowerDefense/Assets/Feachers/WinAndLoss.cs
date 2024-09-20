using UnityEngine;

public class WinAndLoss : MonoBehaviour
{
    [SerializeField] private Pause pause;

    [Space]
    [SerializeField] private GameObject LossMenu;

    private void Awake()
    {
        LossMenu.SetActive(false);
    }

    private void Loss()
    {
        pause.PauseGame();

        LossMenu.SetActive(true);
    }

    private void OnEnable()
    {
        PlayerBase.OnBaseDestroyed += Loss;
    }

    private void OnDisable()
    {
        PlayerBase.OnBaseDestroyed -= Loss;
    }
}
