using UnityEngine;

namespace Experimentation.ECS_Project.Scripts.Player.PlayerInit
{
    public struct Player
    {
        public Rigidbody playerRigidbody;
        public float playerSpeed;
        public Animator playerAnimator;
        public Transform playerTransform;
    }
}
