using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Experimentation.ECS_Project.Scripts.Enemy
{
    public class EnemyView : MonoBehaviour
    {
        public EcsEntity entity;
        public NavMeshAgent navMeshAgent;
        public Animator animator;
        public float meleeAttackDistance;
        public float triggerDistance;
        public float meleeAttackInterval;
        public int startHealth;
        public int damage;
    }
}
