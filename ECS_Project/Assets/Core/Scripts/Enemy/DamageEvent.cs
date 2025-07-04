using Leopotam.Ecs;

namespace Experimentation.ECS_Project.Scripts.Enemy
{
    public struct DamageEvent
    {
        public EcsEntity Target;
        public int Value;
    }
}
