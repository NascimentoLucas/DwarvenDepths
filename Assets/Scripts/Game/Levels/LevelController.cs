using System.Collections;
using System.Collections.Generic;
using Nascimento.Game.Minion;
using UnityEngine;

namespace Nascimento.Game
{

    public class LevelController : MonoBehaviour, IMinionCHandler
    {
        [Header("Setup")]
        [SerializeField]
        private CaveController _cave;

        [SerializeField]
        private MinionController[] _minions;

        [SerializeField]
        private EnvironmentAttributes _attr;

        public CaveController Cave => _cave;

        public Vector3 GetFloorCenter()
        {
            return _cave.FloorCenter;
        }

        public Vector3 GetFloorMax()
        {
            return _cave.FloorMax;
        }

        public Vector3 GetFloorMin()
        {
            return _cave.FloorMin;
        }

        public void Setup()
        {
            _cave.Setup(_attr);
            foreach (var minion in _minions)
            {
                minion.SetHandler(this);
            }
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1f);
        }
#endif
    }
}