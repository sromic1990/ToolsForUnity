using UnityEngine;

namespace Sourav.Engine.Editable.DataRelated
{
    public class TimerData : CommonData
    {
        [Space(10)] [Header("Timer Related")] 
        public float waitTimerAfterLevelCompleteToRotateWheel;
        public float waitTimerBeforeDoorAnimation;
        [Range(0, 60)]
        public int timerValue;
        public bool isTimerStarted;
        public bool pauseTimer;
        public float waitAfterTimesUp;
        public float waitAfterEffectCollect;
        public float waitBeforeShowingTutorialHand;
        public float waitBeforeShowingPuzzle;
        public float waitTimeAtLevelStart;
        public float waitAfterTutorialComplete;
        public float waitAfterLevelComplete;
        public float waitBeforeHidingBombedTiles;
        public float playerDieDelay;
    }
}
