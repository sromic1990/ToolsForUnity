// using Sirenix.OdinInspector;
using UnityEngine;
using Sourav.Idle;

namespace Sourav.IdleGameEngine.IdleGameData
{
    [CreateAssetMenu()]
    public class IdleUnitData : ScriptableObject
    {
        public IdleCurrency baseCost;
        public float percentageOnBaseCost;
        public float percentageOnNewCost;
        public int totalCount;
        public bool isLockedByDefault;
        // [ShowIf("isLockedByDefault", true)] 
        public bool canBeUnlockedWithCost;
        // [ShowIf("canBeUnlockedWithCost", true)]
        public IdleCurrency unlockCost;
        public bool hasTimerBeforeNextClick;
        // [ShowIf("hasTimerBeforeNextClick")] 
        public float defaultTimer;
        
        [Space(10)][Header("Growth Related")]
        public float separatorPercentage;
        public float valueSeparatorPercentage;
    }
}
