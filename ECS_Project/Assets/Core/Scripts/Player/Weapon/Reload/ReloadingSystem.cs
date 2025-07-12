using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Player.Weapon.Reload
{
    public class ReloadingSystem : IEcsRunSystem
    {
        private EcsFilter<TryReload, AnimatorRef>.Exclude<Reloading> tryReloadFilter;
        private EcsFilter<Core.Scripts.Player.Weapon.Base.Weapon, ReloadingFinished> reloadingFinishedFilter;
        private Experimentation.ECS_Project.Scripts.UI.UI ui;
        
        public void Run()
        {
            foreach (var i in tryReloadFilter)
            {
                ref var animatorRef = ref tryReloadFilter.Get2(i);


                animatorRef.animator.SetTrigger("Reload");

                ref var entity = ref tryReloadFilter.GetEntity(i);
                entity.Get<Reloading>();
            }

            foreach (var i in reloadingFinishedFilter)
            {
                ref var weapon = ref reloadingFinishedFilter.Get1(i);
            
                var needAmmo = weapon.maxInMagazine - weapon.currentInMagazine;
                weapon.currentInMagazine = (weapon.totalAmmo >= needAmmo)
                    ? weapon.maxInMagazine
                    : weapon.currentInMagazine + weapon.totalAmmo;
                weapon.totalAmmo -= needAmmo;
                weapon.totalAmmo = weapon.totalAmmo < 0
                    ? 0
                    : weapon.totalAmmo;

                ref var entity = ref reloadingFinishedFilter.GetEntity(i);
                
                if (weapon.owner.Has<Core.Scripts.Player.PlayerInit.Player>())
                {
                    ui.gameScreen.SetCurrentInMagazine(weapon.currentInMagazine);
                }
                
                weapon.owner.Del<Reloading>();
                entity.Del<ReloadingFinished>();
            }
        }
    }
}
