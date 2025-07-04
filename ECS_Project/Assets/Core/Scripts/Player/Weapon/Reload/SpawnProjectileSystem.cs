using Experimentation.ECS_Project.Scripts.Player.Weapon.Bullet;
using Leopotam.Ecs;
using Unity.VisualScripting;
using UnityEngine;

namespace Experimentation.ECS_Project.Scripts.Player.Weapon
{
    public class SpawnProjectileSystem : IEcsRunSystem
    {
        private EcsFilter<Base.Weapon, SpawnProjectile> filter;
        private EcsWorld ecsWorld;
    
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var weapon = ref filter.Get1(i);
            
                var projectileGO = Object.Instantiate((Object)weapon.projectilePrefab, weapon.projectileSocket.position, Quaternion.identity);
                var projectileEntity = ecsWorld.NewEntity();

                ref var projectile = ref projectileEntity.Get<Projectile>();

                projectile.damage = weapon.weaponDamage;
                projectile.direction = weapon.projectileSocket.forward;
                projectile.radius = weapon.projectileRadius;
                projectile.speed = weapon.projectileSpeed;
                projectile.previousPos = projectileGO.GameObject().transform.position;
                projectile.projectileGO = projectileGO;

                ref var entity = ref filter.GetEntity(i);
                entity.Del<SpawnProjectile>();
            }
        }
    }
}
