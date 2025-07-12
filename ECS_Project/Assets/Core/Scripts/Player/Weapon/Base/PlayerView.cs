using Core.Scripts.Player.Weapon.Reload;
using Experimentation.ECS_Project.Scripts.Player.Weapon;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Player.Weapon.Base
{
    public class PlayerView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _cameraTarget;
        
        public EcsEntity entity;

        #region Properties

        public GameObject CameraTarget => _cameraTarget;

        #endregion

        #endregion

        public void Shoot()
        {
            entity.Get<HasWeapon>().weapon.Get<Shoot.Shoot>();
        }
        
        public void Reload()
        {
            entity.Get<HasWeapon>().weapon.Get<ReloadingFinished>();
        }
    }
}
