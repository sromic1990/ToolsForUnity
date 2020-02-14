using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
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
					App.GetNotificationCenter().Notify(Notification.GameLoaded);
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
				if (App.GetData().GetComponent<LevelCommonData>().isDataChanged)
				{
					SaveData();
					App.GetData().GetComponent<LevelCommonData>().isDataChanged = false;
				}
			}
//			else
//			{
//				LoadData();
//			}
		}

#if ODIN
		[Sirenix.OdinInspector.Button()]
#else
		[Sourav.Utilities.Scripts.Attributes.Button()]
		#endif
		private void SaveData()
		{
			SaveGame data = App.GetData().GetComponent<LevelCommonData>().GetCurrentData();
			string dataString = JsonUtility.ToJson(data);
			FileIO.WriteData(dataString);
		}

		private void LoadData()
		{
			if (FileIO.FileExists())
			{
				string stringData = FileIO.ReadData();
				SaveGame data = JsonUtility.FromJson<SaveGame>(stringData);
				App.GetData().GetComponent<LevelCommonData>().LoadData(data);
			}
			else
			{
				App.GetLevelData().SetDefault();
				SaveData();
			}
			#if IDLEGAME
			App.GetLevelData().isLoaded = true;
			#endif
		}
	}
}
