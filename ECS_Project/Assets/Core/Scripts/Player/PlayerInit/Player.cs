using UnityEngine;

namespace Core.Scripts.Player.PlayerInit
{
    public struct Player
    {
        public CharacterController CharacterController;
        public float playerSpeed;
        public Animator playerAnimator;
        public Transform playerTransform;

        public GameObject CameraTarget;
        public bool AnalogMovement;
        public float SpeedChangeRate;
        public float RotationSmoothTime;
        public float RotationSpeed;
    }
}
