using Experimentation.ECS_Project.Scripts.Player.Weapon.Bullet;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace Experimentation.ECS_Project.Scripts.Player.Weapon
{
    public class ProjectileMoveSystem : IEcsRunSystem
    {
        private EcsFilter<Projectile> filter;
    
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var projectile = ref filter.Get1(i);
            
                var position = projectile.projectileGO.GameObject().transform.position;
                position += projectile.direction * projectile.speed * Time.deltaTime;
                projectile.projectileGO.GameObject().transform.position = position;
            
                var displacementSinceLastFrame = position - projectile.previousPos;
                var hit = Physics.SphereCast(projectile.previousPos, projectile.radius,
                    displacementSinceLastFrame.normalized, out var hitInfo, displacementSinceLastFrame.magnitude);
                if (hit)
                {
                    ref var entity = ref filter.GetEntity(i);
                    ref var projectileHit = ref entity.Get<ProjectileHit>();
                    projectileHit.raycastHit = hitInfo;
                }

                projectile.previousPos = projectile.projectileGO.GameObject().transform.position;
            }
        }
    }
}
