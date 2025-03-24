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
        private bool _hasRemoved = true;
        public float MinionsRatio
        {
            get => _minionsRatio;
            set
            {
                _minionsRatio = value;
                _levelView.SetText($"{MinionsRatio.ToString("00")}X");
            }
        }


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
            MinionsRatio = 0;
            _levelSO = levelSO;
            _levelItemCraft = new LevelItemCraft(_levelSO.Item);
            var ratio = _cave.Setup(_attr);
            _levelView.Setup(this, ratio);

            for (int i = _levelSO.Item.Components.Length - 1; i >= 0; i--)
            {
                _levelView.AddImage(_levelSO.Item.Components[i].Item.Icon, _levelSO.Item.Components[i].Amount.ToString("00"));
            }
            _levelView.AddLastImage(_levelSO.Item.Icon, 1.ToString("00"));

        }


        internal void CraftItem(ICraftHandler handler)
        {
            _levelItemCraft.GetItem(handler, (int)MinionsRatio);
        }

#if UNITY_EDITOR
        void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, 1f);
        }
#endif


        public void OnAddButtonPressed()
        {
            if (_minionControllerPool.Get(transform) is MinionController minion)
            {
                minion.SetHandler(this);
                MinionsRatio += minion.Ratio;
            }
        }

        public void UnspawnMinion(MinionController minionController)
        {
            if (_hasRemoved) return;
            _hasRemoved = true;
            MinionsRatio -= minionController.Ratio;
            _minionControllerPool.Return(minionController);
        }

        public void OnMinusButtonPressed()
        {
            _hasRemoved = false;
            MinionObserver.UnspawnMinion(transform);
        }
    }
}