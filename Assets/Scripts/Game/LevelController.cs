using System.Collections;
using System.Collections.Generic;
using Nascimento.Game.Minion;
using UnityEngine;

namespace Nascimento.Game
{

    public class LevelController : MonoBehaviour
    {
        [Header("Setup.SpriteRenderer")]
        [SerializeField]
        private SpriteRenderer _background;
        [SerializeField]
        private SpriteRenderer _floor;

        [Header("Setup")]
        [SerializeField]
        private MinionController[] _minions;

        [SerializeField]
        private EnvironmentAttributes _attr;
        private Vector3 FloorMax => _floor.bounds.max;
        private Vector3 FloorCenter => _floor.bounds.center;
        private Vector3 FloorMin => _floor.bounds.min;


        void Start()
        {
            Camera cam = Camera.main;
            if (cam == null) return;

            float spriteWidth = _background.bounds.size.x;
            float cameraHeight = cam.orthographicSize * 2f;
            float cameraWidth = cameraHeight * cam.aspect;
            float width = cameraWidth / spriteWidth * _attr.LevelWidthPadding;
            float ratio = width / _background.transform.localScale.x;
            _background.transform.localScale = new Vector3(width, _background.transform.localScale.y * ratio, _background.transform.localScale.z);

            foreach (var minion in _minions)
            {
                minion.Patrol(FloorMin, FloorMax, FloorCenter);
            }
        }


#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (_floor != null)
            {
                float size = .5f;
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(FloorMin, size);
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(FloorCenter, size);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(FloorMax, size);
            }
        }
#endif  
    }
}