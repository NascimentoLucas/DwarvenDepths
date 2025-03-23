using Nascimento.Game.Level.Controller;
using UnityEngine;

namespace Nascimento.Game
{
    public class LevelsController : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private Transform _content;
        [SerializeField]
        private LevelController _levelPrefab;


        void Awake()
        {
            for (int i = 0; i < 20; i++)
            {
                var level = Instantiate(_levelPrefab, _content);
                level.name = $"Level {i}";
            }
        }
    }
}