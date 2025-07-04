using Core.Scripts.AllData.StaticData;
using Experimentation.ECS_Project.Scripts.AllData.SceneData;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Camera
{
    public class CameraFollowSystem : IEcsRunSystem
    {
        private EcsFilter<Experimentation.ECS_Project.Scripts.Player.PlayerInit.Player> filter;
        private EcsFilter<Camera> cameraFilter;
        private SceneData sceneData;
        private StaticData staticData;
        private Vector3 currentVelocity;

        public void Run()
        {
            foreach (var i in filter)
            {
                foreach (var j in cameraFilter)
                {
                    ref var player = ref filter.Get1(i);
                    Transform transformPlayer = player.playerTransform;
                    
                    ref var camera = ref cameraFilter.Get1(i);
                    Transform transformCamera = camera.cameraTransform;
                
                    var wantedRotationAngle = transformPlayer.eulerAngles.y;
                    var wantedHeight =  transformPlayer.position.y + staticData.Height;
                
                    var currentRotationAngle = transformCamera.eulerAngles.y;
                    var currentHeight = transformCamera.position.y;
                
                    currentRotationAngle = Mathf.LerpAngle(currentRotationAngle, wantedRotationAngle, staticData.RotationDamping * Time.deltaTime);

                    currentHeight = Mathf.Lerp(currentHeight, wantedHeight, staticData.HeightDamping * Time.deltaTime);

                    Quaternion currentRotation = Quaternion.Euler(0, currentRotationAngle, 0);

                    transformCamera.position = transformPlayer.position;
                    transformCamera.position -= currentRotation * Vector3.forward * staticData.Distance;

                    transformCamera.position = new Vector3(transformCamera.position.x, currentHeight, transformCamera.position.z);

                    transformCamera.LookAt(transformPlayer);
                
                
                    // var currentPos = sceneData.mainCamera.transform.position;
                    // currentPos = Vector3.SmoothDamp(currentPos, player.playerTransform.position + staticData.FollowOffset, ref currentVelocity, staticData.SmoothTime);
                    // sceneData.mainCamera.transform.position = currentPos; 
                }
            }
        }
    }
}
