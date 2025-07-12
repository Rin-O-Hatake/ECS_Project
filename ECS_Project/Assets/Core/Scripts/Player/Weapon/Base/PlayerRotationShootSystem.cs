using Experimentation.ECS_Project.Scripts.Player.Weapon;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Player.Weapon.Base
{
    public class PlayerRotationShootSystem : IEcsRunSystem
    {
        #region Fields

        private EcsFilter<PlayerInit.Player, HasWeapon> _playerFilter;
        private EcsFilter<Camera.Camera> _cameraFilter;
        private float _targetRotation;
        private float _rotationVelocity;
        private bool _isShooting;

        #endregion
        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                foreach (var j in _cameraFilter)
                {
                    ref var player = ref _playerFilter.Get1(i);
                    ref var camera = ref _cameraFilter.Get1(j);
                    ref var hasWeapon = ref _playerFilter.Get2(i);

                    ChangeRotate();
                    
                    if (!hasWeapon.weapon.Has<Shoot.Shoot>())
                    {
                        return;
                    }
                    
                    float angleDifference = Mathf.DeltaAngle(player.playerTransform.eulerAngles.y, camera.CameraTransform.eulerAngles.y);
                    
                    if (Mathf.Abs(angleDifference) > camera.AutoRotationThreshold)
                    {
                        _isShooting = true;
                    }
                }
            }
        }


        private void ChangeRotate()
        {
            foreach (var i in _playerFilter)
            {
                foreach (var j in _cameraFilter)
                {
                    ref var player = ref _playerFilter.Get1(i);
                    ref var camera = ref _cameraFilter.Get1(j);

                    if (!_isShooting)
                    {
                        return;
                    }
                    
                    float angleDifference = Mathf.DeltaAngle(player.playerTransform.eulerAngles.y, camera.CameraTransform.eulerAngles.y);

                    if (Mathf.Abs(angleDifference) > 1)
                    {
                        float rotationDirection = Mathf.Sign(angleDifference);

                        float targetRotation = player.playerTransform.eulerAngles.y + rotationDirection * player.RotationSpeed * Time.deltaTime;
                        player.playerTransform.eulerAngles = new Vector3(player.playerTransform.eulerAngles.x, targetRotation, player.playerTransform.eulerAngles.z);
                        return;
                    }

                    _isShooting = false;
                }
            }
        }
    }
}
