using Core.Scripts.AllData.RunTimeData;
using Experimentation.ECS_Project.Scripts.UI.Pause;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core.Scripts.UI.Pause
{
    public class PauseSystem : IEcsRunSystem
    {
        private EcsFilter<PauseEvent> filter;
        private RuntimeData runtimeData;
        private Experimentation.ECS_Project.Scripts.UI.UI ui;
    
        public void Run()
        {
            foreach (var i in filter)
            {
                filter.GetEntity(i).Del<PauseEvent>();
                
                if (runtimeData.GameOver)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    continue;
                }
                
                runtimeData.IsPaused = !runtimeData.IsPaused;
                Time.timeScale = runtimeData.IsPaused ? 0f : 1f;
                ui.pauseScreen.Show(runtimeData.IsPaused);
            }
        }
    }
}
