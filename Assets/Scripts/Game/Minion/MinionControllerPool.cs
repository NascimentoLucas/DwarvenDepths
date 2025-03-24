using System;
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
        private readonly object _poolLock = new object();

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
            lock (_poolLock)
            {
                var minion = UnityEngine.Object.Instantiate(_prefabMinion, parent);
                minion.gameObject.SetActive(false);
                _pool.Enqueue(minion);
            }
        }


        public MinionController Get(Transform parent)
        {
            lock (_poolLock)
            {
                if (_pool == null)
                {
                    Debug.LogError("Pool not initialized! Call Initialize() first.");
                    return null;
                }

                if (_pool.Count == 0)
                {
                    return null;
                }

                var minion = _pool.Dequeue();
                minion.transform.SetParent(parent);
                minion.gameObject.SetActive(true);
                return minion;
            }
        }

        public void Return(MinionController minion)
        {
            lock (_poolLock)
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

        internal void IncreasePoolSize(Transform parent)
        {
            CreateNewMinion(parent);
        }

        internal int GetPoolSize()
        {
            return _pool.Count;
        }
    }
}