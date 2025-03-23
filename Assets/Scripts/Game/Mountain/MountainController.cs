using Nascimento.Model;
using TMPro.EditorUtilities;
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


        [Header("Setup")]
        [SerializeField]
        private LevelController[] _levels;

        void Start()
        {
            _levels = new LevelController[_levelSO.Length];
            for (int i = 0; i < _levelSO.Length; i++)
            {
                var level = Instantiate(_levelSO[i].Prefab, _content);
                level.Setup();
                level.transform.position = _spawnPoint.position;
                level.name = _levelSO[i].name;
                _levels[i] = level;
            }
        }

        void FixedUpdate()
        {
            Vector3 position = _spawnPoint.position;
            for (int i = 0; i < _levels.Length; i++)
            {
                _levels[i].transform.position = position;
                position.y = _levels[i].Cave.FloorCenter.y;
                position.x = _spawnPoint.position.x;
            }

        }
    }
}
