using System;
using System.Collections;
using System.Collections.Generic;
using Nascimento.Game.Minion;
using Nascimento.Model;
using UnityEngine;

namespace Nascimento.Game
{
    public interface ICraftHandler
    {
        public bool HasItem(ItemSO item, int amount);
        public void AddItem(ItemSO item, int amount);
    }

    public class LevelController : MonoBehaviour, IMinionCHandler
    {
        [Header("Setup")]
        [SerializeField]
        private CaveController _cave;

        [SerializeField]
        private MinionController[] _minions;

        [SerializeField]
        private EnvironmentAttributes _attr;
        private LevelSO _levelSO;

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

        public void Setup(LevelSO levelSO)
        {
            _levelSO = levelSO;
            _cave.Setup(_attr);
            foreach (var minion in _minions)
            {
                minion.SetHandler(this);
            }
        }

        internal void CraftItem(ICraftHandler handler)
        {
            handler.AddItem(_levelSO.Item, 1);
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