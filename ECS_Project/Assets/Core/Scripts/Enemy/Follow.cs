using Leopotam.Ecs;

namespace Experimentation.ECS_Project.Scripts.Enemy
{
    public struct Follow
    {
        public EcsEntity Target;
        public float NextAttackTime;
    }
}
