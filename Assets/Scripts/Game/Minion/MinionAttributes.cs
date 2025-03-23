using System;
using UnityEngine;

namespace Nascimento.Game.Minion
{
    [CreateAssetMenu(fileName = nameof(MinionAttributes), menuName = "Dev/ScriptableObject/" + nameof(MinionAttributes))]
    public class MinionAttributes : ScriptableObject
    {
        [field: Header("Design")]
        [field: SerializeField]
        public float TranslationTime { get; private set; } = 5f;
        [field: SerializeField]
        public float MaxDelay { get; private set; } = 3;
        [field: SerializeField]
        public float MinWalkDistance { get; internal set; } = .2f;

        [SerializeField]
        private float _walkDistance = 3f;
        [SerializeField]
        private AnimationCurve _movementValue;

        internal float GetMovementSpeed(float lerpTime, float distance)
        {
            if (distance < _walkDistance)
            {
                return _movementValue.Evaluate(.9f);
            }
            else
            {
                return _movementValue.Evaluate(lerpTime / TranslationTime) * distance;
            }
        }

        internal float GetStoppedValue()
        {
            return _movementValue.Evaluate(1);
        }
    }
}