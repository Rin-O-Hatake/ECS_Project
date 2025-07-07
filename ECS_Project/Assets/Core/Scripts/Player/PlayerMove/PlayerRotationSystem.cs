using Core.Scripts.AllData.RunTimeData;
using Core.Scripts.Player.PlayerInput;
using Experimentation.ECS_Project.Scripts.AllData.SceneData;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Player.PlayerMove
{
    public class PlayerRotationSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerInit.Player, PlayerInputData> filter;
        private SceneData sceneData;
        private RuntimeData runtimeData;
        
        private float cameraRotationX;
        private float cameraRotationY;

        public void Run()
        {
            if (runtimeData.IsPaused)
            {
                return;
            }
            
            foreach (var i in filter)
            {
                ref var player = ref filter.Get1(i);
                ref var input = ref filter.Get2(i);

                // Plane playerPlane = new Plane(Vector3.up, player.playerTransform.position);
                // Ray ray = sceneData.mainCamera.ScreenPointToRay(Input.mousePosition);
                // if (!playerPlane.Raycast(ray, out var hitDistance)) continue;

                // player.playerTransform.forward = ray.GetPoint(hitDistance) - player.playerTransform.position;

                // cameraRotationX += input.mouseX * 2;
                // cameraRotationY -= input.mouseY * 2;
                // cameraRotationY = Mathf.Clamp(cameraRotationY, -40, 40);
                //
                // input.CameraRotationX = cameraRotationX;
                // player.playerTransform.localRotation = Quaternion.Euler(cameraRotationY, cameraRotationX, 0f);
            }
        }
    }
}
