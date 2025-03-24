using System.Collections.Generic;
using UnityEngine;

namespace Nascimento.Game.Minion
{
    [CreateAssetMenu(fileName = nameof(MinionControllerPool), menuName = "Dev/SO/" + nameof(MinionControllerPool))]
    public class MinionControllerPool : ScriptableObject
    {
        [Header("Setup")]
        [SerializeField]
        private MinionController _prefabMinion;

        [SerializeField]
        private int _initialSize = 10;

        private Queue<MinionController> _pool;

        public void Initialize(Transform parent)
        {
            _pool = new Queue<MinionController>();
            InitializePool(parent);
        }

        private void InitializePool(Transform parent)
        {
            for (int i = 0; i < _initialSize; i++)
            {
                CreateNewMinion(parent);
            }
        }

        private void CreateNewMinion(Transform parent)
        {
            var minion = UnityEngine.Object.Instantiate(_prefabMinion, parent);
            minion.gameObject.SetActive(false);
            _pool.Enqueue(minion);
        }

        public MinionController Get(Transform parent)
        {
            if (_pool == null)
            {
                Debug.LogError("Pool not initialized! Call Initialize() first.");
                return null;
            }

            if (_pool.Count == 0)
            {
                CreateNewMinion(parent);
            }

            var minion = _pool.Dequeue();
            minion.transform.SetParent(parent);
            minion.gameObject.SetActive(true);
            return minion;
        }

        public void Return(MinionController minion)
        {
            if (_pool == null)
            {
                Debug.LogError("Pool not initialized! Call Initialize() first.");
                return;
            }

            minion.gameObject.SetActive(false);
            _pool.Enqueue(minion);
        }
    }
}