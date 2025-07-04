using Core.Scripts.AllData.StaticData;
using Experimentation.ECS_Project.Scripts.AllData.SceneData;
using Experimentation.ECS_Project.Scripts.Player.PlayerInput;
using Experimentation.ECS_Project.Scripts.Player.Weapon;
using Leopotam.Ecs;
using UnityEngine;

namespace Experimentation.ECS_Project.Scripts.Player.PlayerInit
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private StaticData _staticData;
        private SceneData _sceneData;
        private UI.UI ui;

        public void Init()
        {
            #region Player

            EcsEntity playerEntity = _ecsWorld.NewEntity();

            ref var player = ref playerEntity.Get<Player>();
            ref var inputData = ref playerEntity.Get<PlayerInputData>();
            ref var hasWeapon = ref playerEntity.Get<HasWeapon>();
            ref var animatorRef = ref playerEntity.Get<AnimatorRef>();
        
            GameObject playerGO = Object.Instantiate(_staticData.PlayerPrefab, _sceneData.playerSpawnPoint.position, Quaternion.identity);
            player.playerRigidbody = playerGO.GetComponent<Rigidbody>();
            player.playerSpeed = _staticData.PlayerSpeed;
            player.playerTransform = playerGO.transform;
            player.playerAnimator = playerGO.GetComponent<Animator>();
            animatorRef.animator = player.playerAnimator;

            #endregion
            
            #region Weapon

            var weaponEntity = _ecsWorld.NewEntity();
            var weaponView = playerGO.GetComponentInChildren<WeaponSettings>();
            ref var weapon = ref weaponEntity.Get<Weapon.Base.Weapon>();
            weapon.owner = playerEntity;
            weapon.projectilePrefab = weaponView.ProjectilePrefab;
            weapon.projectileRadius = weaponView.ProjectileRadius;
            weapon.projectileSocket = weaponView.ProjectileSocket;
            weapon.projectileSpeed = weaponView.ProjectileSpeed;
            weapon.totalAmmo = weaponView.TotalAmmo;
            weapon.weaponDamage = weaponView.WeaponDamage;
            weapon.currentInMagazine = weaponView.CurrentInMagazine;
            weapon.maxInMagazine = weaponView.MaxInMagazine;

            hasWeapon.weapon = weaponEntity;
            
            playerGO.GetComponent<PlayerView>().entity = playerEntity;

            #endregion
            
            ui.gameScreen.SetCurrentInMagazine(weapon.currentInMagazine);
            ui.gameScreen.SetTotalAmmo(weapon.totalAmmo);
        }
    }
}
