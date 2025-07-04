using Experimentation.ECS_Project.Scripts.AllData.RunTimeData;
using Experimentation.ECS_Project.Scripts.Player.Weapon;
using Leopotam.Ecs;

namespace Experimentation.ECS_Project.Scripts.Enemy
{
    public class PlayerDeathSystem : IEcsRunSystem
    {
        private EcsFilter<Player.PlayerInit.Player, DeathEvent, AnimatorRef> deadPlayers;
        private RuntimeData runtimeData;
        private UI.UI ui;
    
        public void Run()
        {
            foreach (var i in deadPlayers)
            {
                ref var animatorRef = ref deadPlayers.Get3(i);
            
                animatorRef.animator.SetTrigger("Death");
                ui.deathScreen.Show();
                runtimeData.GameOver = true;
            
                deadPlayers.GetEntity(i).Destroy();
            }
        }
    }
}
