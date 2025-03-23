using System.Collections;
using System.Collections.Generic;
using Nascimento.Game.Minion;
using UnityEngine;

namespace Nascimento.Game
{

    public class LevelController : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private CaveController _cave;

        [SerializeField]
        private MinionController[] _minions;

        [SerializeField]
        private EnvironmentAttributes _attr;

        public CaveController Cave => _cave;


        public void Setup()
        {
            _cave.Setup(_attr);
            foreach (var minion in _minions)
            {
                minion.Patrol(_cave.FloorMin, _cave.FloorMax, _cave.FloorCenter);
            }
        }
    }
}