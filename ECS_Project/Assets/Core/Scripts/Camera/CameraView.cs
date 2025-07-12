using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Scripts.Camera
{
    public class CameraView : MonoBehaviour
    {
        #region Fields

        [Title("Camera View")]
        [SerializeField] private Transform _cameraTransform;
        [SerializeField] private CinemachineVirtualCamera _cameraVirtualCamera;
        [SerializeField, Range(-150, 0)] private float _bottomClamp = -30.0f;
        [SerializeField, Range(0, 150)] private float _topClamp = 70.0f;
        [SerializeField, Range(-150, 150)] private float _cameraAngleOverride = 70.0f;
        [SerializeField, Range(0, 150)] private float _autoRotationThreshold = 60.0f;

        #region Properties

        public Transform CameraTransform => _cameraTransform;
        public CinemachineVirtualCamera CameraVirtualCamera => _cameraVirtualCamera;
        public float BottomClamp => _bottomClamp;
        public float TopClamp => _topClamp;
        public float CameraAngleOverride => _cameraAngleOverride;
        public float AutoRotationThreshold => _autoRotationThreshold;

        #endregion

        #endregion
    }
}
