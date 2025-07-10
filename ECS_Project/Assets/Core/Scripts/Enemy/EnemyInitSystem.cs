using Core.Scripts.Damage;
using Core.Scripts.Player.Weapon;
using Experimentation.ECS_Project.Scripts.Enemy;
using Experimentation.ECS_Project.Scripts.Player.Weapon;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Enemy
{
    public class EnemyInitSystem : IEcsInitSystem
    {
        private EcsWorld ecsWorld;
    
        public void Init()
        {
            foreach (var enemyView in Object.FindObjectsOfType<EnemyView>())
            {
                var enemyEntity = ecsWorld.NewEntity();

                
                ref var enemy = ref enemyEntity.Get<Enemy>();
                ref var animatorRef = ref enemyEntity.Get<AnimatorRef>();
                ref var health = ref enemyEntity.Get<Health>();

                enemyEntity.Get<Idle>();
                enemyView.Entity = enemyEntity;
                
                health.value = enemyView.StartHealth;
                enemy.damage = enemyView.Damage;
                enemy.meleeAttackDistance = enemyView.MeleeAttackDistance;
                enemy.navMeshAgent = enemyView.NavMeshAgent;
                enemy.transform = enemyView.transform;
                enemy.meleeAttackInterval = enemyView.MeleeAttackInterval;
                enemy.triggerDistance = enemyView.TriggerDistance;
                animatorRef.animator = enemyView.Animator;
            }
        }
    }
}
