using Cinemachine;
using UnityEngine;

namespace Core.Scripts.Camera
{
    public struct Camera
    {
        public Transform CameraTransform;
        public CinemachineVirtualCamera CameraVirtualCamera;
        public GameObject CameraTarget;
        public float BottomClamp;
        public float TopClamp;
        public float CameraAngleOverride;
    }
}
