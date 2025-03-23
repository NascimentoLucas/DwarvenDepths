using System.Collections.Generic;
using Nascimento.Model;
using UnityEngine;

namespace Nascimento.Game.Mountain
{

    public partial class MountainController : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private LevelSO[] _levelSO;

        [SerializeField]
        private Transform _content;
        [SerializeField]
        private Transform _spawnPoint;
        [SerializeField]
        private ScrollManager _scrollManager;


        private LevelController[] _levels;

        void Start()
        {
            _levels = new LevelController[_levelSO.Length];
            var sortedLevels = new List<LevelSO>();
            sortedLevels.AddRange(_levelSO);
            sortedLevels.Sort((a, b) => a.MinLvl.CompareTo(b.MinLvl));


            for (int i = 0; i < sortedLevels.Count; i++)
            {
                var level = Instantiate(sortedLevels[i].Prefab, _content);
                level.Setup();
                level.transform.position = _spawnPoint.position;
                level.name = sortedLevels[i].name;
                _levels[i] = level;
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
    }
}
