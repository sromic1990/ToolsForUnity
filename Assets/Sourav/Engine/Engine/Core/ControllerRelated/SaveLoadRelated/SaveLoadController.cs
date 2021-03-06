﻿using Sourav.DebugRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using Sourav.Utilities.Scripts;
using UnityEngine;

namespace Sourav.Engine.Core.ControllerRelated.SaveLoadRelated
{
	public class SaveLoadController : Controller
	{	
		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.LoadGame:
					// D.Log("Notification.LoadGame");
					LoadData();
					App.Notify(Notification.GameLoaded);
					break;
				
				case Notification.SaveGame:
					SaveData();
					break;
			}
		}

		private void OnApplicationPause(bool isPaused)
		{
			if (isPaused)
			{
				if (App.GetData<LevelCommonData>().isDataChanged)
				{
					SaveData();
					App.GetData<LevelCommonData>().isDataChanged = false;
				}
			}
//			else
//			{
//				LoadData();
//			}
		}
		
		// [Sirenix.OdinInspector.Button()]
		private void SaveData()
		{
			SaveGame data = App.GetData<LevelCommonData>().GetCurrentData();
			string dataString = JsonUtility.ToJson(data);
			FileIO.WriteData(dataString);
		}

		private void LoadData()
		{
			// D.Log("LOAD DATA");
			if (FileIO.FileExists())
			{
				string stringData = FileIO.ReadData();
				SaveGame data = JsonUtility.FromJson<SaveGame>(stringData);
				App.GetData<LevelCommonData>().LoadData(data);
			}
			else
			{
				App.GetData<LevelCommonData>().SetDefault();
				SaveData();
			}
		}
	}
}
