using Nascimento.SO;
using NaughtyAttributes;
using UnityEngine;

namespace Nascimento.Game
{
    public class FitSpriteToCamera : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private SpriteRenderer _render;
        [SerializeField]
        private EnvironmentAttributes _attr;


        void Awake()
        {
            FitToCamera();
        }

        [Button]
        public void FitToCamera()
        {
            Camera cam = Camera.main;
            if (cam == null) return;

            float spriteWidth = _render.bounds.size.x;
            float cameraHeight = cam.orthographicSize * 2f;
            float cameraWidth = cameraHeight * cam.aspect;
            float width = cameraWidth / spriteWidth * _attr.LevelWidthPadding;
            transform.localScale = new Vector3(width, transform.localScale.y, transform.localScale.z);
        }
    }
}