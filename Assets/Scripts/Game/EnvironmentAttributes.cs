using UnityEngine;

namespace Nascimento.SO
{
    [CreateAssetMenu(fileName = nameof(EnvironmentAttributes), menuName = "Dev/SO/" + nameof(EnvironmentAttributes))]
    public class EnvironmentAttributes : ScriptableObject
    {
        [field: Header("Setup")]
        [field: SerializeField]
        public float LevelWidthPadding = 1.1f;
    }
}
