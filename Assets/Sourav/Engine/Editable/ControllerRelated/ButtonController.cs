using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.ButtonRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.UIPresets;
using UnityEngine;
using UnityEngine.UI;
using Controller = Sourav.Engine.Core.ControllerRelated.Controller;

namespace Sourav.Engine.Editable.ControllerRelated
{
	public class ButtonController : Core.ControllerRelated.Controller 
	{
		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			
		}

		public void OnButtonPressed(ButtonType button)
		{
			if (App.IsGamePaused())
			{
				Debug.Log("App is paused");
				return;
			}
			
			App.GetNotificationCenter().Notify(Notification.ButtonPressed);
			
			switch (button)
			{
				case ButtonType.Play:
					App.GetNotificationCenter().Notify(Notification.PlayGame);
					break;
				
				case ButtonType.Settings:
					if (!App.GetLevelData().IsTutorialOver)
					{
						return;
					}
					OpenPopUp(PopUpType.Settings);
					break;
				
				case ButtonType.Store:
					if (!App.GetLevelData().IsTutorialOver)
					{
						return;
					}
					OpenPopUp(PopUpType.Store);
					break;
				
				case ButtonType.PopUpClose:
					if (!App.GetLevelData().IsTutorialOver)
					{
						return;
					}
					App.GetNotificationCenter().Notify(Notification.ClosePopUp);
					break;
				
				case ButtonType.Home:
					if (!App.GetLevelData().IsTutorialOver)
					{
						return;
					}
					App.GetNotificationCenter().Notify(Notification.HomeButtonPressed);
					break;
				
				case ButtonType.Coin1:
					Debug.Log("0");
					App.GetNotificationCenter().Notify(Notification.Coins1);
					break;
				
				case ButtonType.Coin2:
					Debug.Log("1");
					App.GetNotificationCenter().Notify(Notification.Coins2);
					break;
				
				case ButtonType.Coin3:
					Debug.Log("2");
					App.GetNotificationCenter().Notify(Notification.Coins3);
					break;
				
				case ButtonType.Coin4:
					Debug.Log("3");
					App.GetNotificationCenter().Notify(Notification.Coins4);
					break;
				
				case ButtonType.Coin5:
					Debug.Log("4");
					App.GetNotificationCenter().Notify(Notification.Coins5);
					break;
				
				case ButtonType.Coin6:
					Debug.Log("5");
					App.GetNotificationCenter().Notify(Notification.Coins6);
					break;
				
				case ButtonType.RemoveAds:
					Debug.Log("6");
					App.GetNotificationCenter().Notify(Notification.RemoveAds);
					break;
				
				case ButtonType.RestorePurchase:
					App.GetNotificationCenter().Notify(Notification.RestorePurchase);
					break;
				
				case ButtonType.WatchVideoAd:
					App.GetLevelData().isFromDoubleEarnings = false;
					App.GetNotificationCenter().Notify(Notification.ShowRewardVideo);
					break;
			}
		}

		private void OpenPopUp(PopUpType type)
		{
			NotificationParam popUpStore = new NotificationParam(Mode.intData);
			popUpStore.intData.Add((int) type);
			App.GetNotificationCenter().Notify(Notification.OpenPopUp, popUpStore);
		}
	}
}
