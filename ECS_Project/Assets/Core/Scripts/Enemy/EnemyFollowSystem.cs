using Experimentation.ECS_Project.Scripts.AllData.RunTimeData;
using Experimentation.ECS_Project.Scripts.Player.Weapon;
using Leopotam.Ecs;
using UnityEngine;

namespace Experimentation.ECS_Project.Scripts.Enemy
{
    public class EnemyFollowSystem : IEcsRunSystem
    {
        private EcsFilter<Enemy, Follow, AnimatorRef> followingEnemies;
        private RuntimeData runtimeData;
        private EcsWorld ecsWorld;
        
        public void Run()
        {
            foreach (var i in followingEnemies)
            {
                ref var enemy = ref followingEnemies.Get1(i);
                ref var follow = ref followingEnemies.Get2(i);
                ref var animatorRef = ref followingEnemies.Get3(i);

                if (!follow.Target.IsAlive())
                {
                    ref var entity = ref followingEnemies.GetEntity(i);
                    animatorRef.animator.SetBool("Running", false);
                    entity.Del<Follow>();
                    continue;
                }
            
                ref var transformRef = ref follow.Target.Get<TransformRef>();
                var targetPos = transformRef.transform.position;
                enemy.navMeshAgent.SetDestination(targetPos);
                var direction = (targetPos - enemy.transform.position).normalized;
                direction.y = 0f;
                enemy.transform.forward = direction;

                if ((enemy.transform.position - transformRef.transform.position).sqrMagnitude <
                    enemy.meleeAttackDistance * enemy.meleeAttackDistance && Time.time >= follow.NextAttackTime)
                {
                    follow.NextAttackTime = Time.time + enemy.meleeAttackInterval;
                    animatorRef.animator.SetTrigger("Attack");
                    ref var e = ref ecsWorld.NewEntity().Get<DamageEvent>();
                    e.Target = follow.Target;
                    e.Value = enemy.damage;
                }
            }
        }
    }
}
