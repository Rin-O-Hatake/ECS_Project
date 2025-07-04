using Experimentation.ECS_Project.Scripts.AllData.RunTimeData;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Experimentation.ECS_Project.Scripts.UI.Pause
{
    public class PauseSystem : IEcsRunSystem
    {
        private EcsFilter<PauseEvent> filter;
        private RuntimeData runtimeData;
        private UI ui;
    
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
