using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sourav.Engine.Core.ControllerRelated.PauseResumeRelated;
using Sourav.Engine.Core.ControllerRelated.SaveLoadRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Editable.Timer;
using Sourav.IdleGameEngine.UpdateRelated;
using Sourav.Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Sourav.Engine.Engine.Core.ApplicationRelated
{
	public class App : MonoBehaviour
	{
		private static App Instance;

		#region FIELDS
		private static NotificationCenter _notificationCenter;
		[SerializeField][ReadOnly] private List<Sourav.Engine.Core.ControllerRelated.Controller> _controllers;
		private static CoroutineHandler _coroutineHandler;
		private static UpdateElement _updateElement;
		private static List<CommonData> commonData;

		private bool isInitialized;
		#endregion

		#region METHODS
		#region MONO METHODS
		private void Awake()
		{
			if (Instance == null)
			{
				Instance = this;
			}
			else
			{
				Destroy(transform.parent.gameObject);
				return;
			}

			SceneManager.sceneLoaded += (arg0, scene) =>
			{
				// D.Log($"Scene Changed to {SceneManager.GetActiveScene().name}");
				Initialize();
			};
			
			SceneManager.sceneUnloaded += arg0 => 
			{
				// D.Log($"Scene Changed to {SceneManager.GetActiveScene().name}");
				Initialize();
			};
			
			// Initialize();
		}
		#endregion

		#region INIT METHODS
		private void Initialize()
		{
			CreateCoroutineHandler();

			CreateAndPopulateUpdater();

			FindAndPopulateAllCommonData();

			CreateSystemControllers();

			FindAndPopulateAllControllers();
			CreateNotificationCenter();

			isInitialized = true;
		}
		
		private void CreateCoroutineHandler()
		{
			if (_coroutineHandler == null)
			{
				GameObject coroutineHandler = GetObject("CoroutineHandler", this.transform);
				_coroutineHandler = coroutineHandler.AddComponent<CoroutineHandler>();
			}
		}

		private void CreateAndPopulateUpdater()
		{
			if (_updateElement == null)
			{
				GameObject updateElement = GetObject("Updater", this.transform);
				_updateElement = updateElement.AddComponent<UpdateElement>();
			}
		}

		private void FindAndPopulateAllCommonData()
		{
			commonData = new List<CommonData>();
			
			CommonData[] allData = Resources.FindObjectsOfTypeAll<CommonData>();
			commonData = allData.ToList();
		}

		private void CreateSystemControllers()
		{
			if(isInitialized)
				return;
			
			GameObject controllerObj = GetObject("SystemControllers", this.transform);
			GameObject saveLoadController = GetObject("SaveLoadController", controllerObj.transform);
			SaveLoadController slC = saveLoadController.AddComponent<SaveLoadController>();
			GameObject pauseResumeController = GetObject("PauseResumeController", controllerObj.transform);
			PauseResumeController psC = pauseResumeController.AddComponent<PauseResumeController>();
		}

		private void FindAndPopulateAllControllers()
		{
			Sourav.Engine.Core.ControllerRelated.Controller[] controllers =
				Resources.FindObjectsOfTypeAll<Sourav.Engine.Core.ControllerRelated.Controller>();
			List<Sourav.Engine.Core.ControllerRelated.Controller> tempController = controllers.ToList();
			this._controllers = tempController;
		}

		private void CreateNotificationCenter()
		{
			_notificationCenter = new NotificationCenter(GetAllControllers());
		}
		#endregion
		
		#region GET METHODS
		public static T GetData<T>() where T : CommonData
		{
			for (int i = 0; i < commonData.Count; i++)
			{
				if (commonData[i] is T)
				{
					return (T)commonData[i];
				}
			}

			Debug.LogError($"Data type not found in the commonData");
			return null;
		}

		public LevelCommonData GetLevelData()
		{
			return GetData<LevelCommonData>();
		}
		
		public static CoroutineHandler GetCoroutineHandler()
		{
			return _coroutineHandler;
		}
		
		public static UpdateElement GetUpdater()
		{
			return _updateElement;
		}
		#endregion

		#region NOTIFICATION RELATED
		public static void Notify(Notification notification, NotificationParam param = null)
		{
			_notificationCenter.Notify(notification, param);
		}
		#endregion
		
		#region NOTIFICATIONS LOCK UNLOCK METHODS
		public void LockNotification()
		{
			_notificationCenter.LockNotification();
		}

		public void UnlockNotification()
		{
			_notificationCenter.UnlockNotificationStatus();
		}
		#endregion
		
		#region CONTROLLER RELATED METHODS
		public Sourav.Engine.Core.ControllerRelated.Controller GetController<T>() where T : Sourav.Engine.Core.ControllerRelated.Controller
		{
			for (int i = 0; i < _controllers.Count; i++)
			{
				if (_controllers[i] is T)
				{
					return (T) _controllers[i];
				}
			}

			return null;
		}

		public Dictionary<string, Sourav.Engine.Core.ControllerRelated.Controller> GetAllControllers()
		{
			Dictionary<string, Sourav.Engine.Core.ControllerRelated.Controller> controllers = new Dictionary<string, Sourav.Engine.Core.ControllerRelated.Controller>();
			for (int i = 0; i < this._controllers.Count; i++)
			{
				controllers[this._controllers[i].GetType().Name] = this._controllers[i];
			}
			
			return controllers;
		}

		public void SetControllers(Sourav.Engine.Core.ControllerRelated.Controller[] controllers)
		{
			this._controllers = new List<Sourav.Engine.Core.ControllerRelated.Controller>();
			for (int i = 0; i < controllers.Length; i++)
			{
				this._controllers.Add(controllers[i]);
			}
		}
		
		#endregion
		
		#region UTIL METHODS
		public bool IsGamePaused()
		{
			bool isPaused = false;
			Sourav.Engine.Core.ControllerRelated.Controller pauseController = GetController<PauseResumeController>();
			if (pauseController != null)
			{
				if (pauseController is PauseResumeController)
				{
					isPaused = ((PauseResumeController) pauseController).IsPaused;
				}
			}

			return isPaused;
		}
		#endregion

		#region HELPER
		private GameObject GetObject(string name, Transform parent)
		{
			GameObject gObj = new GameObject(name);
			gObj.transform.position = Vector3.zero;
			gObj.transform.SetParent(parent);
			return gObj;
		}
		#endregion
		#endregion
	}
}
