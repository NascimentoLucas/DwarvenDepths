using UnityEngine;

namespace Nascimento.Game.Minion
{
    [CreateAssetMenu(fileName = nameof(MinionAttributes), menuName = "Dev/ScriptableObject/" + nameof(MinionAttributes))]
    public class MinionAttributes : ScriptableObject
    {
        [field: Header("Design")]
        [field: SerializeField]
        public float Speed { get; private set; } = 2f;
        [field: SerializeField]
        public float MaxDelay { get; private set; } = 3;
    }
}