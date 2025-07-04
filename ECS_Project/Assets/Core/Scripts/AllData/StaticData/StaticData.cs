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
        
        [Title("Camera")]
        [SerializeField] private float _smoothTime;
        [SerializeField] private Vector3 _followOffset;
        
        [SerializeField] private float distance = 5.0f;
        [SerializeField] private float height = 2.0f;
        [SerializeField] private float rotationSpeed = 200.0f;
        [SerializeField] private float heightDamping = 2.0f;
        [SerializeField] private float rotationDamping = 3.0f;
        
        #region Properties

        public GameObject PlayerPrefab => _playerPrefab;
        public float PlayerSpeed => _playerSpeed;
        public int Health => _health;
        
        public Vector3 FollowOffset => _followOffset;
        public float SmoothTime => _smoothTime;
        
        public float Distance => distance;
        public float Height => height;
        public float RotationSpeed => rotationSpeed;
        public float HeightDamping => heightDamping;
        public float RotationDamping => rotationDamping;

        #endregion

        #endregion
    }
}
