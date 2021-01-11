using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.Ads;
using Sourav.Engine.Editable.ButtonRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using UnityEngine;
using Sourav.Engine.UIPresets.PopUp;

namespace Sourav.Engine.Editable.ControllerRelated
{
	public class ButtonController : Core.ControllerRelated.Controller 
	{
		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.ButtonPressedInternally:
					OnButtonPressed((ButtonType)param.intData["buttonType"]);
					break;
			}
		}

		public void OnButtonPressed(ButtonType button)
		{
			App.Notify(Notification.ButtonPressed);
			App.Notify(Notification.HapticImpactHeavy);
			
			switch (button)
			{
				#region GAMEPLAY RELATED
				case ButtonType.Play:
					App.Notify(Notification.StartGame);
					break;
				
				case ButtonType.Hint:
					// App.GetData<LevelCommonData>().rvType = RVType.HintRV;
					// App.Notify(Notification.ShowRewardVideo);
					// App.Notify(Notification.UseHint);
					break;
				
				case ButtonType.Continue:
					App.GetData<AdsData>().rvType = RVType.Continue;
					App.Notify(Notification.ShowRewardVideo);
					// App.Notify(Notification.ContinueGame);
					break;
				
				case ButtonType.GetOfflineCoinDiamond:
					App.Notify(Notification.OfflineDiamondAttempt);
					break;
				
				case ButtonType.Get2xCoins:
					App.GetData<AdsData>().rvType = RVType.Get2x;
					App.Notify(Notification.ShowRewardVideo);
					break;

				case ButtonType.TapperButton:
					App.Notify(Notification.TapperButtonClicked);
					break;
				
				case ButtonType.CoinTapperButton:
					App.Notify(Notification.CoinTapperTapped);
					break;
				
				case ButtonType.ShowBook:
					App.Notify(Notification.ShowBook);
					break;
				
				case ButtonType.GoToCurrentPointOfInterest:
					App.Notify(Notification.UnlockedNewLevel);
					break;
				
				case ButtonType.NoThanksVideoOfflineEarning:
					App.Notify(Notification.CoinsAdded);
					break;
				
				case ButtonType.OfflineCoinUpgrade:
					App.Notify(Notification.OfflineCoinsUpgraded);
					break;
				
				case ButtonType.CoinTapperUpgrade:
					App.Notify(Notification.TapperCoinsUpgraded);
					break;
				
				// #region TIME TRAVEL
				// case ButtonType.TimeTravelFree:
				// 	int indexOfTTFree = App.GetData<TimeTravelData>().GetFreeTTIndex();
				// 	if (indexOfTTFree >= 0)
				// 	{
				// 		App.GetData<TimeTravelData>().seconds = App.GetData<TimeTravelData>().ttData[indexOfTTFree].secondsofTT;
				// 		App.Notify(Notification.TimeTravelSecondsUpdated);
				// 		App.Notify(Notification.StartTimeTravel);
				// 		// App.GetData<AdsData>().rvType = RVType.TimeTravel;
				// 		// App.Notify(Notification.ShowRewardVideo);
				// 	}
				// 	break;
				//
				// case ButtonType.TimeTravelVideo:
				// 	int indexOfTTRV = App.GetData<TimeTravelData>().GetFreeTTIndex();
				// 	if (indexOfTTRV >= 0)
				// 	{
				// 		App.GetData<TimeTravelData>().seconds = App.GetData<TimeTravelData>().ttData[indexOfTTRV].secondsofTT;
				// 		App.Notify(Notification.TimeTravelSecondsUpdated);
				// 		// App.Notify(Notification.StartTimeTravel);
				// 		App.GetData<AdsData>().rvType = RVType.TimeTravel;
				// 		App.Notify(Notification.ShowRewardVideo);
				// 	}
				// 	break;
				//
				// case ButtonType.TimeTravel1:
				// 	NotificationParam tt1 = new NotificationParam(Mode.intData);
				// 	tt1.intData["ttIndex"] = 1;
				// 	App.Notify(Notification.TimeTravelPurchaseAttempt, tt1);
				// 	break;
				//
				// case ButtonType.TimeTravel2:
				// 	NotificationParam tt2 = new NotificationParam(Mode.intData);
				// 	tt2.intData["ttIndex"] = 2;
				// 	App.Notify(Notification.TimeTravelPurchaseAttempt, tt2);
				// 	break;
				//
				// case ButtonType.TimeTravel3:
				// 	NotificationParam tt3 = new NotificationParam(Mode.intData);
				// 	tt3.intData["ttIndex"] = 3;
				// 	App.Notify(Notification.TimeTravelPurchaseAttempt, tt3);
				// 	break;
				//
				// case ButtonType.TimeTravel4:
				// 	NotificationParam tt4 = new NotificationParam(Mode.intData);
				// 	tt4.intData["ttIndex"] = 4;
				// 	App.Notify(Notification.TimeTravelPurchaseAttempt, tt4);
				// 	break;
				//
				// case ButtonType.TimeTravelDeal:
				// 	int indexOfCombo = App.GetData<IAPData>().GetIndex(IAPType.DIAMONDSANDTT);
				// 	if (indexOfCombo >= 0)
				// 	{
				// 		App.GetData<TimeTravelData>().seconds = App.GetData<IAPData>().iaps[indexOfCombo].value2;
				// 		App.Notify(Notification.StartTimeTravel);
				// 	}
				// 	break;
				// #endregion
				
				#region FRV
				case ButtonType.FRV:
					App.Notify(Notification.FlyingRVClicked);
					break;
				
				case ButtonType.FRVVideo:
					App.GetData<AdsData>().rvType = RVType.FlyingRV;
					App.Notify(Notification.ShowRewardVideo);
					break;
				
				case ButtonType.FRVDiamond:
					App.Notify(Notification.FRVPurchaseAttempt);
					break;
				#endregion
				
				case ButtonType.DiamondsAdded:
					App.Notify(Notification.DiamondsAdded);
					break;

				// case ButtonType.GirlTutorialComplete:
				// 	App.Notify(Notification.HideGirlTutorial);
				// 	break;
				#endregion
				
				#region POP UP RELATED
				case ButtonType.Settings:
					OpenPopUp(PopUpType.Settings);
					break;
				
				case ButtonType.Store:
					// OpenPopUp(PopUpType.Store);
					App.Notify(Notification.ShowStore);
					break;
				
				case ButtonType.Upgrades:
					OpenPopUp(PopUpType.Upgrades);
					break;
				
				case ButtonType.TimeTravel:
					if (!App.GetData<LevelCommonData>().HasTravelledTimeBefore)
					{
						App.Notify(Notification.ShowFreeTimeTravel);
					}
					else
					{
						App.Notify(Notification.ShowTimeTravelStore);
					}
					break;
				
				case ButtonType.TimeTravelStore:
					App.Notify(Notification.ShowTimeTravelStore);
					break;
				#endregion

				#region IAP RELATED
				case ButtonType.RestorePurchase:
					App.Notify(Notification.RestorePurchase);
					break;
				
				case ButtonType.RemoveAds:
					// D.Log("6");
					App.Notify(Notification.RemoveAds);
					break;
				
				case ButtonType.Diamonds1:
					// D.Log("0");
					App.Notify(Notification.Diamonds1);
					break;
				
				case ButtonType.Diamonds2:
					// D.Log("1");
					App.Notify(Notification.Diamonds2);
					break;
				
				case ButtonType.Diamonds3:
					// D.Log("2");
					App.Notify(Notification.Diamonds3);
					break;
				
				case ButtonType.Diamonds4:
					// D.Log("3");
					App.Notify(Notification.Diamonds4);
					break;
				
				case ButtonType.Diamonds5:
					// D.Log("4");
					App.Notify(Notification.Diamonds5);
					break;
				
				case ButtonType.DiamondsCombo:
					// D.Log("5");
					App.Notify(Notification.DiamondsCombo);
					break;
				
				case ButtonType.DiamondsFree:
					App.GetData<AdsData>().rvType = RVType.Diamonds;
					App.Notify(Notification.ShowRewardVideo);
					break;
				#endregion
				
				#region AD RELATED
				case ButtonType.WatchVideoAd:
					Debug.Log("Watch Video Ad Button");
					App.Notify(Notification.ShowRewardVideo);
					break;
				
				case ButtonType.ShowFSAd:
					Debug.Log("Watch FS Ad Button");
					App.Notify(Notification.ShowInterstitial);
					break;
				
				case ButtonType.ShowAdsOffline:
					App.GetData<AdsData>().rvType = RVType.Get2x;
					App.Notify(Notification.ShowRewardVideo);
					break;
				#endregion
			}
		}

		private void OpenPopUp(PopUpType type)
		{
			NotificationParam popUpStore = new NotificationParam(Mode.intData);
			popUpStore.intData["popUpType"] = (int) type;
			App.Notify(Notification.ShowPopup, popUpStore);
		}
	}
}
