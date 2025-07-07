using Experimentation.ECS_Project.Scripts.Player.Weapon.Reload;
using Leopotam.Ecs;

namespace Experimentation.ECS_Project.Scripts.Player.Weapon.Base
{
    public class WeaponShootSystem : IEcsRunSystem
    {
        private EcsFilter<Weapon, Shoot> filter;
        private UI.UI ui;
    
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var weapon = ref filter.Get1(i);

                ref var entity = ref filter.GetEntity(i);
                entity.Del<Shoot>();
            
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
