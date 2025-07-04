using Experimentation.ECS_Project.Scripts.Enemy;
using Leopotam.Ecs;
using Unity.VisualScripting;

namespace Experimentation.ECS_Project.Scripts.Player.Weapon.Bullet
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
                    if (enemyView.entity.IsAlive())
                    {
                        ref var e = ref ecsWorld.NewEntity().Get<DamageEvent>();
                        e.Target = enemyView.entity;
                        e.Value = projectile.damage;
                    }
                }

                projectile.projectileGO.GameObject().SetActive(false);
                filter.GetEntity(i).Destroy();
            }
        }
    
    }
}
