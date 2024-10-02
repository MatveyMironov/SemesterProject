using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ConstructionMenu : MonoBehaviour
{
    [SerializeField] private GameObject menuWindow;
    [SerializeField] private Transform content;

    private EventSystem _eventSystem;

    private void Awake()
    {
        _eventSystem = EventSystem.current;
    }

    public void OpenMenu()
    {
        menuWindow.SetActive(true);
    }

    public void CloseMenu()
    {
        menuWindow.SetActive(false);
        _eventSystem.SetSelectedGameObject(null);
    }

    [SerializeField] private Button constructionButtonPrefab;

    public void CreateConstructionButton(ConstructionBlueprint blueprint)
    {
        Button button = Instantiate(constructionButtonPrefab, content);
        button.GetComponentInChildren<TextMeshProUGUI>().text = blueprint.TurretData.TurretName;
        button.onClick.AddListener(blueprint.SelectBlueprint);
    }
}
