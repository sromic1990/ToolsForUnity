using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.IdleGameEngine.IdleCurrency.IdleCurrency;
using Sourav.IdleGameEngine.IdleGameData;
using Sourav.IdleGameEngine.UpdateRelated;
using Sourav.Utilities.Extensions;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Sourav.Test.IdleGameTest._Scripts.ViewRelated.Elements
{
    public class IdleButtonView : GameElement
    {
        [SerializeField] private GameObject[] enabledGameObjects;
        [SerializeField] private GameObject[] disabledGameObjects;
        [SerializeField] private GameObject[] lockedGameObjects;
        [SerializeField] private GameObject[] finishedGameObjects;
        [SerializeField] private Image countFill;
        [SerializeField] private Image fillImage;
        [SerializeField] private Text[] countComparisonText;
        [SerializeField] private Text[] costText;
        [SerializeField] private Text[] increaseUnitTexts;
        [SerializeField] private ParticleSystem[] particlesOnPress;

        [Sirenix.OdinInspector.ReadOnly][SerializeField] private IdleUnitType unitType;

        [SerializeField] [ReadOnly] private float currentTimer;
        [SerializeField] [ReadOnly] private float currentRatio;
        [SerializeField] [ReadOnly] private float fillTimer;

        public void ButtonPressed()
        {
            PlayParticles();
            ShowAsPerStatus(ButtonStatus.Disabled);
            NotificationParam idleButtonPressed = new NotificationParam(Mode.intData);
            idleButtonPressed.intData.Add((int)unitType);
            App.Notify(Notification.ButtonPressed);
            App.Notify(Notification.IdleButtonPressed, idleButtonPressed);
        }

        private void PlayParticles()
        {
            for (int i = 0; i < particlesOnPress.Length; i++)
            {
                particlesOnPress[i].Play();
            }
        }

        public void SetUp(IdleInfo info)
        {
            SetUpData(info);
            
            if (!info.idleActivation.isUnlocked)
            {
                ShowAsPerStatus(ButtonStatus.Locked);
            }
            else if (info.currentCount == info.data.totalCount)
            {
                ShowAsPerStatus(ButtonStatus.Finished);
            }
            else
            {
                if (info.idleActivation.isAffordable && info.idleActivation.isClickable)
                {
                    ShowAsPerStatus(ButtonStatus.Enabled);
                }
                else
                {
                    ShowAsPerStatus(ButtonStatus.Disabled);
                }
            }
        }

        private void SetUpData(IdleInfo info)
        {
            SetUpCount(info.currentCount, info.data.totalCount);
            SetUpCost(info.nextCost.cost.cost);
            SetUpIncreaseUnit(info.defaultUnitIncreasePerSecond, info.permanentMultiplier);
            if (info.data.hasTimerBeforeNextClick)
            {
                fillTimer = info.currentBlockTime;
            }
            else
            {
                fillTimer = 0.0f;
            }
            unitType = info.unitType;
        }

        private void SetUpIncreaseUnit(IdleCurrency unitIncreasePerSecond, IdleCurrency permanentMultiplier)
        {
            IdleCurrency currentUnitsPerInNextClickPerSecond =
                unitIncreasePerSecond * permanentMultiplier * App.GetLevelData().Prestige *
                App.GetLevelData().currentMultiplier;
            for (int i = 0; i < increaseUnitTexts.Length; i++)
            {
                increaseUnitTexts[i].text = currentUnitsPerInNextClickPerSecond.ToShortString() + "/sec";
            }
        }

        private void SetUpCost(IdleCurrency cost)
        {
            IdleCurrency currentCost = cost * App.GetLevelData().currentMultiplier;
            for (int i = 0; i < costText.Length; i++)
            {
                costText[i].text = currentCost.ToShortString();
            }
        }

        private void SetUpCount(int currentCount, int totalCount)
        {
            if (currentCount != totalCount)
            {
                float amountOfFill = (float) currentCount / (float) totalCount;
                countFill.fillAmount = amountOfFill;
            }
            else
            {
                //Finished
                countFill.fillAmount = 1.0f;
            }

            for (int i = 0; i < countComparisonText.Length; i++)
            {
                countComparisonText[i].text = currentCount + "/" + totalCount;
            }
        }

        //Fill related method
        public void StartFill()
        {
            Updater updater = new Updater();
            updater.action = OnUpdate;
            updater.id = unitType.ToString() + ".OnUpdate";
            App.GetUpdater().AddAction(updater, UpdateType.Update);
        }

        private void OnUpdate()
        {
            currentTimer += Time.deltaTime;
            currentRatio = currentTimer / fillTimer;
            currentRatio = Mathf.Clamp01(currentRatio);
            SetBlockFill();
            if (currentTimer > fillTimer)
            {
                string id = unitType.ToString() + ".OnUpdate";
                App.GetUpdater().RemoveAction(id);
                FillComplete();
            }
        }

        private void FillComplete()
        {
            NotificationParam param = new NotificationParam(Mode.intData);
            param.intData.Add((int)unitType);
            App.Notify(Notification.IdleButtonFillComplete, param);
        }

        private void ShowAsPerStatus(ButtonStatus status)
        {
            HideAll();

            switch (status)
            {
                case ButtonStatus.Disabled:
                    for (int i = 0; i < disabledGameObjects.Length; i++)
                    {
                        disabledGameObjects[i].Show();
                    }
                    break;
                
                case ButtonStatus.Enabled:
                    for (int i = 0; i < enabledGameObjects.Length; i++)
                    {
                        enabledGameObjects[i].Show();
                    }

                    currentTimer = 0.0f;
                    currentRatio = 0.0f;
                    SetBlockFill();
                    break;
                
                case ButtonStatus.Locked:
                    for (int i = 0; i < lockedGameObjects.Length; i++)
                    {
                        lockedGameObjects[i].Show();
                    }
                    break;
                
                case ButtonStatus.Finished:
                    for (int i = 0; i < finishedGameObjects.Length; i++)
                    {
                        finishedGameObjects[i].Show();
                    }
                    break;
            }
        }

        private void SetBlockFill()
        {
            fillImage.fillAmount = currentRatio;
        }

        private void HideAll()
        {
            for (int i = 0; i < enabledGameObjects.Length; i++)
            {
                enabledGameObjects[i].Hide();
            }
            for (int i = 0; i < disabledGameObjects.Length; i++)
            {
                disabledGameObjects[i].Hide();
            }
            for (int i = 0; i < lockedGameObjects.Length; i++)
            {
                lockedGameObjects[i].Hide();
            }
            for (int i = 0; i < finishedGameObjects.Length; i++)
            {
                finishedGameObjects[i].Hide();
            }
        }
    }

    public enum ButtonStatus
    {
        Locked,
        Disabled,
        Enabled,
        Finished,
    }
}
