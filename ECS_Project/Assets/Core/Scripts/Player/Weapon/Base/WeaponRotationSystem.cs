using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Player.Weapon.Base
{
    public class WeaponRotationSystem : IEcsRunSystem
    {

        EcsFilter<Weapon, WeaponRaycastHit> filter;
        EcsFilter<Camera.Camera> cameraFilter;
        public void Run()
        {
            foreach (var i in filter)
            {
                foreach (var j in cameraFilter)
                {
                    ref var camera = ref cameraFilter.Get1(j);
                    ref var weapon = ref filter.Get1(i);
                    ref var weaponHit = ref filter.Get2(j);
                
                    RaycastHit hit;
                    Ray ray = new Ray(camera.CameraTransform.position, camera.CameraTransform.forward);
                    if (Physics.Raycast(ray, out hit, weapon.RangeShot))
                    {
                        if (hit.collider.TryGetComponent(out PlayerView player))
                        {
                            return;
                        }

                        weaponHit.RaycastHit = hit;
                        weaponHit.Ray = ray;
                        weapon.TargetShot.position = hit.point;
                    }   
                }
            }
        }
    }
}
