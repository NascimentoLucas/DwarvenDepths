using System;
using System.Collections;
using System.Collections.Generic;
using Nascimento.Game.Minion;
using Nascimento.Model;
using UnityEngine;
using Nascimento.Game.Level.View;

namespace Nascimento.Game.Level.Controller
{
    public interface ICraftHandler
    {
        public bool GetItem(ItemSO item, int amount);
        public void AddItem(ItemSO item, int amount);
    }

    public class LevelController : MonoBehaviour, IMinionCHandler
    {
        [Header("Setup")]
        [SerializeField]
        private CaveController _cave;
        [SerializeField]
        private LevelView _levelView;

        [SerializeField]
        private EnvironmentAttributes _attr;
        private LevelSO _levelSO;
        private LevelItemCraft _levelItemCraft;

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
            _levelItemCraft = new LevelItemCraft(_levelSO.Item);
            var ratio = _cave.Setup(_attr);
            _levelView.Setup(ratio);
        }

        internal void CraftItem(ICraftHandler handler)
        {
            _levelItemCraft.GetItem(handler);
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