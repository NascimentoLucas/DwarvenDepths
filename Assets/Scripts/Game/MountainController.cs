using UnityEngine;

namespace Nascimento.Game
{




    public class MountainController : MonoBehaviour
    {
        [Header("Setup.Spwan")]
        [SerializeField]
        private LevelController _prefab;
        [SerializeField]
        private Transform _container;
        [SerializeField]
        private Transform _start;

        [Header("Setup")]
        [SerializeField]
        private ScrollManager _scrollManager;

        [Header("Design")]
        [SerializeField]
        private int _amount = 10;

        void Start()
        {
            Vector3 levelPos = _start.position;
            levelPos -= new Vector3(0, _prefab.Size / 2, 0);
            for (int i = 0; i < _amount; i++)
            {
                LevelController level = Instantiate(_prefab, _container);
                level.transform.position = levelPos;
                levelPos -= new Vector3(0, level.Size, 0);
                level.name = $"Level {i}";
            }

            _scrollManager.SetBottomItem(_container.GetChild(_container.childCount - 1));
        }


#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if (_start != null)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_start.position, .1f);
            }
        }
#endif
    }
}