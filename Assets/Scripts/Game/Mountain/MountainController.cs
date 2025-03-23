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

        void Start()
        {
            foreach (var levelSO in _levelSO)
            {
                var level = Instantiate(levelSO.Prefab, _content);
                level.transform.localPosition = new Vector3(0, 0, 0);
            }
        }


    }
}
