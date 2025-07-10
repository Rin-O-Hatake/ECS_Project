using Core.Scripts.AllData.RunTimeData;
using Core.Scripts.Base.Damage;
using Core.Scripts.Base.Move;
using Core.Scripts.Damage;
using Core.Scripts.Player.Weapon;
using Experimentation.ECS_Project.Scripts.Enemy;
using Experimentation.ECS_Project.Scripts.Player.Weapon;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Enemy
{
    public class EnemyFollowSystem : IEcsRunSystem
    {
        private EcsFilter<Enemy, Follow, AnimatorRef> followingEnemies;
        private EcsFilter<Enemy, Follow, AttackMarker> attackingEnemies;
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
                    enemy.meleeAttackDistance * enemy.meleeAttackDistance &&
                    Time.time >= follow.NextAttackTime)
                {
                    follow.NextAttackTime = Time.time + enemy.meleeAttackInterval;
                    animatorRef.animator.SetTrigger("Attack");
                }
            }
            
            foreach (var i in attackingEnemies)
            {
                ref var enemyAttack = ref attackingEnemies.Get1(i);
                ref var followAttack = ref attackingEnemies.Get2(i);
                
                ref var entity = ref followingEnemies.GetEntity(i);
                entity.Del<AttackMarker>();
                
                ref var e = ref ecsWorld.NewEntity().Get<DamageEvent>();
                e.Target = followAttack.Target;
                e.Value = enemyAttack.damage;
                
            }
        }
    }
}
