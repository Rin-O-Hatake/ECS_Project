using Experimentation.ECS_Project.Scripts.Player.PlayerInput;
using Leopotam.Ecs;
using UnityEngine;

namespace Experimentation.ECS_Project.Scripts.Player.PlayerMove
{
    public class PlayerMoveSystem : IEcsRunSystem
    {
        private EcsFilter<PlayerInit.Player, PlayerInputData> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var player = ref filter.Get1(i);
                ref var input = ref filter.Get2(i);
                
                Vector3 direction = (Vector3.forward * input.moveInput.z + Vector3.right * input.moveInput.x).normalized;
                player.playerRigidbody.AddForce(direction * player.playerSpeed);
            }
        }
    }
}
