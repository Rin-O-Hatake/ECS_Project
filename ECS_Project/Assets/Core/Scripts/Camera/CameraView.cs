using UnityEngine;

namespace Core.Scripts.Camera
{
    public class CameraView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private Transform _cameraTransform;

        #region Properties

        public Transform CameraTransform => _cameraTransform;

        #endregion

        #endregion
    }
}
