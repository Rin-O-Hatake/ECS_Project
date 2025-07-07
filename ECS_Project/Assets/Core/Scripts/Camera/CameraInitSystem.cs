using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Camera
{
    public class CameraInitSystem : IEcsInitSystem
    {
        private EcsWorld ecsWorld;
        private EcsFilter<Player.PlayerInit.Player> filter;
        
        public void Init()
        {
            foreach (var cameraView in Object.FindObjectsOfType<CameraView>())
            {
                var cameraEntity = ecsWorld.NewEntity();

                ref var cameraComponent = ref cameraEntity.Get<Camera>();
                
                cameraComponent.CameraVirtualCamera = cameraView.CameraVirtualCamera;
                cameraComponent.CameraTransform = cameraView.CameraTransform;
                cameraComponent.BottomClamp = cameraView.BottomClamp;
                cameraComponent.TopClamp = cameraView.TopClamp;
                cameraComponent.CameraAngleOverride = cameraView.CameraAngleOverride;
                
                foreach (var i in filter)
                {
                    ref var player = ref filter.Get1(i);
                    cameraComponent.CameraVirtualCamera.Follow = player.CameraTarget.transform;
                    cameraComponent.CameraTarget = player.CameraTarget;
                }
            }
        }
    }
}
