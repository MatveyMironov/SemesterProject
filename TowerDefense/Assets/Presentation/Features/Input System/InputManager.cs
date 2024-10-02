using ConstructionSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private ConstructionManager constructionManager;

    public void ToggleConstructionMode()
    {
        if (constructionManager.IsConstructionModeEntered)
        {
            constructionManager.ExitConstructionMode();
        }
        else
        {
            constructionManager.EnterConstructionMode();
        }
    }

    public void SelectConstructionSite()
    {
        constructionManager.SelectConstructionSite();
    }
}
