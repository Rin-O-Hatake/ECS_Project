using Leopotam.Ecs;

namespace Core.Scripts.Damage
{
    public class DamageSystem : IEcsRunSystem
    {
        private EcsFilter<DamageEvent> damageEvents;
    
        public void Run()
        {
            foreach (var i in damageEvents)
            {
                ref var e = ref damageEvents.Get1(i);
                ref var health = ref e.Target.Get<Health>();

                health.value -= e.Value;

                if (health.value <= 0)
                {
                    e.Target.Get<DeathEvent>();
                }

                damageEvents.GetEntity(i).Destroy();
            }
        }
    }
}
