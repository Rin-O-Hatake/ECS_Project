using Core.Scripts.Player.PlayerInput;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Player.PlayerAnimation
{
    public class PlayerAnimationSystem : IEcsRunSystem
    {
        private EcsFilter<Core.Scripts.Player.PlayerInit.Player, PlayerInputData> filter;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var player = ref filter.Get1(i);
                ref var input = ref filter.Get2(i);
            
                player.playerAnimator.SetFloat("Horizontal", input.moveInput.x, 0.1f, Time.deltaTime);
                player.playerAnimator.SetFloat("Vertical", input.moveInput.y, 0.1f, Time.deltaTime);
                player.playerAnimator.SetBool("Shooting", input.shootInput);
            }
        }
    }
}
