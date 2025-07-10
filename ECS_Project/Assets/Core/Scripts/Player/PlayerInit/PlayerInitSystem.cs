using Core.Scripts.AllData.RunTimeData;
using Core.Scripts.AllData.SceneData;
using Core.Scripts.AllData.StaticData;
using Core.Scripts.Camera;
using Core.Scripts.Damage;
using Core.Scripts.Player.PlayerInput;
using Core.Scripts.Player.Weapon;
using Core.Scripts.Player.Weapon.Base;
using Experimentation.ECS_Project.Scripts.AllData.SceneData;
using Experimentation.ECS_Project.Scripts.Enemy;
using Experimentation.ECS_Project.Scripts.Player.Weapon;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Player.PlayerInit
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        private EcsWorld _ecsWorld;
        private StaticData _staticData;
        private SceneData _sceneData;
        private Experimentation.ECS_Project.Scripts.UI.UI ui;
        private RuntimeData _runtimeData;

        public void Init()
        {
            #region Player

            EcsEntity playerEntity = _ecsWorld.NewEntity();

            ref var player = ref playerEntity.Get<Player>();
            ref var inputData = ref playerEntity.Get<PlayerInputData>();
            ref var hasWeapon = ref playerEntity.Get<HasWeapon>();
            ref var animatorRef = ref playerEntity.Get<AnimatorRef>();
            ref var transformRef = ref playerEntity.Get<TransformRef>();
            ref var healthPlayer = ref playerEntity.Get<Health>();
        
            GameObject playerGO = Object.Instantiate(_staticData.PlayerPrefab, _sceneData.playerSpawnPoint.position, Quaternion.identity);
            player.CharacterController = playerGO.GetComponent<CharacterController>();
            player.playerSpeed = _staticData.PlayerSpeed;
            player.playerTransform = playerGO.transform;
            player.playerAnimator = playerGO.GetComponent<Animator>();
            player.AnalogMovement = _staticData.AnalogMovement;
            player.SpeedChangeRate = _staticData.SpeedChangeRate;
            player.RotationSmoothTime = _staticData.RotationSmoothTime;
            
            healthPlayer.value = _staticData.Health;
            animatorRef.animator = player.playerAnimator;
            transformRef.transform = playerGO.transform;
            _runtimeData.PlayerEntity = playerEntity;
            
            PlayerView playerView = playerGO.GetComponent<PlayerView>();
            playerView.entity = playerEntity;
            
            player.CameraTarget = playerView.CameraTarget;
            
            #endregion
            
            #region Weapon

            var weaponEntity = _ecsWorld.NewEntity();
            var weaponView = playerGO.GetComponentInChildren<WeaponSettings>();
            ref var weapon = ref weaponEntity.Get<Weapon.Base.Weapon>();
            ref var weaponRaycastHit = ref weaponEntity.Get<WeaponRaycastHit>();
            ref var weaponEffects = ref weaponEntity.Get<WeaponEffects>();
            
            weapon.owner = playerEntity;

            InitWeapon(ref weapon, weaponView);
            InitWeaponEffects(ref weaponEffects, weaponView);
            
            hasWeapon.weapon = weaponEntity;
            
            #endregion
            
            ui.gameScreen.SetCurrentInMagazine(weapon.currentInMagazine);
            ui.gameScreen.SetTotalAmmo(weapon.totalAmmo);
        }

        private void InitWeapon(ref Weapon.Base.Weapon weapon, WeaponSettings weaponView)
        {
            weapon.projectilePrefab = weaponView.ProjectilePrefab;
            weapon.projectileRadius = weaponView.ProjectileRadius;
            weapon.projectileSocket = weaponView.ProjectileSocket;
            weapon.projectileSpeed = weaponView.ProjectileSpeed;
            weapon.totalAmmo = weaponView.TotalAmmo;
            weapon.weaponDamage = weaponView.WeaponDamage;
            weapon.currentInMagazine = weaponView.CurrentInMagazine;
            weapon.maxInMagazine = weaponView.MaxInMagazine;
            weapon.TargetShot = weaponView.TargetShot;
            weapon.ProjectileLifetime = weaponView.ProjectileLifetime;
            weapon.RangeShot = weaponView.RangeShot;
        }
        
        private void InitWeaponEffects(ref WeaponEffects weaponEffectsEntity, WeaponSettings weaponView)
        {
            weaponEffectsEntity.ImpulseListener = weaponView.CameraImpulseListener;
            weaponEffectsEntity.RecoilForce = weaponView.RecoilForce;
            weaponEffectsEntity.RecoilDirection = weaponView.RecoilDirection;
        }
    }
}
