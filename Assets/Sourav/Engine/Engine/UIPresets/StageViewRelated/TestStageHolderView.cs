using Sirenix.OdinInspector;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.UIPresets.StageViewRelated
{
    public class TestStageHolderView : GameElement
    {
        [SerializeField] private StageHolderView stageHolder;

        [Button()]
        public void SetStageView(int numberOfLevels, int currentLevel, int pointsOfProgression)
        {
            stageHolder.SetUpStages(numberOfLevels, currentLevel, pointsOfProgression);
        }
        
        [Button()]
        public void SetStageView(int numberOfLevels, int currentLevel)
        {
            stageHolder.SetUpStages(numberOfLevels, currentLevel, 10);
        }

        [Button()]
        public void IncreaseProgression()
        {
            stageHolder.IncreaseProgressionInStage();
        }
    }
}
