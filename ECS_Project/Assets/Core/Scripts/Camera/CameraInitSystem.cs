using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Camera
{
    public class CameraInitSystem : IEcsInitSystem
    {
        private EcsWorld ecsWorld;
        
        public void Init()
        {
            foreach (var cameraView in Object.FindObjectsOfType<CameraView>())
            {
                var cameraEntity = ecsWorld.NewEntity();

                ref var cameraComponent = ref cameraEntity.Get<Camera>();
                
                cameraComponent.cameraTransform = cameraView.CameraTransform;
            }
        }
    }
}
