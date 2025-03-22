using UnityEngine;

namespace Nascimento.Game
{
    public class LevelController : MonoBehaviour
    {

        [Header("Setup")]
        [SerializeField]
        private Transform _root;

        public float Size => _root.localScale.y;
    }
}