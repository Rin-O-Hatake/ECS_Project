using Cinemachine;
using UnityEngine;

namespace Core.Scripts.Player.Weapon.Base
{
    public struct WeaponEffects
    {
        public CinemachineImpulseSource ImpulseListener;
        public float RecoilForce;
        public Vector3 RecoilDirection;
    }
}
