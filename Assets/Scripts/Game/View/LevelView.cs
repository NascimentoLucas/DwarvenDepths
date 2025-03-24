using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Nascimento.View;

namespace Nascimento.Game.Level.View
{
    public interface ILevelViewHandler
    {
        public void OnButtonPressed();
    }

    public class LevelView : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private ProductionInfoView _prefabIcon;
        [SerializeField]
        private LayoutGroup _iconsContent;
        [SerializeField]
        private TextMeshProUGUI _text;
        [SerializeField]
        private TextMeshProUGUI _equalSymbol;

        private ILevelViewHandler _handler;

        public void Setup(ILevelViewHandler handler, float ratio)
        {
            transform.localScale *= ratio;
            _handler = handler;
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void AddImage(Sprite sprite, string text)
        {
            var image = Instantiate(_prefabIcon, _iconsContent.transform);
            image.Setup(sprite, text);
        }

        public void AddLastImage(Sprite sprite, string text)
        {
            _equalSymbol.gameObject.SetActive(_iconsContent.transform.childCount > 1);
            var image = Instantiate(_prefabIcon, _iconsContent.transform);
            image.Setup(sprite, text);
            _equalSymbol.transform.SetAsLastSibling();
            image.transform.SetAsLastSibling();
        }

        public void OnButtonPressed()
        {
            _handler.OnButtonPressed();
        }
    }
}
