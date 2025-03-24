using UnityEngine;
using UnityEngine.UI;

namespace Nascimento.View
{
    public class ForceLayoutRebuilder : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private RectTransform[] _rebuilders;

        void LateUpdate()
        {
            for (int i = 0; i < _rebuilders.Length; i++)
            {
                LayoutRebuilder.ForceRebuildLayoutImmediate(_rebuilders[i]);
            }
        }
    }
}
