
using UnityEngine;

namespace Core.Scripts.Player.Weapon.Bullet
{
    public struct Projectile
    {
        public int damage;
        public Vector3 direction;
        public float radius;
        public float speed;
        public Vector3 previousPos;
        public GameObject projectileGO;
    }
}
