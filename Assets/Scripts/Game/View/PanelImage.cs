using UnityEngine;
using UnityEngine.UI;

namespace Nascimento.View
{
    public class PanelImage : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private Image _background;

        public void Setup(Sprite icon)
        {
            _icon.sprite = icon;
        }
    }
}
