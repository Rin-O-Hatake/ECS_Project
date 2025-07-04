using Experimentation.ECS_Project.Scripts.AllData.SceneData;
using Experimentation.ECS_Project.Scripts.AllData.StaticData;
using Leopotam.Ecs;
using UnityEngine;

namespace Experimentation.ECS_Project.Scripts.Player.Camera
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerInit.Player> filter;
        private SceneData sceneData;
        private StaticData staticData;
        private Vector3 currentVelocity;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var player = ref filter.Get1(i);
            
                var currentPos = sceneData.mainCamera.transform.position;
                currentPos = Vector3.SmoothDamp(currentPos, player.playerTransform.position + staticData.FollowOffset, ref currentVelocity, staticData.SmoothTime);
                sceneData.mainCamera.transform.position = currentPos;
            }
        }
    }
}
