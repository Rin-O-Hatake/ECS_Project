using Sirenix.OdinInspector;
using UnityEngine;

namespace Core.Scripts.AllData.StaticData
{
    [CreateAssetMenu(fileName = "StaticData", menuName = "ECS/Scene Data", order = 55)]
    public class StaticData : ScriptableObject
    {
        #region Fields

        [Title("Player")]
        [SerializeField] private GameObject _playerPrefab;
        [SerializeField] private float _playerSpeed;
        [SerializeField] private int _health;
        [SerializeField] private bool _analogMovement;
        [SerializeField] private float _speedChangeRate  = 10.0f;
        [SerializeField, Range(0.0f, 0.3f)] private float _rotationSmoothTime  = 0.12f;
        
        [Title("Camera")]
        
        [SerializeField] private float rotationSpeed = 200.0f;
        
        
        #region Properties

        public GameObject PlayerPrefab => _playerPrefab;
        public float PlayerSpeed => _playerSpeed;
        public int Health => _health;
        public float RotationSpeed => rotationSpeed;
        public bool AnalogMovement => _analogMovement;
        public float SpeedChangeRate => _speedChangeRate;
        public float RotationSmoothTime => _rotationSmoothTime;

        #endregion

        #endregion
    }
}
