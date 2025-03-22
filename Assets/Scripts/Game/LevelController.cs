using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Nascimento.Game
{

    public class LevelController : MonoBehaviour
    {
        [Header("Setup")]
        [SerializeField]
        private SpriteRenderer _background;
        [SerializeField]
        private EnvironmentAttributes _attr;


        void Start()
        {
            Camera cam = Camera.main;
            if (cam == null) return;

            float spriteWidth = _background.bounds.size.x;
            float cameraHeight = cam.orthographicSize * 2f;
            float cameraWidth = cameraHeight * cam.aspect;
            float width = cameraWidth / spriteWidth * _attr.LevelWidthPadding;
            transform.localScale = new Vector3(width, transform.localScale.y, transform.localScale.z);
        }
    }
}