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

    public class LevelController : MonoBehaviour, IMinionCHandler, ILevelViewHandler
    {
        [Header("Setup")]
        [SerializeField]
        private CaveController _cave;
        [SerializeField]
        private LevelView _levelView;
        [SerializeField]
        private MinionController _prefabMinion;

        [SerializeField]
        private EnvironmentAttributes _attr;

        private List<MinionController> _minions = new List<MinionController>();
        private LevelSO _levelSO;
        private LevelItemCraft _levelItemCraft;

        public CaveController Cave => _cave;
        private float _minionsRatio = 1;

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

        public void OnButtonPressed()
        {
            var minion = Instantiate(_prefabMinion, transform);
            _minions.Add(minion);
            minion.SetHandler(this);
        }

        public void Setup(LevelSO levelSO)
        {
            _levelSO = levelSO;
            _levelItemCraft = new LevelItemCraft(_levelSO.Item);
            var ratio = _cave.Setup(_attr);
            _levelView.Setup(this, ratio);

            for (int i = 0; i < _levelSO.Item.Components.Length; i++)
            {
                _levelView.AddImage(_levelSO.Item.Components[i].Item.Icon, _levelSO.Item.Components[i].Amount.ToString("00"));
            }
            _levelView.AddLastImage(_levelSO.Item.Icon, 1.ToString("00"));


#if UNITY_EDITOR
            for (int i = 0; i < 10; i++)
            {
                OnButtonPressed();
            }
#endif
        }


        internal void CraftItem(ICraftHandler handler)
        {
            _minionsRatio = 0;
            for (int i = 0; i < _minions.Count; i++)
            {
                _minionsRatio += _minions[i].Ratio;
            }

            _levelView.SetText($"{_minionsRatio}X");

            _levelItemCraft.GetItem(handler, (int)_minionsRatio);
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