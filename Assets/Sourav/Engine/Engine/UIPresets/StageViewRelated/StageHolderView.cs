using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Utilities.Extensions;
using UnityEngine;

namespace Sourav.UIPresets.StageViewRelated
{
    public class StageHolderView : GameElement
    {
        [SerializeField] private StageView[] stages;
        [SerializeField] private StageView currentStageView;

        public void SetUpStages(int levelCount, int currentLevel, int pointsOfProgression = 1)
        {
            // if (currentLevel > 0)
            // {
            //     HideAllStages();
            //     ShowCorrectStage(levelCount);
            //     AdvanceStage(currentLevel, pointsOfProgression);
            // }
            // else
            // {
                HideAllStages();
                ShowCorrectStage(levelCount);
                AdvanceStage(currentLevel, pointsOfProgression);
            // }
        }

        public void IncreaseProgressionInStage()
        {
            currentStageView.IncreaseProgressionInLevel();
        }

        private void AdvanceStage(int currentLevel, int pointsOfProgression)
        {
            currentStageView.SetUpLevelView(currentLevel, pointsOfProgression);
        }

        private void HideAllStages()
        {
            for (int i = 0; i < stages.Length; i++)
            {
               stages[i].gameObject.Hide(); 
            }
        }
        
        private void ShowCorrectStage(int levelCount)
        {
            bool found = false;
            for (int i = 0; i < stages.Length; i++)
            {
                if (stages[i].GetLevelsCount() == levelCount)
                {
                    found = true;
                    currentStageView = stages[i];
                    break;
                }
            }

            if (!found)
            {
                D.LogError($"NO PRESET WITH {levelCount} LEVELS IS SET!");
            }
            else
            {
                currentStageView.gameObject.Show();
            }
        }
    }
}
