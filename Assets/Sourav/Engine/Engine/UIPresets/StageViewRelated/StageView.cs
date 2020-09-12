using Sirenix.OdinInspector;
using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;
using UnityEngine.UI;

namespace Sourav.UIPresets.StageViewRelated
{
    public class StageView : GameElement
    {
        #region FIELDS
        [SerializeField] private Image fillImage;
        [SerializeField] private ToggleSwitch[] levels;
        
        [ReadOnly][SerializeField] private float incrementPerLevel;
        [ReadOnly][SerializeField] private float levelIncrement;
        [ReadOnly][SerializeField] private float currentIncrement;
        #endregion

        #region METHODS
        #region MONO METHODS
        private void Start()
        {
            incrementPerLevel = 1.0f / GetLevelsCount();
        }
        #endregion

        #region PUBLIC METHODS
        public void SetUpLevelView(int currentLevelIndex, int pointsOfIncrement)
        {
            if (currentLevelIndex > levels.Length)
            {
                D.LogError($"CURRENT LEVEL {currentLevelIndex} IS GREATER THAN LEVEL COUNT {GetLevelsCount()}");
                return;
            }

            SetAllLevelsOff();
            ResetFill();
            
            SetUpLevelIncrement(pointsOfIncrement);

            for (int i = 0; i < currentLevelIndex; i++)
            {
                levels[i].SetOn(true);
                fillImage.fillAmount += incrementPerLevel;
            }

            currentIncrement = 0;
        }
        
        public void IncreaseProgressionInLevel()
        {
            currentIncrement += levelIncrement;
            if (currentIncrement < incrementPerLevel)
            {
                fillImage.fillAmount += levelIncrement;
            }
        }
        
        public int GetLevelsCount()
        {
            return levels.Length;
        }
        #endregion

        #region HELPER METHODS
        private void SetAllLevelsOff()
        {
            for (int i = 0; i < levels.Length; i++)
            {
                levels[i].SetOff(true);
            }
        }
        
        private void ResetFill()
        {
            D.Log("RESET FILL");
            fillImage.fillAmount = 0.0f;
        }

        private void SetUpLevelIncrement(int numberOfPointsOfIncrement)
        {
            levelIncrement = incrementPerLevel / numberOfPointsOfIncrement;
        }
        #endregion
        #endregion
    }
}
