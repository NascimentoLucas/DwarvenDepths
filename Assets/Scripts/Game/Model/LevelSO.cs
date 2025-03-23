using Nascimento.Game;
using UnityEngine;

namespace Nascimento.Model
{

    [CreateAssetMenu(fileName = nameof(LevelSO), menuName = "Dev/SO/" + nameof(LevelSO))]
    public class LevelSO : ScriptableObject
    {
        [field: Header("Setup")]
        [field: SerializeField]
        public ItemSO Item { get; private set; }
        [field: SerializeField]
        public LevelController _prefab { get; private set; }

        [field: Header("Design")]

        [field: SerializeField]
        public uint minLvl { get; private set; } = 0;
        [field: SerializeField]
        public uint maxLvl { get; private set; } = uint.MaxValue;
    }
}