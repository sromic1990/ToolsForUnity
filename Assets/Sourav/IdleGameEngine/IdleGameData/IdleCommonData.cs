#if IDLEGAME
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sourav.Engine.Editable.DataRelated;
using UnityEngine;

namespace Sourav.IdleGameEngine.IdleGameData
{
    public class IdleCommonData : CommonData
    {
        [Space(10)] [Header("Common Upgrades")]
        public IdleCommonFloatUpgrade idleOfflineTimerUpgrade;
        public IdleCommonFloatUpgrade idleOfflineEarningUpgrade;
        public IdleCommonCurrencyUpgrade idleTapperUpgrade;

        [Space(10)]
        [Header("PaintLevels Related")]
        public List<IdleLevel> idleLevels;

        public int GetCount(IdleUnitType item)
        {
            int count = 0;
            bool canBreak = false;
            for (int i = 0; i < idleLevels.Count; i++)
            {
                for (int j = 0; j < idleLevels[i].idleInfos.Count; j++)
                {
                    if (item == idleLevels[i].idleInfos[j].unitType)
                    {
                        count = idleLevels[i].idleInfos[j].currentCount;
                        canBreak = true;
                        break;
                    }
                }
                if(canBreak)
                    break;
            }
            return count;
        }
        
        public IdleInfo GetInfo(IdleUnitType item)
        {
            IdleInfo info = null;
            bool canBreak = false;
            for (int i = 0; i < idleLevels.Count; i++)
            {
                for (int j = 0; j < idleLevels[i].idleInfos.Count; j++)
                {
                    if (item == idleLevels[i].idleInfos[j].unitType)
                    {
                        info = idleLevels[i].idleInfos[j];
                        canBreak = true;
                        break;
                    }
                }
                if(canBreak)
                    break;
            }
            return info;
        }

    }

    [System.Serializable]
    public class IdleLevel
    {
        public IdleLevelType levelType;
        public List<IdleInfo> idleInfos;
        public List<IdleLevelUpgrades> idleUpgrades;
        
        [Space(10)][Header("Status Related")]
        public bool isComplete;
        public bool isLocked;
        public IdleCurrency.IdleCurrency.IdleCurrency unlockCost;
        public bool canUnlockByUnits;
        public IdleCurrency.IdleCurrency.IdleCurrency unitsPerSecondAfterComplete;

    }
    
    
    [System.Serializable]
    public class IdleInfo : IdlePurchasableItem
    {
        public IdleUnitType unitType;

        [Space(10)] [Header("Unit Increment Related")]
        public IdleCurrency.IdleCurrency.IdleCurrency defaultUnitIncreasePerSecond;
        public IdleCurrency.IdleCurrency.IdleCurrency permanentMultiplier;
        
        [Space(10)] [Header("Dependency Related")]
        public bool dependsOnAllPrevious;
        public Dependencies dependsOn;

        [Space(10)] [Header("Growth Related")] public GrowthRate growth;
        public float minSliderValue;
        public float maxSliderValue;
        [Button()]
        private void DetermineGrowthRate()
        {
            growth.firstHalfClicks = (int)(data.totalCount * (data.separatorPercentage / 100));
            growth.lastHalfClicks = data.totalCount - growth.firstHalfClicks;

            float oneHalfGrowth = (maxSliderValue * (data.valueSeparatorPercentage / 100));
            
            growth.firstHalfGrowthRate = oneHalfGrowth / growth.firstHalfClicks;
            growth.lastHalfGrowthRate = (maxSliderValue - oneHalfGrowth) / growth.lastHalfClicks;

            growth.currentSliderValue = maxSliderValue;
        }

        [Button()]
        public override void SetDefault()
        {
            base.SetDefault();
            growth.currentSliderValue = minSliderValue;
            if (dependsOn.dependencies != null)
            {
                for (int i = 0; i < dependsOn.dependencies.Count; i++)
                {
                    dependsOn.dependencies[i].isComplete = false;
                }
            }

            permanentMultiplier = 1;
        }
    }

    [System.Serializable]
    public class IdleLevelUpgrades : IdlePurchasableItem
    {
        //Offline unit, Offline Timer, Tapper Upgrade, UnitColorBoost
        public IdleUpgradeFunction function;
        //Which unit this upgrade affects
        public IdleUnitType unitAffected;
        //If this is completed, don't show the upgrade any further
        [ReadOnly] public bool isUpgradeCompleted;
        public bool isHideOnComplete;
        //Upgrade limit if any
        public LimitType limitType;
        public IdleCurrency.IdleCurrency.IdleCurrency upgradeLimit;
        public float upgradeLimitF;
        public float upgradeLimitI;
        public bool hasLimitReached;
        //Post Limit UpGradeIncrement
        public bool hidePostLimit;
        public float postLimitIncrementPercentage;
        
        //In case of Serialized upgrade, serialized list of price increments;
        public List<IdleCurrency.IdleCurrency.IdleCurrency> upgradeCostSequence;
        [ReadOnly] public int currentSequence;

        public bool hasBoost;
        [ShowIf("hasBoost", true)]public IdleBoostQuantum boostQuantum;
    }

    [System.Serializable]
    public class IdleCommonFloatUpgrade : IdleLevelUpgrades
    {
        public float currentValue;
        public float defaultValue;
    }

    [System.Serializable]
    public class IdleCommonCurrencyUpgrade : IdleLevelUpgrades
    {
        public IdleCurrency.IdleCurrency.IdleCurrency tapperValue;
        public IdleCurrency.IdleCurrency.IdleCurrency defaultTapperValue;
    }


    [System.Serializable]
    public class Dependencies
    {
        public List<SingleDependency> dependencies;
    }
    
    [System.Serializable]
    public class SingleDependency
    {
        public IdleUnitType unitType;
        public int countToUnlock;
        public bool isComplete;
    }

    [System.Serializable]
    public class NextCost
    {
        public Cost cost;
        public bool isVideo;
    }
    [System.Serializable]
    public class Cost
    {
        public IdleCostType costType;
        [ShowIf("costType", IdleCostType.Price)]public IdleCurrency.IdleCurrency.IdleCurrency cost;
    }
    
    [System.Serializable]
    public class IdlePurchasableItem
    {
        public bool hasIndex;
        [ShowIf("hasIndex", true)]public int index;
        public IdlePurchasableItemType type;
        public IdleUnitData data;
        public bool hasAdsForCost;
        [ShowIf("hasAdsForCost", true)] public int currentCountOfTap;
        [ShowIf("hasAdsForCost", true)] public bool alwaysVideo;
        [ShowIf("hasAdsForCost", true)] public int adsEveryHowManyTap;
        public NextCost nextCost;

        public IdleActivation idleActivation;

        public List<LogicData> logicToUnlock;

        public int currentCount;

        public bool hasVariableBlockTime;
        // [ShowIf("hasVariableBlockTime", true)] public float decrementOfBlockTimePer
        [ShowIf("hasVariableBlockTime", true)] public float currentBlockTime;

        public virtual void SetDefault()
        {
            nextCost.cost.cost = data.baseCost;
            currentCount = 0;
            currentCountOfTap = 0;
            if (data.isLockedByDefault)
            {
                idleActivation.isUnlocked = false;
            }
            else
            {
                idleActivation.isUnlocked = true;
            }

            idleActivation.isAffordable = false;
            idleActivation.isClickable = false;
            idleActivation.hasTimerFinished = true;
            currentBlockTime = data.defaultTimer;
        }
    }

    [System.Serializable]
    public class IdleActivation
    {
        public bool isUnlocked;
        [ReadOnly] public bool isAffordable;
        [ReadOnly] public bool isClickable;
        [ReadOnly] public bool allLogicFulfilled;
        [ReadOnly] public bool hasTimerFinished;
    }

    [System.Serializable]
    public class IdleBoostQuantum
    {
        public float defaultBoostPercentage;
        public float currentBoostPercentage;
        public float boostIncrementQuantum;
    }

    [System.Serializable]
    public class GrowthRate
    {
        public float currentSliderValue;
        [Space(10)]
        [Header("ONLY FOR READONLY AND TESTING")]
        [ReadOnly] public int firstHalfClicks;
        [ReadOnly] public int lastHalfClicks;
        [ReadOnly] public float firstHalfGrowthRate;
        [ReadOnly] public float lastHalfGrowthRate;
    }
}
#endif