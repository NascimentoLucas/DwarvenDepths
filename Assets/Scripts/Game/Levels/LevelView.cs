using UnityEngine;

namespace Nascimento.Game.Level.View
{
    public class LevelView : MonoBehaviour
    {
        public void Setup(float ratio)
        {
            transform.localScale *= ratio;
        }
    }
}
