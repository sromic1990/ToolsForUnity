using Sourav.Engine.Core.ApplicationRelated;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.DebugRelated;
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
			
			App.Notify(Notification.ButtonPressed);
			
			switch (button)
			{
				case ButtonType.Play:
					App.Notify(Notification.PlayGame);
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
					App.Notify(Notification.ClosePopUp);
					break;
				
				case ButtonType.Home:
					if (!App.GetLevelData().IsTutorialOver)
					{
						return;
					}
					App.Notify(Notification.HomeButtonPressed);
					break;
				
				case ButtonType.Coin1:
					D.Log("0");
					App.Notify(Notification.Coins1);
					break;
				
				case ButtonType.Coin2:
					D.Log("1");
					App.Notify(Notification.Coins2);
					break;
				
				case ButtonType.Coin3:
					D.Log("2");
					App.Notify(Notification.Coins3);
					break;
				
				case ButtonType.Coin4:
					D.Log("3");
					App.Notify(Notification.Coins4);
					break;
				
				case ButtonType.Coin5:
					D.Log("4");
					App.Notify(Notification.Coins5);
					break;
				
				case ButtonType.Coin6:
					D.Log("5");
					App.Notify(Notification.Coins6);
					break;
				
				case ButtonType.RemoveAds:
					D.Log("6");
					App.Notify(Notification.RemoveAds);
					break;
				
				case ButtonType.RestorePurchase:
					App.Notify(Notification.RestorePurchase);
					break;
				
				case ButtonType.WatchVideoAd:
					App.Notify(Notification.ShowRewardVideo);
					break;
			}
		}

		private void OpenPopUp(PopUpType type)
		{
			NotificationParam popUpStore = new NotificationParam(Mode.intData);
			popUpStore.intData["popUpType"] = (int) type;
			App.Notify(Notification.OpenPopUp, popUpStore);
		}
	}
}
