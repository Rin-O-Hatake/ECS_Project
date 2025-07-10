using Leopotam.Ecs;
using Object = UnityEngine.Object;

namespace Core.Scripts.Player.Weapon.Bullet
{
    public class DestroyerProjectileSystem : IEcsRunSystem
    {
        #region Fields

        private EcsFilter<Projectile, ProjectileLifetimeMarker> filter;

        #endregion
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var entity = ref filter.GetEntity(i);
                ref var projectile = ref filter.Get1(i);
                // entity.Destroy();
                Object.Destroy(projectile.projectileGO);
            }
        }
    }
}
