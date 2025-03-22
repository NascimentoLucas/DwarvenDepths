using UnityEngine;

namespace Nascimento.Game
{
    [CreateAssetMenu(fileName = nameof(EnvironmentAttributes), menuName = "Dev/ScriptableObject/" + nameof(EnvironmentAttributes))]
    public class EnvironmentAttributes : ScriptableObject
    {
        public float LevelWidthPadding = 1.01f;
    }
}