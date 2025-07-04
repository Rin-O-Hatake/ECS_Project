using Leopotam.Ecs;

namespace Core.Scripts.Damage
{
    public struct DamageEvent
    {
        public EcsEntity Target;
        public int Value;
    }
}
