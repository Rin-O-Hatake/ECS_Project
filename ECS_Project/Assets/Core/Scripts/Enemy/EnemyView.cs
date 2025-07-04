using Core.Scripts.Base.Damage;
using Core.Scripts.Damage;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Core.Scripts.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        #region Fields

        [SerializeField] private NavMeshAgent _navMeshAgent;
        [SerializeField] private Animator _animator;
        [SerializeField] private float _meleeAttackDistance;
        [SerializeField] private float _triggerDistance;
        [SerializeField] private float _meleeAttackInterval;
        [SerializeField] private int _startHealth;
        [SerializeField] private int _damage;
        
        public EcsEntity Entity;
        
        private bool _isAttacking;

        #region Properties

        public NavMeshAgent NavMeshAgent => _navMeshAgent;
        public Animator Animator => _animator;
        public float MeleeAttackDistance => _meleeAttackDistance;
        public float TriggerDistance => _triggerDistance;
        public float MeleeAttackInterval => _meleeAttackInterval;
        public int StartHealth => _startHealth;
        public int Damage => _damage;
        
        #endregion

        #endregion

        public void Attack()
        {
            Entity.Get<AttackMarker>();
        }
    }
}
