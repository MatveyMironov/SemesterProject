using TMPro;
using UnityEngine;

namespace ResourceSystem
{
    public class ResourceIndicator : MonoBehaviour
    {
        [SerializeField] private ResourceManager resourceManager;
        [SerializeField] private TextMeshProUGUI resourceText;

        private void Start()
        {
            DisplayResourceAmount();
        }

        private void DisplayResourceAmount()
        {
            resourceText.text = $"{resourceManager.ResourceAmount}";
        }

        private void OnEnable()
        {
            resourceManager.OnResourceAmountChanged += DisplayResourceAmount;
        }

        private void OnDisable()
        {
            resourceManager.OnResourceAmountChanged -= DisplayResourceAmount;
        }
    }
}
