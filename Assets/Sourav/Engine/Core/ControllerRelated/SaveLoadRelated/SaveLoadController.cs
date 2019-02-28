using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Utilities.Scripts;
using UnityEngine;

namespace Sourav.Engine.Core.ControllerRelated.SaveLoadRelated
{
	public class SaveLoadController : Controller
	{
		[SerializeField] private string saveGameName;
		
		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.LoadData:
					LoadData();
					App.GetNotificationCenter().Notify(Notification.DataLoaded);
					break;
				
				case Notification.SaveData:
					SaveData();
					break;
			}
		}

		private void OnApplicationPause(bool isPaused)
		{
			if (isPaused)
			{
				if (App.GetData().GetComponent<LevelData>().isDataChanged)
				{
					SaveData();
					App.GetData().GetComponent<LevelData>().isDataChanged = false;
				}
			}
//			else
//			{
//				LoadData();
//			}
		}

		private void SaveData()
		{
			SaveGame data = App.GetData().GetComponent<LevelData>().GetCurrentData();
			string dataString = JsonUtility.ToJson(data);
			FileIO.WriteData(dataString);
		}

		private void LoadData()
		{
			if (FileIO.FileExists())
			{
				string stringData = FileIO.ReadData();
				SaveGame data = JsonUtility.FromJson<SaveGame>(stringData);
				App.GetData().GetComponent<LevelData>().LoadData(data);
			}
			else
			{
				App.GetData().GetComponent<LevelData>().SetDefault();
				SaveData();
			}
		}
	}
}
