using Cinemachine;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Scripts.AllData.SceneData
{
    public class WeaponSettings : MonoBehaviour
    {
        #region Fields

        [Title("Weapon Settings")]
        [SerializeField] private GameObject _projectilePrefab;
        [SerializeField] private  Transform _projectileSocket;
        [SerializeField] private  float _projectileSpeed;
        [SerializeField] private  float _projectileRadius;
        [SerializeField] private  int _weaponDamage;
        [SerializeField] private  int _currentInMagazine;
        [SerializeField] private  int _maxInMagazine;
        [SerializeField] private  int _totalAmmo;
        [SerializeField] private Transform _targetShot;
        [SerializeField] private float _projectileLifetime;
        [SerializeField] private int _rangeShot;
        
        [Title("Weapon Settings")]
        [SerializeField] private CinemachineImpulseSource _cameraImpulseListener;
        [SerializeField] private float _recoilForce = 1f;
        [SerializeField] private Vector3 _recoilDirection = Vector3.back;

        #region Properties

        public GameObject ProjectilePrefab => _projectilePrefab;
        public Transform ProjectileSocket => _projectileSocket;
        public float ProjectileSpeed => _projectileSpeed;
        public float ProjectileRadius => _projectileRadius;
        public int WeaponDamage => _weaponDamage;
        public int CurrentInMagazine => _currentInMagazine;
        public int MaxInMagazine => _maxInMagazine;
        public int TotalAmmo => _totalAmmo;
        public Transform TargetShot => _targetShot;
        public float ProjectileLifetime => _projectileLifetime;
        public int RangeShot => _rangeShot;
        
        public CinemachineImpulseSource CameraImpulseListener => _cameraImpulseListener;
        public float RecoilForce => _recoilForce;
        public Vector3 RecoilDirection => _recoilDirection;

        #endregion

        #endregion
    }
}
