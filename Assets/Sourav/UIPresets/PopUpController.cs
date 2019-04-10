using DG.Tweening;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Utilities.Extensions;
using UnityEngine;

namespace Sourav.UIPresets
{
	public class PopUpController : Engine.Core.ControllerRelated.Controller
	{
		[SerializeField] private GameObject bgPanel;
		[SerializeField] private PopUp[] popUps;

		[SerializeField] private GameObject currentPopUp;
		
		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.OpenPopUp:
					HideAllPopUps();
					currentPopUp = GetCorrectPopUp((PopUpType) param.intData[0]);
					if (currentPopUp != null)
					{
						bgPanel.Show();
						currentPopUp.Show();
						DOTween.Restart(currentPopUp, "popUpShow");
					}
					break;
				
				case Notification.ClosePopUp:
					if (currentPopUp != null)
					{
						currentPopUp.Hide();
						currentPopUp.transform.localScale = Vector3.zero;
					}
					bgPanel.Hide();
					break;
			}
		}

		private void HideAllPopUps()
		{
			for (int i = 0; i < popUps.Length; i++)
			{
				if (popUps[i].popUp != null)
				{
					popUps[i].popUp.Hide();
				}
			}
		}
		
		private GameObject GetCorrectPopUp(PopUpType type)
		{
			GameObject popUp = null;
			
			for (int i = 0; i < popUps.Length; i++)
			{
				if (popUps[i].type == type)
				{
					popUp = popUps[i].popUp;
					break;
				}
			}

			return popUp;
		}
		
		
	}

	public enum PopUpType
	{
		Settings,
		Store
	}

	[System.Serializable]
	public class PopUp
	{
		public PopUpType type;
		public GameObject popUp;
	}
}
