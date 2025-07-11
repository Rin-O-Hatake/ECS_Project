using Experimentation.ECS_Project.Scripts.Player.Weapon;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Player.Weapon.Bullet
{
    public class ProjectileMoveSystem : IEcsRunSystem
    {
        private EcsFilter<Projectile>.Exclude<ProjectileLifetimeMarker> filter;
    
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var projectile = ref filter.Get1(i);
            
                var position = projectile.projectileGO.transform.position;
                position += projectile.direction * projectile.speed * Time.deltaTime;
                projectile.projectileGO.transform.position = position;
            
                var displacementSinceLastFrame = position - projectile.previousPos;
                var hit = Physics.SphereCast(projectile.previousPos, projectile.radius,
                    displacementSinceLastFrame.normalized, out var hitInfo, displacementSinceLastFrame.magnitude);
                if (hit)
                {
                    ref var entity = ref filter.GetEntity(i);
                    ref var projectileHit = ref entity.Get<ProjectileHit>();
                    projectileHit.raycastHit = hitInfo;
                }

                projectile.previousPos = projectile.projectileGO.transform.position;
                projectile.ProjectileLifetime -= Time.deltaTime;
                if (projectile.ProjectileLifetime <= 0)
                {
                    ref var entity = ref filter.GetEntity(i);
                    entity.Get<ProjectileLifetimeMarker>();
                }
            }
        }
    }
}
