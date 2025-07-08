using Core.Scripts.AllData.RunTimeData;
using Core.Scripts.Player.PlayerInput;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Camera
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        private EcsFilter<Player.PlayerInit.Player, PlayerInputData> filter;
        private EcsFilter<Camera> cameraFilter;
        private RuntimeData _runtimeData;
        
        private const float _threshold = 0.01f;
        
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        public void Run()
        {
            if (_runtimeData.IsPaused)
            {
                return; 
            }
            
            foreach (var i in filter)
            {
                foreach (var j in cameraFilter)
                {
                    ref var input = ref filter.Get2(i);
                    ref var camera = ref cameraFilter.Get1(j);
                
                    if (input.lookInput.sqrMagnitude >= _threshold)
                    {
                        float deltaTimeMultiplier = 1.0f;
            
                        _cinemachineTargetYaw += input.lookInput.x * deltaTimeMultiplier;
                        _cinemachineTargetPitch += input.lookInput.y * deltaTimeMultiplier;
                    }
            
                    _cinemachineTargetYaw = ClampAngle(_cinemachineTargetYaw, float.MinValue, float.MaxValue);
                    _cinemachineTargetPitch = ClampAngle(_cinemachineTargetPitch, camera.BottomClamp, camera.TopClamp);

                    camera.CameraTarget.transform.rotation = Quaternion.Euler(_cinemachineTargetPitch + camera.CameraAngleOverride,
                        _cinemachineTargetYaw, 0.0f);
                }
            }
        }
        
        private static float ClampAngle(float lfAngle, float lfMin, float lfMax)
        {
            if (lfAngle < -360f) lfAngle += 360f;
            if (lfAngle > 360f) lfAngle -= 360f;
            return Mathf.Clamp(lfAngle, lfMin, lfMax);
        }
    }
}
