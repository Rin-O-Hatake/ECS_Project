using UnityEngine;
using UnityEngine.Serialization;

namespace Experimentation.ECS_Project.Scripts.AllData.SceneData
{
    public class WeaponSettings : MonoBehaviour
    {
        #region Fields

        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private  Transform _projectileSocket;
        [SerializeField] private  float _projectileSpeed;
        [SerializeField] private  float _projectileRadius;
        [SerializeField] private  int _weaponDamage;
        [SerializeField] private  int _currentInMagazine;
        [SerializeField] private  int _maxInMagazine;
        [SerializeField] private  int _totalAmmo;

        #region Properties

        public GameObject ProjectilePrefab => _projectilePrefab;
        public Transform ProjectileSocket => _projectileSocket;
        public float ProjectileSpeed => _projectileSpeed;
        public float ProjectileRadius => _projectileRadius;
        public int WeaponDamage => _weaponDamage;
        public int CurrentInMagazine => _currentInMagazine;
        public int MaxInMagazine => _maxInMagazine;
        public int TotalAmmo => _totalAmmo;

        #endregion

        #endregion
    }
}
