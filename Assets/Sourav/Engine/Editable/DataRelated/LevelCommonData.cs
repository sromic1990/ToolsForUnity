using System.Collections.Generic;
using Sourav.Engine.Editable.AdditionalLogicRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.IdleGameEngine.IdleCurrency.IdleCurrency;
using Sourav.IdleGameEngine.IdleGameData;
using UnityEngine;

namespace Sourav.Engine.Editable.DataRelated
{
	//Serialize this class and store in the disk for savegames
	public class LevelCommonData : CommonData
	{
		#region ALMOST FIXED FOR EACH GAME
		[SerializeField] private SaveGame game;

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

		public int CurrentTutorial
		{
			get { return game.currentTutorialValue; }
			set
			{
				game.currentTutorialValue = value;
				if (value >= maxTutorialLevels)
				{
					IsTutorialOver = true;
				}
				DataChanged();
			}
		}
		
		public int Coins
		{
			get { return game.coins; }
			set
			{
				game.coins = value;
				DataChanged();
			}
		}

		public bool IsTutorialOver
		{
			get { return game.isTutorialOver; }
			set
			{
				game.isTutorialOver = value; 
				DataChanged();
			}
		}

		public bool AdsInactive
		{
			get { return game.adsInactive; }
			set
			{
				game.adsInactive = value;
				DataChanged();
			}
		}

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

		public int AdLastLevel
		{
			get { return game.lastAdLevel; }
			set
			{
				game.lastAdLevel = value;
				DataChanged();
			}
		}

		#region IDLE GAME RELATED
		#if IDLEGAME
		public bool IsOfflineTimerOn
		{
			get { return game.isOfflineTimerOn; }
			set
			{
				game.isOfflineTimerOn = value;
				DataChanged();
			}
		}
		public string LastDateTime
		{
			get { return game.lastDateTime; }
			set
			{
				game.lastDateTime = value;
				DataChanged();
			}
		}

		public IdleCurrency Unit
		{
			get { return game.unit; }
			set
			{
				game.unit = value;
				App.Notify(Notification.UnitsUpdated);
				DataChanged();
			}
		}

		public IdleCurrency UnitIncrement
		{
			get { return game.unitIncrement; }
			set
			{
				game.unitIncrement = value;
				DataChanged();
			}
		}

		public IdleCurrency UnitPerTap
		{
			get { return game.unitPerTap; }
			set
			{
				game.unitPerTap = value;
				DataChanged();
			}
		}

		public float Prestige
		{
			get { return game.prestige; }
			set
			{
				game.prestige = value;
				DataChanged();
			}
		}

		public int lastDateTimeSeconds;
		public bool isLoaded;
		public IdleCurrency unitsFoundInTapThisSecond;
		public float currentMultiplier;
		#endif
		
		public List<LogicData> LogicData
		{
			get { return game.logicData; }
			set
			{
				game.logicData = value;
				DataChanged();
			}
		}

		public LogicStatus GetLogicStatus(LogicType type)
		{
			for (int i = 0; i < LogicData.Count; i++)
			{
				if (LogicData[i].type == type)
				{
					return LogicData[i].fulfillmentStatus;
				}
			}

			return LogicStatus.Unfulfilled;
		}

		public bool IsLogicPopulated
		{
			get { return game.isLogicPopulated; }
			set
			{
				game.isLogicPopulated = value;
				DataChanged();
			}
		}
		#endregion
		
		public bool isDataChanged;

		[Space(10)] [Header("Defaults")] 
		public int currentLevelDefault;
		public bool isSfxOnDefault;
		public bool isMusicOnDefault;
		public bool isVibrationOnDefault;
		public int coinsDefault;
		public int totalMoneyDefault;
		public int currentMoneyDefault;
		public bool isTutorialOverDefault;
		public bool isIntroVideoOverDefault;
		public bool isAdsInactiveDefault;
		public float currentMultiplierDefault;
		#if IDLEGAME
		public IdleCurrency defaultUnits;
		public IdleCurrency defaultUnitPerSecond;
		#endif
		
		private void DataChanged()
		{
			isDataChanged = true;
			App.GetNotificationCenter().Notify(Notification.DataChanged);
			#if UNITY_EDITOR
			App.GetNotificationCenter().Notify(Notification.SaveGame);
			#endif
		}

		public string GetDataInString()
		{
			#if IDLEGAME
			game.idleLevels = App.GetIdleData().idleLevels;
			game.idleOfflineTimerUpgrade = App.GetIdleData().idleOfflineTimerUpgrade;
			game.idleOfflineEarningUpgrade = App.GetIdleData().idleOfflineEarningUpgrade;
			game.idleTapperUpgrade = App.GetIdleData().idleTapperUpgrade;
			#endif
			return JsonUtility.ToJson(game);
		}

		public void SetData(string dataInString)
		{
			game = JsonUtility.FromJson<SaveGame>(dataInString);
			#if IDLEGAME
			App.GetIdleData().idleLevels = game.idleLevels;
			App.GetIdleData().idleOfflineTimerUpgrade = game.idleOfflineTimerUpgrade;
			App.GetIdleData().idleOfflineEarningUpgrade = game.idleOfflineEarningUpgrade;
			App.GetIdleData().idleTapperUpgrade = game.idleTapperUpgrade;
			#endif
		}

		public SaveGame GetCurrentData()
		{
			#if IDLEGAME
			game.idleLevels = App.GetIdleData().idleLevels;
			game.idleOfflineTimerUpgrade = App.GetIdleData().idleOfflineTimerUpgrade;
			game.idleOfflineEarningUpgrade = App.GetIdleData().idleOfflineEarningUpgrade;
			game.idleTapperUpgrade = App.GetIdleData().idleTapperUpgrade;
			#endif
			return game;
		}

		public void LoadData(SaveGame saveGame)
		{
			game = saveGame;
			#if IDLEGAME
			App.GetIdleData().idleLevels = game.idleLevels;
			App.GetIdleData().idleOfflineTimerUpgrade = game.idleOfflineTimerUpgrade;
			App.GetIdleData().idleOfflineEarningUpgrade = game.idleOfflineEarningUpgrade;
			App.GetIdleData().idleTapperUpgrade = game.idleTapperUpgrade;
			#endif
		}

		public void SetDefault()
		{
			game = new SaveGame();
			CurrentLevel = currentLevelDefault;
			CurrentLevelActual = currentLevelDefault;
			IsTutorialOver = isTutorialOverDefault;
			IsSfxOn = isSfxOnDefault;
			IsMusicOn = isMusicOnDefault;
			IsVibrationOn = isVibrationOnDefault;
			Coins = coinsDefault;
			AdsInactive = isAdsInactiveDefault;
			AreLevelsExhausted = false;
			AdLastLevel = firstAdLevel;
			//Idle Game Related
			#if IDLEGAME
			LastDateTime = "";
			unitsFoundInTapThisSecond = new IdleCurrency(0);
			Prestige = 1.0f;
			currentMultiplier = currentMultiplierDefault;
			UnitIncrement = defaultUnitPerSecond;
			Unit = defaultUnits;
			#endif
			//Idle Game Related
		}
		#endregion

		[Space(10)]
		[Header("Tutorial Related")]
		public bool isTutorialOn;

		[Space(10)]
		[Header("Level Complete Related")]
		public int levelOverCoins;
		public int multiplier;

		[Space(10)]
		[Header("Ad Related")]
		public int firstAdLevel;
		public int adAfterEachLevel;
		public bool turnOnMusic;
		public bool turnOnSfx;
		public bool isVideoRewarded;
		public bool videoFromLevelOver;
		public bool videoFromMainMenu;
		public bool alreadyRewarded;
		public int rewardAmount;
		[HideInInspector] public bool isFromDoubleEarnings;
		public int bannerRequestLevel;

		[Space(10)]
		[Header("In App Purchase Related")]
		public int coins1;
		public int coins2;
		public int coins3;
		public int coins4;
		public int coins5;
		public int coins6;
		public int removeAds;
		public bool isFromRestore;
		[HideInInspector] public int coinAdd;

		[Space(10)] 
		[Header("Tutorial Related")]
		public int maxTutorialLevels;
		
		#if IDLEGAME
		[Space(10)] [Header("Idle Game Related")]
		public IdleCurrency unitsToBeAddedNextTick;
#endif
	}

	[System.Serializable]
	public class SaveGame
	{
		public int currentLevel;
		public int currentLevelActual;
		public bool areLevelsExhausted;

		public int currentTutorialValue;
		public bool isTutorialOver;
		
		public int coins;

		public bool adsInactive;

		public bool isMusicOn;
		public bool isSfxOn;
		public bool isVibrationOn;

		public int lastAdLevel;

		#region IDLE GAME RELATED
		#if IDLEGAME
		public string lastDateTime;
		public bool isOfflineTimerOn;
		public IdleCurrency unit;
		public IdleCurrency unitIncrement;
		public IdleCurrency unitPerTap;
		public float prestige;

		//Game Specific
		[Space(10)][Header("Game Levels Related")]
		public List<IdleLevel> idleLevels;
		public IdleCommonFloatUpgrade idleOfflineTimerUpgrade;
		public IdleCommonFloatUpgrade idleOfflineEarningUpgrade;
		public IdleCommonCurrencyUpgrade idleTapperUpgrade;
		#endif
		#endregion
		
		#region LOGIC RELATED
		//Additional Logic Related
		public List<LogicData> logicData;
		public bool isLogicPopulated;
		#endregion
	}

	[System.Serializable]
	public class LogicData
	{
		public LogicType type;
		public LogicStatus fulfillmentStatus;
	}
}
