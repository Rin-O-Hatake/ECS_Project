using UnityEngine;

namespace Experimentation.ECS_Project.Scripts.UI
{
    public class Screen : MonoBehaviour
    {
        public virtual void Show(bool state = true)
        {
            gameObject.SetActive(state);
        }
    }
}
