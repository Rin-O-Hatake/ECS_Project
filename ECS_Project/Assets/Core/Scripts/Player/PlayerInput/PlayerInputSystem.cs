using Experimentation.ECS_Project.Scripts.Player.Weapon;
using Experimentation.ECS_Project.Scripts.Player.Weapon.Reload;
using Experimentation.ECS_Project.Scripts.UI.Pause;
using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Player.PlayerInput
{
    public class PlayerInputSystem : IEcsRunSystem, IEcsInitSystem, IEcsDestroySystem
    {
        readonly EcsWorld _world;

        private EcsFilter<PlayerInputData, HasWeapon> filter;
        
        private IA_Player _inputActions;

        public void Init()
        {
            _inputActions = new IA_Player();
            _inputActions.Player.Enable();
        }

        public void Run()
        {
            Vector2 lookInputAction = _inputActions.Player.Look.ReadValue<Vector2>();
            Vector2 moveInputAction = _inputActions.Player.Move.ReadValue<Vector2>();
            
            bool isShootWeaponInput = _inputActions.Weapon.Shoot.ReadValue<bool>();
            bool isReloadWeaponInput = _inputActions.Weapon.Reload.ReadValue<bool>();
            bool isPauseInput = _inputActions.UI.Pause.ReadValue<bool>();
            
            foreach (var i in filter)
            {
                ref var input = ref filter.Get1(i);
                ref var hasWeapon = ref filter.Get2(i);
            
                input.lookInput = lookInputAction;
                input.moveInput = moveInputAction;
                
                input.shootInput = isShootWeaponInput;
                
                if (isReloadWeaponInput)
                {
                    ref var weapon = ref hasWeapon.weapon.Get<Experimentation.ECS_Project.Scripts.Player.Weapon.Base.Weapon>();
            
                    if (weapon.currentInMagazine < weapon.maxInMagazine)
                    {
                        ref var entity = ref filter.GetEntity(i);
                        entity.Get<TryReload>();
                    }
                }
                
                if (isPauseInput)
                {
                    _world.NewEntity().Get<PauseEvent>();
                }
            }
        }

        public void Destroy()
        {
            _inputActions.Player.Disable();
        }
        
        
        
        // private EcsWorld _ecsWorld;
        // private EcsFilter<PlayerInputData, HasWeapon> filter;
        //
        // public void Run()
        // {
        //     foreach (var i in filter)
        //     {
        //         ref var input = ref filter.Get1(i);
        //         ref var hasWeapon = ref filter.Get2(i);
        //
        //         input.moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        //         input.shootInput = Input.GetMouseButton(0);
        //         input.mouseX = Input.GetAxis("Mouse X");
        //         input.mouseY = Input.GetAxis("Mouse Y");
        //         
        //         if (Input.GetKeyDown(KeyCode.R))
        //         {
        //             ref var weapon = ref hasWeapon.weapon.Get<Experimentation.ECS_Project.Scripts.Player.Weapon.Base.Weapon>();
        //
        //             if (weapon.currentInMagazine < weapon.maxInMagazine)
        //             {
        //                 ref var entity = ref filter.GetEntity(i);
        //                 entity.Get<TryReload>();
        //             }
        //         }
        //         
        //         if (Input.GetKeyDown(KeyCode.Escape))
        //         {
        //             _ecsWorld.NewEntity().Get<PauseEvent>();
        //         }
        //     }
        // }
    }
}
