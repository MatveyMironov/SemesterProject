using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace ConstructionSystem
{
    public class ConstructionMenu : MonoBehaviour
    {
        [SerializeField] private GameObject menuWindow;

        [Header("Construction buttons")]
        [SerializeField] private Button constructionButtonPrefab;
        [SerializeField] private Transform content;

        private List<ConstructionButton> _constructionButtons = new();

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

        public void AddConstructionButton(ConstructionBlueprint blueprint)
        {
            Button button = Instantiate(constructionButtonPrefab, content);
            ConstructionButton constructionButton = new(button, blueprint);
            _constructionButtons.Add(constructionButton);
        }

        public void RemoveConstructionButton(ConstructionBlueprint blueprint)
        {
            foreach (var button in _constructionButtons)
            {
                if (button.Blueprint == blueprint)
                {
                    Destroy(button.Button.gameObject);
                    _constructionButtons.Remove(button);
                    return;
                }
            }

            throw new System.Exception("No button found for such turret");
        }

        private class ConstructionButton
        {
            public ConstructionButton(Button button, ConstructionBlueprint blueprint)
            {
                Button = button;
                Blueprint = blueprint;

                button.GetComponentInChildren<TextMeshProUGUI>().text = blueprint.TurretData.TurretName;
                button.onClick.AddListener(blueprint.CallBlueprintSelection);
            }

            public Button Button { get; private set; }
            public ConstructionBlueprint Blueprint { get; private set; }
        }
    }
}
