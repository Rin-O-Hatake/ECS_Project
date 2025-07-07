using Leopotam.Ecs;
using UnityEngine;

namespace Core.Scripts.Base
{
    public class CursorEnabledSystem : IEcsInitSystem
    {
        public void Init()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
