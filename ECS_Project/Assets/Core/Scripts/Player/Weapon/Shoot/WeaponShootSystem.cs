using Core.Scripts.Player.Weapon.Base;
using Core.Scripts.Player.Weapon.Reload;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Player.Weapon.Shoot
{
    public class WeaponShootSystem : IEcsRunSystem
    {
        private EcsFilter<Base.Weapon, Shoot> filter;
        private Experimentation.ECS_Project.Scripts.UI.UI ui;
    
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var weapon = ref filter.Get1(i);

                ref var entity = ref filter.GetEntity(i);
                entity.Del<Shoot>();
                entity.Get<ImpulseShootMarker>();
            
                if (weapon.currentInMagazine > 0)
                {
                    weapon.currentInMagazine--;
                    
                    if (weapon.owner.Has<Core.Scripts.Player.PlayerInit.Player>())
                    {
                        ui.gameScreen.SetCurrentInMagazine(weapon.currentInMagazine);
                    }
                
                    ref var spawnProjectile = ref entity.Get<SpawnProjectile>();
                    return;
                }

                ref var reload = ref entity.Get<TryReload>();
            }
        }
    }
}
