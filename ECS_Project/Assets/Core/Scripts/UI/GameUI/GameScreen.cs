using TMPro;
using UnityEngine;

namespace Experimentation.ECS_Project.Scripts.UI
{
    public class GameScreen : Screen
    {
        #region Feilds

        [SerializeField] private TMP_Text _currentInMagazineLabel;
        [SerializeField] private TMP_Text _totalAmmoLabel;

        #endregion

        #region Bullets

        public void SetTotalAmmo(int totalAmmo)
        {
            if (!CheckTrueValue(totalAmmo))
            {
                return;
            }
            
            _totalAmmoLabel.text = totalAmmo.ToString();
        }

        public void SetCurrentInMagazine(int currentInMagazine)
        {
            if (!CheckTrueValue(currentInMagazine))
            {
                return;
            }
            
            _currentInMagazineLabel.text = currentInMagazine.ToString();
        }

        private bool CheckTrueValue(int value)
        {
            if (value < 0)
            {
                return false;
            }
            
            return true;
        }

        #endregion
    }
}
