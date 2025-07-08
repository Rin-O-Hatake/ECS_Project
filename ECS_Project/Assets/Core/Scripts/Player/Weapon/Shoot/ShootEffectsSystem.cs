using Core.Scripts.Player.Weapon.Base;
using Leopotam.Ecs;

namespace Core.Scripts.Player.Weapon.Shoot
{
    public class ShootEffectsSystem : IEcsRunSystem
    {
        private EcsFilter<Base.Weapon, WeaponEffects, ImpulseShootMarker> _filter;
        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var entity = ref _filter.GetEntity(i);
                entity.Del<ImpulseShootMarker>();
                
                ref var weaponEffect = ref _filter.Get2(i);
                
                weaponEffect.ImpulseListener?.GenerateImpulse(weaponEffect.RecoilDirection * weaponEffect.RecoilForce);
            }
        }
    }
}
