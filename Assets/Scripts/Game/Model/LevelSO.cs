using Nascimento.Game;
using UnityEngine;

namespace Nascimento.Model
{
    public class LevelSO : ScriptableObject
    {
        [field: Header("Setup")]
        [field: SerializeField]
        public ItemData[] Items { get; private set; }
        [field: SerializeField]
        public LevelController _prefab { get; private set; }
    }
}