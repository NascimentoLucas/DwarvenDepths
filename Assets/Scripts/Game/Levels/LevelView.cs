using UnityEngine;

namespace Nascimento.Game.Level.View
{
    public interface ILevelViewHandler
    {
        public void OnButtonPressed();
    }

    public class LevelView : MonoBehaviour
    {
        private ILevelViewHandler _handler;

        public void Setup(float ratio, ILevelViewHandler handler)
        {
            transform.localScale *= ratio;
            _handler = handler;
        }

        public void OnButtonPressed()
        {
            _handler.OnButtonPressed();
        }
    }
}
