using Core.Scripts.AllData.RunTimeData;
using Core.Scripts.AllData.StaticData;
using Core.Scripts.Base;
using Core.Scripts.Camera;
using Core.Scripts.Damage;
using Core.Scripts.Enemy;
using Core.Scripts.Player.PlayerAnimation;
using Core.Scripts.Player.PlayerInit;
using Core.Scripts.Player.PlayerInput;
using Core.Scripts.Player.PlayerMove;
using Core.Scripts.Player.Weapon.Base;
using Core.Scripts.Player.Weapon.Bullet;
using Core.Scripts.Player.Weapon.Reload;
using Core.Scripts.Player.Weapon.Shoot;
using Core.Scripts.UI.Pause;
using Experimentation.ECS_Project.Scripts.AllData.SceneData;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts
{
    public sealed class EcsGameStartup : MonoBehaviour
    {
        [SerializeField] private StaticData configuration;
        [SerializeField] private SceneData sceneData;
        
        private EcsWorld _world;
        private EcsSystems _systems;
        private EcsSystems _fixedUpdateSystems;
        private EcsSystems _lateUpdateSystems;
        private RuntimeData _runtimeData;
        
        public Experimentation.ECS_Project.Scripts.UI.UI ui;

        #region MonoBehavior

        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);
            _fixedUpdateSystems = new EcsSystems(_world);
            _lateUpdateSystems = new EcsSystems(_world);

            _runtimeData = new RuntimeData();

            AddSystemsPlayer();
            AddSystemsCamera();
            AddSystemsEnemy();
            AddSystemsWeapons();
            AddSystemsUI();
            AddSystemsOtherSystems();
            
            _systems.Init();
            _fixedUpdateSystems.Init();
            _lateUpdateSystems.Init();
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void FixedUpdate()
        {
            _fixedUpdateSystems?.Run();
        }

        private void LateUpdate()
        {
            _lateUpdateSystems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems == null)
            {
                return;
            }
            
            _systems?.Destroy();
            _systems = null;
            _fixedUpdateSystems?.Destroy();
            _fixedUpdateSystems = null;
            _world?.Destroy();
            _world = null;
        }

        #endregion

        #region Player

        private void AddSystemsPlayer()
        {
            _systems
                .Add(new PlayerInitSystem())
                .Add(new PlayerInputSystem())
                .Add(new PlayerAnimationSystem())
                .Add(new PlayerDeathSystem())
                .Add(new DamageSystem())
                .Inject(configuration)
                .Inject(sceneData)
                .Inject(ui)
                .Inject(_runtimeData);
            
            _fixedUpdateSystems
                .Add(new PlayerMoveSystem());
        }

        #endregion

        #region Camera

        private void AddSystemsCamera()
        {
            _systems
                .Add(new CameraInitSystem())
                .Inject(configuration)
                .Inject(sceneData);

            _lateUpdateSystems
                .Add(new CameraFollowSystem())
                .Inject(_runtimeData);
        }

        #endregion
        
        #region Enemy

        private void AddSystemsEnemy()
        {
            _systems
                .Add(new EnemyInitSystem())
                .Add(new EnemyDeathSystem())
                .Add(new EnemyFollowSystem())
                .Add(new EnemyIdleSystem())
                .Inject(configuration)
                .Inject(_runtimeData);
        }

        #endregion
        
        #region Weapon

        private void AddSystemsWeapons()
        {
            _systems
                .Add(new PlayerRotationShootSystem())
                .Add(new WeaponShootSystem())
                .Add(new SpawnProjectileSystem())
                .Add(new ProjectileMoveSystem())
                .Add(new ProjectileHitSystem())
                .Add(new ReloadingSystem())
                .OneFrame<TryReload>()
                .Add(new ShootEffectsSystem())
                .Add(new DestroyerProjectileSystem())
                .Add(new WeaponRotationSystem())
                .Inject(configuration)
                .Inject(ui);
        }

        #endregion
        
        #region Weapon

        private void AddSystemsUI()
        {
            _systems
                .Add(new PauseSystem())
                .Inject(_runtimeData)
                .Inject(ui);
        }

        #endregion

        #region Other

        private void AddSystemsOtherSystems()
        {
            _systems
                .Add(new CursorEnabledSystem());
        }

        #endregion
    }
}
