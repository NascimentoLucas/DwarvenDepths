using System;
using UnityEngine;

namespace Nascimento.Game.Minion
{
    public static class MinionObserver
    {
        public static event Action<Transform> OnUnspawnMinion;

        public static void UnspawnMinion(Transform transform)
        {
            OnUnspawnMinion?.Invoke(transform);
        }
    }

    public interface IMinionCHandler
    {
        public Vector3 GetFloorMin();
        public Vector3 GetFloorMax();
        public Vector3 GetFloorCenter();
        void UnspawnMinion(MinionController minionController);
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

        private Vector3 _endPosition;
        private Vector3 _startPosition;

        private float _waitTime;
        private float _currentWaitTime;
        private float _lerpTime;
        private float _currentLerpTime;
        private IMinionCHandler _handler;

        public float Ratio => _ratio;

        private void OnEnable()
        {
            MinionObserver.OnUnspawnMinion += OnUnspawnMinion;
        }

        private void OnDisable()
        {
            MinionObserver.OnUnspawnMinion -= OnUnspawnMinion;
        }

        void OnDestroy()
        {
            MinionObserver.OnUnspawnMinion -= OnUnspawnMinion;
        }

        void Update()
        {
            if (_handler == null) return;

            if (_endPosition == Vector3.zero)
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
            _endPosition.y = _handler.GetFloorCenter().y;
            _charSprite.transform.position = Vector3.Lerp(_startPosition, _endPosition, t);

            if (t >= 1f)
            {
                _endPosition = Vector3.zero;
                _animator.SetFloat(SpeedKey, _attr.GetStoppedValue());
            }
            else
            {
                _animator.SetFloat(SpeedKey, _attr.GetMovementSpeed(_lerpTime, Vector3.Distance(_startPosition, _endPosition)));
            }
        }

        private void GenerateNewTarget()
        {
            Vector3 last = _endPosition;
            do
            {
                float randomX = UnityEngine.Random.Range(_handler.GetFloorMin().x, _handler.GetFloorMax().x);
                float randomY = UnityEngine.Random.Range(_handler.GetFloorMin().y, _handler.GetFloorMax().y);
                _endPosition = new Vector3(randomX, randomY, _charSprite.transform.position.z);
            } while (Vector3.Distance(last, _endPosition) < _attr.MinWalkDistance);
            _waitTime = UnityEngine.Random.Range(0.5f, _attr.MaxDelay);
            _currentWaitTime = _waitTime;

            _startPosition = _charSprite.transform.position;
            _startPosition.y = _handler.GetFloorCenter().y;
            _endPosition.y = _handler.GetFloorCenter().y;
            _lerpTime = UnityEngine.Random.Range(1f, _attr.TranslationTime);
            _currentLerpTime = 0;

            Vector3 scale = _charSprite.transform.localScale;

            if (_endPosition.x < _startPosition.x)
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
            _startPosition = _handler.GetFloorCenter();
            _charSprite.transform.position = _startPosition;
            _endPosition = _startPosition;

        }

        private void OnUnspawnMinion(Transform possibleParent)
        {
            if (transform.parent.GetInstanceID().Equals(possibleParent.GetInstanceID()))
            {
                _handler.UnspawnMinion(this);
            }
        }
    }
}