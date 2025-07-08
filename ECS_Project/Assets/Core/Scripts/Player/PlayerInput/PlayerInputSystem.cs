using Core.Scripts.UI.Pause;
using Experimentation.ECS_Project.Scripts.Player.Weapon;
using Experimentation.ECS_Project.Scripts.Player.Weapon.Reload;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.InputSystem;

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
            _inputActions.Weapon.Enable();
            _inputActions.UI.Enable();
        }

        public void Run()
        {
            Vector2 lookInputAction = _inputActions.Player.Look.ReadValue<Vector2>();
            Vector2 moveInputAction = _inputActions.Player.Move.ReadValue<Vector2>();
            
            _inputActions.Weapon.Shoot.performed += Shoot;
            _inputActions.Weapon.Shoot.canceled += StopShooting;
            
            _inputActions.UI.Pause.performed += Pause;
            _inputActions.Weapon.Reload.performed += ReloadWeapon;

            foreach (var i in filter)
            {
                ref var input = ref filter.Get1(i);
            
                input.lookInput = lookInputAction;
                input.moveInput = moveInputAction;
            }
        }

        public void Destroy()
        {
            _inputActions.Player.Disable();
            _inputActions.Weapon.Disable();
            _inputActions.UI.Disable();
        }

        #region Weapon

        private void Shoot(InputAction.CallbackContext context)
        {
            foreach (var i in filter)
            {
                ref var input = ref filter.Get1(i);
                input.shootInput = true;   
            }
        }

        private void StopShooting(InputAction.CallbackContext context)
        {
            foreach (var i in filter)
            {
                ref var input = ref filter.Get1(i);
                input.shootInput = false;
            }
        }

        private void ReloadWeapon(InputAction.CallbackContext context)
        {
            foreach (var i in filter)
            {
                ref var hasWeapon = ref filter.Get2(i);
                
                ref var weapon = ref hasWeapon.weapon.Get<Weapon.Base.Weapon>();
            
                if (weapon.currentInMagazine < weapon.maxInMagazine)
                {
                    ref var entity = ref filter.GetEntity(i);
                    entity.Get<TryReload>();
                }   
            }
        }

        #endregion

        #region UI

        private void Pause(InputAction.CallbackContext context)
        {
            _world.NewEntity().Get<PauseEvent>();
        }

        #endregion
    }
}
