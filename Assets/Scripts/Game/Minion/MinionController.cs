using System;
using UnityEngine;

namespace Nascimento.Game.Minion
{

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


        private Vector3 _floorMin;
        private Vector3 _floorMax;
        private Vector3 _floorCenter;
        private Vector3 _currentTarget;
        private Vector3 _startPosition;

        private float _waitTime;
        private float _currentWaitTime;
        private float _lerpTime;
        private float _currentLerpTime;

        void Update()
        {
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
            _charSprite.transform.position = Vector3.Lerp(_startPosition, _currentTarget, t);

            if (t >= 1f)
            {
                _currentTarget = Vector3.zero;
                _animator.SetFloat(SpeedKey, 0);
            }
            else
            {
                _animator.SetFloat(SpeedKey, 1);
            }
        }

        private void GenerateNewTarget()
        {
            float randomX = UnityEngine.Random.Range(_floorMin.x, _floorMax.x);
            float randomY = UnityEngine.Random.Range(_floorMin.y, _floorMax.y);
            _currentTarget = new Vector3(randomX, randomY, _charSprite.transform.position.z);
            _waitTime = UnityEngine.Random.Range(0.5f, _attr.MaxDelay);
            _currentWaitTime = _waitTime;

            // Reset lerp values when setting new target
            _startPosition = _charSprite.transform.position;
            _startPosition.y = _floorCenter.y;
            _currentTarget.y = _floorCenter.y;
            _lerpTime = UnityEngine.Random.Range(1f, _attr.Speed);
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

        internal void Patrol(Vector3 floorMin, Vector3 floorMax, Vector3 center)
        {
            _floorMin = floorMin;
            _floorMax = floorMax;
            _floorCenter = center;
        }
    }
}