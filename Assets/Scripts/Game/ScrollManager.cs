using System;
using UnityEngine;

namespace Nascimento.Game
{
    public class ScrollManager : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private Transform _transformToMove;

        [Header("Design")]
        [SerializeField]
        private float _scrollMultiplier = 0.1f;
        [SerializeField]
        private float _smoothTime = 0.1f;

        [Header("Debug")]
        [SerializeField]
        private Vector3 _startPosition;
        [SerializeField]
        private Vector3 _endPosition;


        private Vector3 _targetPosition;
        private Vector3 _currentVelocity;
        private Camera _mainCamera;


        private void Start()
        {
            _targetPosition = _transformToMove.position;
            _startPosition = _transformToMove.position;
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Moved)
                {
                    Vector2 delta = touch.deltaPosition;


                    _targetPosition += new Vector3(0, delta.y, 0) * _scrollMultiplier;
                }
            }
            else
            {
                if (_transformToMove.position.y < _startPosition.y)
                {
                    _targetPosition = _startPosition;
                }

                if (_transformToMove.position.y > _endPosition.y)
                {
                    _targetPosition = _endPosition;
                }
            }



            _transformToMove.position = Vector3.SmoothDamp(
                _transformToMove.position,
                _targetPosition,
                ref _currentVelocity,
                _smoothTime
            );
        }
        internal void SetBottomItem(Transform bottomItem)
        {
            _endPosition = _startPosition;
            _endPosition.y += Vector3.Distance(_startPosition, bottomItem.position);
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(_startPosition, 0.1f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(_endPosition, 0.1f);
        }
#endif
    }
}