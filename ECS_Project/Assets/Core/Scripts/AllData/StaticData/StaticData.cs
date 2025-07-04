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
        
        [Title("Camera")]
        [SerializeField] private float _smoothTime;
        [SerializeField] private Vector3 _followOffset;
        
        #region Properties

        public GameObject PlayerPrefab => _playerPrefab;
        public float PlayerSpeed => _playerSpeed;
        
        public Vector3 FollowOffset => _followOffset;
        public float SmoothTime => _smoothTime;

        #endregion

        #endregion
    }
}
