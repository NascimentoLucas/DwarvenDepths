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
        public LevelController Prefab { get; private set; }

        [field: Header("Design")]

        [field: SerializeField]
        public uint MinLvl { get; private set; } = 0;
        [field: SerializeField]
        public uint MaxLvl { get; private set; } = uint.MaxValue;
    }
}