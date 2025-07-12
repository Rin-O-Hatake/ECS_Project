using Core.Scripts.Player.PlayerInput;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Player.PlayerMove
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        #region Fields

        private EcsFilter<PlayerInit.Player, PlayerInputData> filter;
        private EcsFilter<Camera.Camera> filterCamera;

        private float _speed;
        private float _targetRotation;
        private float _rotationVelocity;

        #endregion

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var player = ref filter.Get1(i);
                ref var input = ref filter.Get2(i);

                float targetSpeed = player.playerSpeed;


                if (input.moveInput == Vector2.zero)
                {
                    targetSpeed = 0.0f;
                }

                var currentHorizontalSpeed = new Vector3(player.CharacterController.velocity.x, 0.0f, player.CharacterController.velocity.z).magnitude;

                float speedOffset = 0.1f;
                float inputMagnitude = player.AnalogMovement ? input.moveInput.magnitude : 1f;

                if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                    currentHorizontalSpeed > targetSpeed + speedOffset)
                {
                    _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                        Time.deltaTime * player.SpeedChangeRate);

                    _speed = Mathf.Round(_speed * 1000f) / 1000f;
                }
                else
                {
                    _speed = targetSpeed;
                }

                Vector3 inputDirection = new Vector3(input.moveInput.x, 0.0f, input.moveInput.y).normalized;

                foreach (var j in filterCamera)
                {
                    ref var camera = ref filterCamera.Get1(j);
                    if (input.moveInput != Vector2.zero)
                    {
                        _targetRotation = camera.CameraTransform.eulerAngles.y;

                        float rotation = Mathf.SmoothDampAngle(
                            player.playerTransform.eulerAngles.y,
                            _targetRotation,
                            ref _rotationVelocity,
                            player.RotationSmoothTime
                        );

                        player.playerTransform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                    }   
                }

                Vector3 worldMoveDirection = player.playerTransform.TransformDirection(inputDirection);
                player.CharacterController.Move(worldMoveDirection * (_speed * Time.deltaTime));
            }
        }
    }
}
