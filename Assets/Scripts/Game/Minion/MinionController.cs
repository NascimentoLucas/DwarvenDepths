using System;
using UnityEngine;

namespace Nascimento.Game.Minion
{

    public interface IMinionCHandler
    {
        public Vector3 GetFloorMin();
        public Vector3 GetFloorMax();
        public Vector3 GetFloorCenter();
    }

    public class MinionController : MonoBehaviour
    {
        private const string SpeedKey = "Speed";
        [Header("Setup")]
        [SerializeField]
        private SpriteRenderer _charSprite;
        [SerializeField]
        private MinionAttributes _attr;
        [SerializeField]
        private Animator _animator;


        [Header("Game Design")]
        [SerializeField]
        private float _ratio = 1.25f;

        private Vector3 _currentTarget;
        private Vector3 _startPosition;

        private float _waitTime;
        private float _currentWaitTime;
        private float _lerpTime;
        private float _currentLerpTime;
        private IMinionCHandler _handler;

        public float Ratio => _ratio;



        void Update()
        {
            if (_handler == null) return;

            if (_currentTarget == Vector3.zero)
            {
                if (_currentWaitTime <= 0)
                {
                    GenerateNewTarget();
                }
                else
                {
                    _currentWaitTime -= Time.deltaTime;
                    return;
                }
            }

            _currentLerpTime += Time.deltaTime;
            float t = Mathf.Clamp01(_currentLerpTime / _lerpTime);
            _startPosition.y = _handler.GetFloorCenter().y;
            _currentTarget.y = _handler.GetFloorCenter().y;
            _charSprite.transform.position = Vector3.Lerp(_startPosition, _currentTarget, t);

            if (t >= 1f)
            {
                _currentTarget = Vector3.zero;
                _animator.SetFloat(SpeedKey, _attr.GetStoppedValue());
            }
            else
            {
                _animator.SetFloat(SpeedKey, _attr.GetMovementSpeed(_lerpTime, Vector3.Distance(_startPosition, _currentTarget)));
            }
        }

        private void GenerateNewTarget()
        {
            Vector3 last = _currentTarget;
            do
            {
                float randomX = UnityEngine.Random.Range(_handler.GetFloorMin().x, _handler.GetFloorMax().x);
                float randomY = UnityEngine.Random.Range(_handler.GetFloorMin().y, _handler.GetFloorMax().y);
                _currentTarget = new Vector3(randomX, randomY, _charSprite.transform.position.z);
            } while (Vector3.Distance(last, _currentTarget) < _attr.MinWalkDistance);
            _waitTime = UnityEngine.Random.Range(0.5f, _attr.MaxDelay);
            _currentWaitTime = _waitTime;

            _startPosition = _charSprite.transform.position;
            _startPosition.y = _handler.GetFloorCenter().y;
            _currentTarget.y = _handler.GetFloorCenter().y;
            _lerpTime = UnityEngine.Random.Range(1f, _attr.TranslationTime);
            _currentLerpTime = 0;

            Vector3 scale = _charSprite.transform.localScale;

            if (_currentTarget.x < _startPosition.x)
            {
                scale.x = -Mathf.Abs(scale.x);
            }
            else
            {
                scale.x = Mathf.Abs(scale.x);
            }

            _charSprite.transform.localScale = scale;
        }

        internal void SetHandler(IMinionCHandler handler)
        {
            _handler = handler;
        }
    }
}