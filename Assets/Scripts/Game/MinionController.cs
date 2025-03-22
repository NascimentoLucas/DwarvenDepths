using System;
using UnityEngine;

namespace Nascimento.Game.Minion
{

    public class MinionController : MonoBehaviour
    {

        [Header("Setup")]
        [SerializeField]
        private SpriteRenderer _charSprite;
        [SerializeField]
        private MinionAttributes _attr;


        private Vector3 _floorMin;
        private Vector3 _floorMax;
        private Vector3 _currentTarget;

        private float _waitTime;
        private float _currentWaitTime;

        void Update()
        {
            if (_currentTarget == Vector3.zero)
            {
                if (_currentWaitTime <= 0)
                {
                    float randomX = UnityEngine.Random.Range(_floorMin.x, _floorMax.x);
                    float randomY = UnityEngine.Random.Range(_floorMin.y, _floorMax.y);
                    _currentTarget = new Vector3(randomX, randomY, transform.position.z);
                    _waitTime = UnityEngine.Random.Range(0.5f, _attr.MaxDelay);
                    _currentWaitTime = _waitTime;
                }
                else
                {
                    _currentWaitTime -= Time.deltaTime;
                    return;
                }
            }

            transform.position = Vector3.Lerp(transform.position, _currentTarget, _attr.Speed * Time.deltaTime);

            if (Vector3.Distance(transform.position, _currentTarget) < 0.1f)
            {
                _currentTarget = Vector3.zero;
            }
        }

        internal void Patrol(Vector3 floorMin, Vector3 floorMax)
        {
            _floorMin = floorMin;
            _floorMax = floorMax;
        }
    }
}