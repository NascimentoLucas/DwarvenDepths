using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Nascimento.View
{
    public class ProductionInfoView : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TextMeshProUGUI _text;

        public void Setup(Sprite icon, string text)
        {
            _icon.sprite = icon;
            _text.text = text;
        }
    }
}
