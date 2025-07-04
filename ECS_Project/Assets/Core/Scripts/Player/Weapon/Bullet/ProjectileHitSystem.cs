using Core.Scripts.Damage;
using Core.Scripts.Enemy;
using Experimentation.ECS_Project.Scripts.Enemy;
using Experimentation.ECS_Project.Scripts.Player.Weapon;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Player.Weapon.Bullet
{
    public class ProjectileHitSystem : IEcsRunSystem
    {
        private EcsFilter<Projectile, ProjectileHit> filter;
        private EcsWorld ecsWorld;
    
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var projectile = ref filter.Get1(i);
                ref var hit = ref filter.Get2(i);

                if (hit.raycastHit.collider.gameObject.TryGetComponent(out EnemyView enemyView))
                {
                    if (enemyView.Entity.IsAlive())
                    {
                        ref var e = ref ecsWorld.NewEntity().Get<DamageEvent>();
                        e.Target = enemyView.Entity;
                        e.Value = projectile.damage;
                    }
                }

                projectile.projectileGO.SetActive(false);
                filter.GetEntity(i).Destroy();
            }
        }
    
    }
}
