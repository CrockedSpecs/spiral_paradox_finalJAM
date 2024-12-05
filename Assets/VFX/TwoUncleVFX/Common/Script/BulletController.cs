using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.VFX;

namespace VFXTools
{
    public enum TowardType
    {
        Forward,
        Right
    }

    public class BulletController : MonoBehaviour
    {
        public float rotationSpeed = 100f;
        public float movementSpeed = 10f;
        public float delayTime = 0f;
        private bool isPlay = false;
        public float time = 1f;
        private float lastTime = 0f;
        private Vector3 startPos;
        public TowardType towardType = TowardType.Forward;
        private Vector3 directionToCenter;
        private Vector3 scale;
        private VisualEffect[] vfxs;
        private TrailRenderer[] trails;
        public float maxDistance = 100f;
        float curDistance = 0f;

        private void Start()
        {
            vfxs = GetComponentsInChildren<VisualEffect>(false);
            trails = GetComponentsInChildren<TrailRenderer>(false);
            startPos = transform.position;
            SetPlay(true);
        }

        private async void SetPlay(bool play)
        {
            isPlay = play;
            if (!isPlay)
            {
                // Destruir efectos visuales si la bala deja de reproducirse
                await DestroyEffects();
            }
        }

        private void Update()
        {
            if (!isPlay) return;

            lastTime += Time.deltaTime;

            if (lastTime > time)
            {
                HandleReset();
                return;
            }

            if (delayTime > lastTime || curDistance > maxDistance)
            {
                return;
            }

            MoveBullet();
        }

        private void HandleReset()
        {
            // Deshabilitar efectos visuales y reiniciar variables
            foreach (var vfx in vfxs)
            {
                if (vfx != null)
                {
                    Destroy(vfx.gameObject); // Destruye los VisualEffect
                }
            }

            foreach (var trail in trails)
            {
                if (trail != null)
                {
                    trail.enabled = false;
                }
            }

            transform.localScale = Vector3.zero;
            transform.position = startPos;
            lastTime = 0f;
            curDistance = 0f;
            transform.localScale = scale;
            isPlay = false;

            Destroy(gameObject); // Destruye la bala
        }

        private void MoveBullet()
        {
            directionToCenter = transform.forward;
            Quaternion targetRotation = Quaternion.LookRotation(directionToCenter);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            if (towardType == TowardType.Forward)
            {
                transform.Translate(Vector3.forward * movementSpeed * Time.deltaTime);
            }
            else if (towardType == TowardType.Right)
            {
                transform.Translate(Vector3.right * movementSpeed * Time.deltaTime);
            }

            curDistance += movementSpeed * Time.deltaTime;
        }

        private async Task DestroyEffects()
        {
            await Task.Delay(500); // Agregar un retraso si es necesario

            // Eliminar efectos visuales
            foreach (var vfx in vfxs)
            {
                if (vfx != null)
                {
                    Destroy(vfx.gameObject);
                }
            }

            // Desactivar trazadores (si no son críticos)
            foreach (var trail in trails)
            {
                if (trail != null)
                {
                    trail.enabled = false;
                }
            }
        }
    }
}
