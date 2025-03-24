using TMPro;
using UnityEngine;
using UnityEngine.UI;

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
        private Image _image;
        [SerializeField]
        private TextMeshProUGUI _text;

        private ILevelViewHandler _handler;

        public void Setup(ILevelViewHandler handler, float ratio, Sprite sprite)
        {
            transform.localScale *= ratio;
            _handler = handler;
            _image.sprite = sprite;
        }

        public void SetText(string text)
        {
            _text.text = text;
        }

        public void OnButtonPressed()
        {
            _handler.OnButtonPressed();
        }
    }
}
