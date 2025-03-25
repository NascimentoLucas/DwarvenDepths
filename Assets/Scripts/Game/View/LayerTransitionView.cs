using UnityEngine;

namespace Nascimento.Game.Minion
{
    public class LayerTransitionView : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private int _layerIndex;
        [SerializeField]
        private float _duration;
        private float _time;

        private float Lerp => (Time.timeSinceLevelLoad - _time) / _duration;


        private void OnEnable()
        {
            _time = Time.timeSinceLevelLoad;
        }

        private void Update()
        {
            _animator.SetLayerWeight(_layerIndex, Mathf.Lerp(1, 0, Lerp));
        }
    }
}