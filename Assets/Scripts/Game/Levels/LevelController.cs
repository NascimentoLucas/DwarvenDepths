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
        private EnvironmentAttributes _attr;
        [SerializeField]
        private MinionControllerPool _minionControllerPool;

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
            var minion = _minionControllerPool.Get(transform);
            minion.SetHandler(this);
            _minionsRatio += minion.Ratio;
            _levelView.SetText($"{_minionsRatio}X");
        }


        public void Setup(LevelSO levelSO)
        {
            _minionsRatio = 0;
            _levelSO = levelSO;
            _levelItemCraft = new LevelItemCraft(_levelSO.Item);
            var ratio = _cave.Setup(_attr);
            _levelView.Setup(this, ratio);

            for (int i = _levelSO.Item.Components.Length - 1; i >= 0; i--)
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