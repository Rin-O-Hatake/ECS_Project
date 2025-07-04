using Core.Scripts.AllData.RunTimeData;
using Core.Scripts.Damage;
using Experimentation.ECS_Project.Scripts.Enemy;
using Experimentation.ECS_Project.Scripts.Player.Weapon;
using Leopotam.Ecs;

namespace Core.Scripts.Enemy
{
    public class PlayerDeathSystem : IEcsRunSystem
    {
        private EcsFilter<Experimentation.ECS_Project.Scripts.Player.PlayerInit.Player, DeathEvent, AnimatorRef> deadPlayers;
        private RuntimeData runtimeData;
        private Experimentation.ECS_Project.Scripts.UI.UI ui;
    
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
