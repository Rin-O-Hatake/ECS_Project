using Core.Scripts.AllData.RunTimeData;
using Core.Scripts.Base.Move;
using Core.Scripts.Player.Weapon;
using Experimentation.ECS_Project.Scripts.Enemy;
using Experimentation.ECS_Project.Scripts.Player.Weapon;
using Leopotam.Ecs;

namespace Core.Scripts.Enemy
{
    public class EnemyIdleSystem : IEcsRunSystem
    {
        private EcsFilter<Enemy, AnimatorRef, Idle> calmEnemies;
        private RuntimeData runtimeData;
    
        public void Run()
        {
            foreach (var i in calmEnemies)
            {
                ref var enemy = ref calmEnemies.Get1(i);
                ref var player = ref runtimeData.PlayerEntity.Get<Player.PlayerInit.Player>();
                ref var animatorRef = ref calmEnemies.Get2(i);

                if ((enemy.transform.position - player.playerTransform.position).sqrMagnitude <= enemy.triggerDistance * enemy.triggerDistance)
                {
                    ref var entity = ref calmEnemies.GetEntity(i);
                    entity.Del<Idle>();
                    ref var follow = ref entity.Get<Follow>();
                    follow.Target = runtimeData.PlayerEntity;
                    animatorRef.animator.SetBool("Running", true);
                }
            }
        }
    }
}
