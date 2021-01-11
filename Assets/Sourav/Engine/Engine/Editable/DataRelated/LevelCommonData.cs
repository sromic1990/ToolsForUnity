using Sirenix.OdinInspector;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using Sourav.Idle;
using UnityEngine;
using Sourav.Engine.SO;

// using SuperPower.GameCode.Data;

namespace Sourav.Engine.Editable.DataRelated
{
	//Serialize this class and store in the disk for savegames
	public class LevelCommonData : CommonData
	{
		#region ALMOST FIXED FOR EACH GAME
		[SerializeField] private SaveGame game;

		#region LAUNCH RELATED
		public bool HasBeenLaunchedBefore
		{
			get { return game.hasBeenLaunchedBefore; }
			set
			{
				game.hasBeenLaunchedBefore = value;
				DataChanged();
			}
		}
		#endregion
		
		#region LEVEL RELATED
		public int CurrentLevel
		{
			get { return game.currentLevel; }
			set
			{
				game.currentLevel = value;
				DataChanged();
			}
		}

		public int CurrentLevelActual
		{
			get { return game.currentLevelActual; }
			set
			{
				game.currentLevelActual = value;
				DataChanged();
			}
		}

		public bool AreLevelsExhausted
		{
			get { return game.areLevelsExhausted; }
			set
			{
				game.areLevelsExhausted = value;
				DataChanged();
			}
		}
		#endregion

		#region TUTORIAL RELATED
		public bool IsTutorialOver
		{
			get { return game.isTutorialOver; }
			set
			{
				game.isTutorialOver = value;
				DataChanged();
			}
		}

		public int CurrentTutorialIndex
		{
			get
			{
				return game.currentTutorialIndex;
			}
			set
			{
				game.currentTutorialIndex = value;
				DataChanged();
			}
		}
		#endregion

		#region TOGGLES RELATED
		public bool IsMusicOn
		{
			get { return game.isMusicOn; }
			set
			{
				game.isMusicOn = value;
				DataChanged();
			}
		}

		public bool IsSfxOn
		{
			get { return game.isSfxOn; }
			set
			{
				game.isSfxOn = value;
				DataChanged();
			}
		}

		public bool IsVibrationOn
		{
			get { return game.isVibrationOn; }
			set
			{
				game.isVibrationOn = value;
				DataChanged();
			}
		}
		#endregion

		#region ADS RELATED
		public bool AdsInactive
		{
			get { return game.adsInactive; }
			set
			{
				game.adsInactive = value;
				DataChanged();
			}
		}
		
		public int AdLastLevel
		{
			get { return game.lastAdLevel; }
			set
			{
				game.lastAdLevel = value;
				DataChanged();
			}
		}
		
		public int AdsShown
		{
			get { return game.adsShown; }
			set
			{
				game.adsShown = value;
				DataChanged();
			}
		}
		#endregion
		
		#region TIME TRAVEL RELATED
		public bool HasTravelledTimeBefore
		{
			get { return game.hasTravelledTimeBefore; }
			set
			{
				game.hasTravelledTimeBefore = value;
				DataChanged();
			}
		}
		#endregion
		
		#region IDLE GAME RELATED
		public string LastDateTime
		{
			get { return game.lastDateTime; }
			set
			{
				game.lastDateTime = value;
				DataChanged();
			}
		}
		
		public IdleCurrency Units
		{
			get { return game.units; }
			set
			{
				game.units = value;
				App.Notify(Notification.UnitsUpdated);
				DataChanged();
			}
		}

		public IdleCurrency Diamonds
		{
			get { return game.diamonds; }
			set
			{
				game.diamonds = value;
				DataChanged();
			}
		}

		public int UnitsProgressionLevel
		{
			get { return game.unitsProgressionLevel; }
			set
			{
				game.unitsProgressionLevel = value;
				DataChanged();
			}
		}

		public int CoinsProgressionLevel
		{
			get { return game.coinProgressionLevel; }
			set
			{
				game.coinProgressionLevel = value;
				DataChanged();
			}
		}

		public int OfflineProgressionLevel
		{
			get { return game.offlineProgressionLevel; }
			set
			{
				game.offlineProgressionLevel = value;
				DataChanged();
			}
		}
		
		// public IdleCurrency UnitsPerSecond
		// {
		// 	get { return game.unitsPerSecond; }
		// 	set
		// 	{
		// 		game.unitsPerSecond = value;
		// 		App.Notify(Notification.UnitsUpdated);
		// 		DataChanged();
		// 	}
		// }
		//
		// public IdleCurrency UnitIncrement
		// {
		// 	get { return game.unitIncrement; }
		// 	set
		// 	{
		// 		game.unitIncrement = value;
		// 		DataChanged();
		// 	}
		// }
		//
		// public IdleCurrency UnitPerTap
		// {
		// 	get { return game.unitPerTap; }
		// 	set
		// 	{
		// 		game.unitPerTap = value;
		// 		DataChanged();
		// 	}
		// }
		#endregion
		
		#region GAME SPECIFIC

		// public List<LockUnLockHistoryData> HistoryData
		// {
		// 	get => game.historyData;
		// }
		#endregion

		#region VERSION RELATED
		public string Version
		{
			get { return game.version; }
			set
			{
				game.version = value;
				DataChanged();
			}
		}
		#endregion

		#region URL
		public string FactsURL
		{
			get { return game.factsURL; }
			set
			{
				game.factsURL = value;
				DataChanged();
			}
		}

		public string GameplayDataURL
		{
			get { return game.gameplayDataURL; }
			set
			{
				game.gameplayDataURL = value;
				DataChanged();
			}
		}
		
		public string CoinProgressionURL
		{
			get { return game.coinProgressionURL; }
			set
			{
				game.coinProgressionURL = value;
				DataChanged();
			}
		}

		public string ClicksProgressionURL
		{
			get { return game.clicksProgressionURL; }
			set
			{
				game.clicksProgressionURL = value;
				DataChanged();
			}
		}

		public string CoinUpgradeURL
		{
			get { return game.coinUpgradeURL; }
			set
			{
				game.coinUpgradeURL = value;
				DataChanged();
			}
		}

		public string OfflineUpgradeURL
		{
			get { return game.offlineUpgradeURL; }
			set
			{
				game.offlineUpgradeURL = value;
				DataChanged();
			}
		}
		#endregion

		public bool isDataChanged;

		private void DataChanged()
		{
			isDataChanged = true;
			App.Notify(Notification.DataChanged);
#if UNITY_EDITOR
			App.Notify(Notification.SaveGame);
#endif
		}

		public SaveGame GetCurrentData()
		{
			// game.saveGames = new List<SaveData>();
			// game.stage = App.GetData<StageData>().stage;
			// for (int i = 0; i < App.GetData<GameData>().saveDataList.Count; i++)
			// {
			// 	SaveData save = new SaveData();
			//
			// 	save.data = App.GetData<GameData>().saveDataList[i].data;
			// 	// save.keys = App.GetData<GameData>().saveDataList[i].keys;
			// 	// save.keyboard = App.GetData<GameData>().saveDataList[i].keyboard;
			// 	// save.randomizedIntegers = App.GetData<GameData>().saveDataList[i].randomizedIntegers;
			// 	// save.nextReplacementKey = App.GetData<GameData>().saveDataList[i].nextReplacementKey;
			// 	
			// 	game.saveGames.Add(save);
			// }
			// // game.maxOfflineSeconds = AppGame.GetData<OfflineData>().maxOfflineSeconds;
			// // game.maxOfflineEarningPercentage = AppGame.GetData<OfflineData>().maxOfflineEarningPercentage;
			// game.colorThemeIndex = App.GetData<ColorData>().colorThemeIndex;
			
			// SaveHistoryData();
			// SaveTutorialData();
			return game;
		}

		public void LoadData(SaveGame saveGame)
		{
			game = saveGame;
			// LoadHistoryData();
			// LoadTutorialData();
			
			// App.GetData<GameData>().saveDataList = game.saveGames;
			// App.GetData<StageData>().stage = game.stage;
			// App.GetData<ColorData>().colorThemeIndex = game.colorThemeIndex;
			// AppGame.GetData<OfflineData>().maxOfflineSeconds = game.maxOfflineSeconds;
			// AppGame.GetData<OfflineData>().maxOfflineEarningPercentage = game.maxOfflineEarningPercentage;
		}

		// private void SaveHistoryData()
		// {
		// 	if(HistoryData == null)
		// 		return;
		// 	
		// 	for (int i = 0; i < HistoryData.Count; i++)
		// 	{
		// 		for (int j = 0; j < App.GetData<GameData>().history.Length; j++)
		// 		{
		// 			if (HistoryData[i].type == App.GetData<GameData>().history[j].type)
		// 			{
		// 				HistoryData[i].lockStatus = App.GetData<GameData>().history[j].lockStatus;
		// 				HistoryData[i].clicks = App.GetData<GameData>().history[j].currentNumberOfClicks;
		//
		// 				break;
		// 			}
		// 		}
		// 	}
		// }
		// private void LoadHistoryData()
		// {
		// 	for (int i = 0; i < HistoryData.Count; i++)
		// 	{
		// 		for (int j = 0; j < App.GetData<GameData>().history.Length; j++)
		// 		{
		// 			if (HistoryData[i].type == App.GetData<GameData>().history[j].type)
		// 			{
		// 				App.GetData<GameData>().history[j].lockStatus = HistoryData[i].lockStatus;
		// 				App.GetData<GameData>().history[j].currentNumberOfClicks = HistoryData[i].clicks;
		//
		// 				break;
		// 			}
		// 		}
		// 	}
		// }

		
		// private void SaveTutorialData()
		// {
		// 	game.tutorialData = new List<TutorialCoreData>();
		// 	for (int i = 0; i < App.GetData<TutorialData>().tutorials.Count; i++)
		// 	{
		// 		TutorialCoreData data = new TutorialCoreData();
		// 		data.type = App.GetData<TutorialData>().tutorials[i].type;
		// 		data.currentCount = App.GetData<TutorialData>().tutorials[i].currentCount;
		// 		game.tutorialData.Add(data);
		// 	}
		// }
		// private void LoadTutorialData()
		// {
		// 	for (int i = 0; i < App.GetData<TutorialData>().tutorials.Count; i++)
		// 	{
		// 		for (int j = 0; j < game.tutorialData.Count; j++)
		// 		{
		// 			if (game.tutorialData[j].type == App.GetData<TutorialData>().tutorials[i].type)
		// 			{
		// 				App.GetData<TutorialData>().tutorials[i].currentCount = game.tutorialData[j].currentCount;
		// 			}
		// 		}
		// 	}
		// 	// App.GetData<TutorialData>().SetUpData();
		// }

		#endregion

		#region DEFAULTS
		[Space(10)][Header("DEFAULTS")]
		public GameDefaultsSO defaults;
		public int defaultProgression;
		
		[Title("URL Defaults")]
		public string defaultFactsURL;
		public string defaultsGameplayDataURL;
		public string defaultsCoinsProgressionURL;
		public string defaultsClickProgressionURL;
		public string defaultsCoinUpgradeURL;
		public string defaultsOfflineUpgradeURL;
		
		
		public void SetDefault()
		{
			// D.Log(">>>>>>SET DEFAULT<<<<<<");
			
			game = new SaveGame();

			game.hasBeenLaunchedBefore = true;
			// Units = App.GetData<LevelData>().defaultCoins;
			// Diamonds = App.GetData<LevelData>().defaultDiamonds;
			UnitsProgressionLevel = defaultProgression;
			CoinsProgressionLevel = defaultProgression;
			OfflineProgressionLevel = defaultProgression;
			
			//URL DEFAULTS
			SetURLDefaults();
			//URL DEFAULTS
			
			//History Data Defaults
			// SetHistoryDataDefault();
			//History Data Defaults
			
			ResetGameplay();
			// CurrentLevel = defaults.currentLevelDefault;
			// CurrentLevelActual = defaults.currentLevelDefault;
			SetConfigurationDefault();
			// Coins = App.GetData<LevelData>().levelConfig.defaultCoins;
			// App.GetData<ColorData>().colorThemeIndex = game.colorThemeIndex;

			//Level Complete Related

			//Idle Game Related
			// LastDateTime = System.DateTime.Now.ToString(CultureInfo.InvariantCulture);
			// isFirstLaunch = true;
			//
			// Units = AppGame.GetData<GameData>().gameConfig.startValue;
			// UnitsPerSecond = AppGame.GetData<GameData>().gameConfig.defaultUnitsPerSecond;

			// App.GetData<SpinData>().SetDefault();

			// game.saveGames = new List<SaveData>();
			// App.GetData<GameData>().saveDataList = game.saveGames;

			//Offline Related
			// AppGame.GetData<OfflineData>().SetDefault();
			// game.maxOfflineSeconds = AppGame.GetData<OfflineData>().maxOfflineSeconds;
			// game.maxOfflineEarningPercentage = AppGame.GetData<OfflineData>().maxOfflineEarningPercentage;
		}

		private void SetConfigurationDefault()
		{
			IsTutorialOver = defaults.isTutorialOverDefault;
			IsSfxOn = defaults.isSfxOnDefault;
			IsMusicOn = defaults.isMusicOnDefault;
			IsVibrationOn = defaults.isVibrationOnDefault;
			AdsInactive = defaults.isAdsInactiveDefault;
			AreLevelsExhausted = false;
		}

		private void SetURLDefaults()
		{
			FactsURL = defaultFactsURL;
			GameplayDataURL = defaultsGameplayDataURL;
			CoinProgressionURL = defaultsCoinsProgressionURL;
			ClicksProgressionURL = defaultsClickProgressionURL;
			CoinUpgradeURL = defaultsCoinUpgradeURL;
			OfflineUpgradeURL = defaultsOfflineUpgradeURL;
		}

		// private void SetHistoryDataDefault()
		// {
		// 	D.Log("Set History Data Default");
		// 	List<HistoryType> histories = EnumUtil.GetValues<HistoryType>();
		// 	game.historyData = new List<LockUnLockHistoryData>();
		// 	for (int i = 0; i < histories.Count; i++)
		// 	{
		// 		if (histories[i] != HistoryType.None)
		// 		{
		// 			LockUnLockHistoryData data = new LockUnLockHistoryData();
		// 			data.type = histories[i];
		// 			data.clicks = 0;
		// 			data.lockStatus = LockStatus.Locked;
		// 			game.historyData.Add(data);
		// 		}
		// 	}
		// 	game.historyData[0].lockStatus = LockStatus.Unlocked;
		// 	LoadHistoryData();
		// }

		public void ResetGameplay()
		{
			CurrentLevel = defaults.currentLevelDefault;
			// CurrentLevelActual = defaults.currentLevelDefault;
			// game.saveGames = new List<SaveData>();
			// game.stage = new StageWords();
			// App.GetData<GameData>().saveDataList = game.saveGames;
			// App.GetData<GameData>().ResetLevelChecks();
		}
		#endregion
	}

	[System.Serializable]
	public class SaveGame
	{
		public bool hasBeenLaunchedBefore;
		
		public int currentLevel;
		public int currentLevelActual;
		public bool areLevelsExhausted;
		
		public bool isTutorialOver;
		public int currentTutorialIndex;

		public bool adsInactive;

		public bool isMusicOn;
		public bool isSfxOn;
		public bool isVibrationOn;

		public int lastAdLevel;

		public string lastDateTime;
		public int maxOfflineSeconds;
		public float maxOfflineEarningPercentage;

		public bool hasTravelledTimeBefore;
		
		#region IDLE GAME RELATED
		public IdleCurrency units;
		// public IdleCurrency unitsPerSecond;
		// public IdleCurrency unitIncrement;
		// public IdleCurrency unitPerTap;
		public int unitsProgressionLevel;
		public int coinProgressionLevel;
		public int offlineProgressionLevel;
		#endregion

		public IdleCurrency diamonds;
		
		public string version;

		public int adsShown;

		// public List<LockUnLockHistoryData> historyData;
		// public List<TutorialCoreData> tutorialData;
		
		//Trivia URL
		public string factsURL;
		//Description URL
		public string gameplayDataURL;
		//Coin Progression URL
		public string coinProgressionURL;
		//Click Progression URL
		public string clicksProgressionURL;
		//Coin Upgrade URL
		public string coinUpgradeURL;
		//Offline Upgrade URL
		public string offlineUpgradeURL;
	}
	
	// [System.Serializable]
	// public class LockUnLockHistoryData
	// {
	// 	public HistoryType type;
	// 	public LockStatus lockStatus;
	// 	public int clicks;
	// }
	//
	// [System.Serializable]
	// public class TutorialCoreData
	// {
	// 	public TutorialType type;
	// 	public int currentCount;
	// }
}
