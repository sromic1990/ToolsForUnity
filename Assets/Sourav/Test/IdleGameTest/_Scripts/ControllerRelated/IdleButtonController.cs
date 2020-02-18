#if IDLEGAME
using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.AdditionalLogicRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.IdleGameEngine.IdleCurrency.IdleCurrency;
using Sourav.IdleGameEngine.IdleGameData;

namespace Sourav.Test.IdleGameTest._Scripts.ControllerRelated
{
    public class IdleButtonController : Sourav.Engine.Core.ControllerRelated.Controller
    {
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.UnitsUpdated:
                    EvaluateInfos();
                    break;
                
                case Notification.IdleButtonPressed:
                    HandleButtonPressed((IdleUnitType) param.intData[0]);
                    break;
                
                case Notification.IdleButtonFillStarted:
                    HandleTimerOfInfo((IdleUnitType) param.intData[0], isStarted: true);
                    EvaluateInfos();
                    break;
                
                case Notification.IdleButtonFillComplete:
                    HandleTimerOfInfo((IdleUnitType) param.intData[0], isStarted: false);
                    EvaluateInfos();
                    break;
            }
        }

        private void HandleButtonPressed(IdleUnitType unitType)
        {
            IdleInfo info = App.GetIdleData().GetInfo(unitType);
            App.GetLevelData().unitsToBeAddedNextTick += info.defaultUnitIncreasePerSecond * info.permanentMultiplier *
                                                         (int)App.GetLevelData().currentMultiplier;
            IdleCurrency unit = App.GetLevelData().Unit;
            unit = unit - info.nextCost.cost.cost;
            if (unit < 0)
            {
                unit = 0;
            }
            App.GetLevelData().Unit = unit;
            //Formula for upgrading cost
            IdleCurrency currentCost = info.nextCost.cost.cost;
            if (info.hasAdsForCost)
            {
                if (info.alwaysVideo)
                {
                    info.nextCost.isVideo = true;
                }
                else
                {
                    info.currentCountOfTap++;
                    if (info.currentCountOfTap >= info.adsEveryHowManyTap)
                    {
                        info.nextCost.isVideo = true;
                    }
                    else
                    {
                        info.nextCost.isVideo = false;
                        IdleCurrency priceOnBaseCost = info.data.baseCost * info.data.percentageOnBaseCost;
                        priceOnBaseCost *= 0.01f;
                        IdleCurrency priceOnNewCost = currentCost * info.data.percentageOnNewCost;
                        priceOnNewCost *= 0.01f;
                        info.nextCost.cost.cost += (priceOnBaseCost + priceOnNewCost);
                    }
                }
                
            }
            else
            {
                info.nextCost.isVideo = false;
                IdleCurrency priceOnBaseCost = info.data.baseCost * info.data.percentageOnBaseCost;
                priceOnBaseCost *= 0.01f;
                IdleCurrency priceOnNewCost = currentCost * info.data.percentageOnNewCost;
                priceOnNewCost *= 0.01f;
                info.nextCost.cost.cost += (priceOnBaseCost + priceOnNewCost);
            }

            int addition = (int)App.GetLevelData().currentMultiplier;
            D.Log($"addition is {addition}");
            info.currentCount += addition;

            info.growth.currentSliderValue = 0;
            for (int i = 0; i < info.currentCount; i++)
            {
                if (i < info.growth.firstHalfClicks)
                {
                    info.growth.currentSliderValue += info.growth.firstHalfGrowthRate;
                }
                else
                {
                    info.growth.currentSliderValue += info.growth.lastHalfGrowthRate;
                }
            }

            if (info.data.hasTimerBeforeNextClick)
            {
                NotificationParam param = new NotificationParam(Mode.intData);
                param.intData.Add((int)info.unitType);
                App.Notify(Notification.StartBlockIng, param);
            }
            App.Notify(Notification.UnitsUpdated);
        }

        private void HandleTimerOfInfo(IdleUnitType unit, bool isStarted)
        {
            IdleInfo info = App.GetIdleData().GetInfo(unit);
            if (info != null)
            {
                if (isStarted)
                {
                    info.idleActivation.hasTimerFinished = false;
                }
                else
                {
                    info.idleActivation.hasTimerFinished = true;
                }
            }
        }

        private void EvaluateInfos()
        {
            // D.Log($"Evaluating info");
            for (int i = 0; i < App.GetIdleData().idleLevels.Count; i++)
            {
                for (int j = 0; j < App.GetIdleData().idleLevels[i].idleInfos.Count; j++)
                {
                    IdleInfo info = App.GetIdleData().idleLevels[i].idleInfos[j];

                    LogicStatus status = App.GetLogicEvaluator().EvaluateLogic(info.logicToUnlock);

                    if (info.nextCost.cost.cost < App.GetLevelData().Unit && status == LogicStatus.Fulfilled)
                    {
                        info.idleActivation.isAffordable = true;
                    }
                    else
                    {
                        info.idleActivation.isAffordable = false;
                    }

                    if (info.data.hasTimerBeforeNextClick)
                    {
                        if (info.idleActivation.hasTimerFinished)
                        {
                            info.idleActivation.isClickable = true;
                        }
                        else
                        {
                            info.idleActivation.isClickable = false;
                        }
                    }

                    if (!info.data.isLockedByDefault)
                    {
                        info.idleActivation.isUnlocked = true;
                    }
                    else
                    {
                        if (info.dependsOnAllPrevious)
                        {
                            bool canUnlock = true;

                            for (int k = 0; k < j; k++)
                            {
                                IdleInfo prevInfo = App.GetIdleData().idleLevels[i].idleInfos[k];
                                if (prevInfo.currentCount < prevInfo.data.totalCount)
                                {
                                    canUnlock = false;
                                    break;
                                }
                            }

                            info.idleActivation.isUnlocked = canUnlock;
                        }
                        else
                        {
                            int filledDependencies = 0;
                            for (int k = 0; k < info.dependsOn.dependencies.Count; k++)
                            {
                                IdleUnitType type = info.dependsOn.dependencies[i].unitType;
                                int requiredCount = info.dependsOn.dependencies[i].countToUnlock;
                                int actualCount = App.GetIdleData().GetCount(type);
                                if (requiredCount <= actualCount)
                                {
                                    info.dependsOn.dependencies[i].isComplete = true;
                                    filledDependencies++;
                                }
                                else
                                {
                                    info.dependsOn.dependencies[i].isComplete = false;
                                }
                            }
                            if (filledDependencies == info.dependsOn.dependencies.Count)
                            {
                                info.idleActivation.isUnlocked = true;
                            }
                            else
                            {
                                info.idleActivation.isUnlocked = false;
                            }
                        }
                    }
                }
            }
            App.Notify(Notification.ButtonsUpdated);
        }
    }
}
#endif
