using Core.Scripts.Player.Weapon.Base;
using Core.Scripts.Player.Weapon.Bullet;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Player.Weapon.Reload
{
    public class SpawnProjectileSystem : IEcsRunSystem
    {
        private EcsFilter<Base.Weapon, SpawnProjectile, WeaponRaycastHit> filter;
        private EcsWorld ecsWorld;
    
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var weapon = ref filter.Get1(i);
                ref var weaponHit = ref filter.Get3(i);
            
                var projectileGO = Object.Instantiate(weapon.projectilePrefab, weapon.projectileSocket.position, Quaternion.identity);
                var projectileEntity = ecsWorld.NewEntity();

                ref var projectile = ref projectileEntity.Get<Projectile>();

                projectile.damage = weapon.weaponDamage;
                projectile.direction = weaponHit.Ray.direction.normalized;
                projectile.radius = weapon.projectileRadius;
                projectile.speed = weapon.projectileSpeed;
                projectile.ProjectileLifetime = weapon.ProjectileLifetime;
                projectile.previousPos = projectileGO.transform.position;
                projectile.projectileGO = projectileGO;

                ref var entity = ref filter.GetEntity(i);
                entity.Del<SpawnProjectile>();
            }
        }
    }
}
