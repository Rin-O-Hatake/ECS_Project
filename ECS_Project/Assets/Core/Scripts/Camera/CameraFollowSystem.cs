using Core.Scripts.AllData.StaticData;
using Core.Scripts.Player.PlayerInput;
using Experimentation.ECS_Project.Scripts.AllData.SceneData;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Camera
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        private EcsFilter<Player.PlayerInit.Player, PlayerInputData> filter;
        private EcsFilter<Camera> cameraFilter;
        private SceneData sceneData;
        private StaticData staticData;
        
        private const float _threshold = 0.01f;
        
        private float _cinemachineTargetYaw;
        private float _cinemachineTargetPitch;

        public void Run()
        {
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
