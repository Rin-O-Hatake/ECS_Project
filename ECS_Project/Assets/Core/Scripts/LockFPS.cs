using UnityEngine;

namespace Experimentation.ECS_Project.Scripts
{
    public class LockFPS : MonoBehaviour
    {
        void Awake()
        {
            Application.targetFrameRate = 60;
        }
    }
}
