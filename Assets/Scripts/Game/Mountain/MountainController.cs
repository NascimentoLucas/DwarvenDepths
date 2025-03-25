using Nascimento.Model;
using UnityEngine;
using System.Collections.Generic;
using Nascimento.Game.Level.Controller;
using Nascimento.Game.Minion;
using UnityEditor;

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
        private LevelController[] _levelControllers;

        private float _timer;
        private float _delay = 1f;

        public void Start()
        {
            _minionControllerPool.Initialize(transform);
            _levelControllers = new LevelController[_levelSO.Length];
            List<LevelSO> levelSOs = new List<LevelSO>();
            levelSOs.AddRange(_levelSO);
            _levelSO = levelSOs.ToArray();


            for (int i = 0; i < _levelSO.Length; i++)
            {
                var level = Instantiate(_levelSO[i].Prefab, _content);
                level.Setup(_levelSO[i]);
                level.transform.position = _spawnPoint.position;
                level.name = _levelSO[i].name;
                _levelControllers[i] = level;
                _bag.AddItem(_levelSO[i].Item, 0);
            }


            Vector3 position = _spawnPoint.position;
            for (int i = 0; i < _levelControllers.Length; i++)
            {
                _levelControllers[i].transform.position = position;
                position.y = _levelControllers[i].Cave.FloorCenter.y;
                position.x = _spawnPoint.position.x;
            }
            _scrollManager.SetBottomItem(_levelControllers[_levelControllers.Length - 1].transform);
            
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
            for (int i = 0; i < _levelControllers.Length; i++)
            {
                _levelControllers[i].CraftItem(_bag);
            }
        }
    }
}
