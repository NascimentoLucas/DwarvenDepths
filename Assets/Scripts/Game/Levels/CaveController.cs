using UnityEngine;

namespace Nascimento.Game
{
    public class CaveController : MonoBehaviour
    {
        [Header("Setup.SpriteRenderer")]
        [SerializeField]
        private SpriteRenderer _root;
        [SerializeField]
        private SpriteRenderer _floor;
        [SerializeField]
        private SpriteRenderer _background;

        public Vector3 FloorMax => _floor.bounds.max;
        public Vector3 FloorCenter => _floor.bounds.center;
        public Vector3 FloorMin => _floor.bounds.min;
        public float Size => _background.bounds.size.y;


        public void Setup(EnvironmentAttributes attr)
        {
            Camera cam = Camera.main;
            if (cam == null) return;


            float spriteWidth = _root.bounds.size.x;
            float cameraHeight = cam.orthographicSize * 2f;
            float cameraWidth = cameraHeight * cam.aspect;
            float width = cameraWidth / spriteWidth * attr.LevelWidthPadding;
            float ratio = width / _root.transform.localScale.x;
            _root.transform.localScale = new Vector3(width, _root.transform.localScale.y * ratio, _root.transform.localScale.z);
        }


#if UNITY_EDITOR
        void OnDrawGizmosSelected()
        {
            if (_floor != null)
            {
                float size = .5f;
                Gizmos.color = Color.green;
                Gizmos.DrawWireSphere(FloorMin, size);
                Gizmos.color = Color.yellow;
                Gizmos.DrawWireSphere(FloorCenter, size);
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(FloorMax, size);
            }
        }
#endif  
    }
}