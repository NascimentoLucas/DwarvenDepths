using UnityEngine;
using UnityEngine.UI;

namespace Nascimento.View
{
    public class TimerImage : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private Image _image;
        [SerializeField]
        private Color _startColor;
        [SerializeField]
        private Color _endColor;


        public void SetTimer(float lerp)
        {
            _image.fillAmount = lerp;
            _image.color = Color.Lerp(_startColor, _endColor, lerp);
        }
    }
}
