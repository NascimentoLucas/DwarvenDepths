using Nascimento.Model;
using UnityEngine;
using System.Collections.Generic;
using Nascimento.Game.Level.Controller;
using Nascimento.Game.Minion;

namespace Nascimento.Game.Mountain
{
    public partial class MountainController : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private LevelSO[] _levelSO;
        [SerializeField]
        private ScrollManager _scrollManager;
        [SerializeField]
        private
        MountainBagController _bag;

        [SerializeField]
        [Header("Setup.SO")]
        private EnvironmentAttributes _env;
        [SerializeField]
        private MinionControllerPool _minionControllerPool;


        [Header("Setup.Spanw")]

        [SerializeField]
        private Transform _content;
        [SerializeField]
        private Transform _spawnPoint;
        private LevelController[] _levels;

        private float _timer;
        private float _delay = 1f;

        public void Start()
        {
            _minionControllerPool.Initialize(transform);
            _levels = new LevelController[_levelSO.Length];
            var sortedLevels = new List<LevelSO>();
            sortedLevels.AddRange(_levelSO);
            sortedLevels.Sort((a, b) => a.MinLvl.CompareTo(b.MinLvl));


            for (int i = 0; i < sortedLevels.Count; i++)
            {
                var level = Instantiate(sortedLevels[i].Prefab, _content);
                level.Setup(sortedLevels[i]);
                level.transform.position = _spawnPoint.position;
                level.name = sortedLevels[i].name;
                _levels[i] = level;
                _bag.AddItem(sortedLevels[i].Item, 0);
            }


            Vector3 position = _spawnPoint.position;
            for (int i = 0; i < _levels.Length; i++)
            {
                _levels[i].transform.position = position;
                position.y = _levels[i].Cave.FloorCenter.y;
                position.x = _spawnPoint.position.x;
            }
            _scrollManager.SetBottomItem(_levels[_levels.Length - 1].transform);
        }

        internal ItemSO GetRandomItem()
        {
            return _levelSO[Random.Range(0, _levelSO.Length)].Item;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            if (_timer >= _env.Delay)
            {
                UpdateMountain();
                _timer = 0f;
            }
        }

        private void UpdateMountain()
        {
            for (int i = 0; i < _levels.Length; i++)
            {
                _levels[i].CraftItem(_bag);
            }
        }
    }
}
