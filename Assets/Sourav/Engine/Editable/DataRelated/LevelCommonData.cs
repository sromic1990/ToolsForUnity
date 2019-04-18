using System.Collections.Generic;
using Break._Scripts.BallRelated;
using Break._Scripts.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Utilities.Scripts.DrawLineRelated;
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

		public bool IsIntroIntroVideoOver
		{
			get { return game.isIntroVideoOver; }
			set
			{
				game.isIntroVideoOver = value;
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

		public int TotalMoney
		{
			get { return game.totalMoney; }
			set
			{
				game.totalMoney = value;
				DataChanged();
			}
		}

		public int CurrentMoney
		{
			get { return game.currentMoney; }
			set
			{
				game.currentMoney = value;
				DataChanged();
			}
		}

		public bool IsPromptShown
		{
			get { return game.isPromptShown; }
			set
			{
				game.isPromptShown = value;
				DataChanged();
			}
		}
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
			return JsonUtility.ToJson(game);
		}

		public void SetData(string dataInString)
		{
			game = JsonUtility.FromJson<SaveGame>(dataInString);
		}

		public SaveGame GetCurrentData()
		{
			return game;
		}

		public void LoadData(SaveGame saveGame)
		{
			this.game = saveGame;
		}

		public void SetDefault()
		{
			game = new SaveGame();
			CurrentLevel = currentLevelDefault;
			CurrentLevelActual = currentLevelDefault;
			IsTutorialOver = isTutorialOverDefault;
			IsIntroIntroVideoOver = isIntroVideoOverDefault;
			IsSfxOn = isSfxOnDefault;
			IsMusicOn = isMusicOnDefault;
			IsVibrationOn = isVibrationOnDefault;
			Coins = coinsDefault;
			CurrentMoney = currentMoneyDefault;
			TotalMoney = totalMoneyDefault;
			AdsInactive = isAdsInactiveDefault;
			AreLevelsExhausted = false;
			AdLastLevel = firstAdLevel;

		}
		#endregion

		
		[Space(10)] [Header("Timer Related")] 
		public float waitTimerAfterLevelCompleteToRotateWheel;
		public float waitTimerBeforeDoorAnimation;
		[Range(0, 60)]
		public int timerValue;
		public bool isTimerStarted;
		public bool pauseTimer;
		public float waitAfterTimesUp;
		public float waitAfterEffectCollect;
		public float waitBeforeShowingTutorialHand;
		public float waitBeforeShowingPuzzle;
		public float waitTimeAtLevelStart;
		public float waitAfterTutorialComplete;
		public float waitAfterLevelComplete;
		public float waitBeforeHidingBombedTiles;
		public float playerDieDelay;

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
		[Header("Info PopUp Related")]
		public string incorrectText;
		public string correctText;
		public string tutorialCompleteText;

		[Space(10)] 
		[Header("MAJOR FIXME NEXT TIME")]
		public bool isHintPressed;
		public int hintTutorialLevel;
		public int maxTutorialLevels;
		
		[Space(10)]
		[Header("Miscellaneous")]
		public bool hasGameStarted;
		public bool isPopUpOpenDuringBonusLevel;
		public int removeTilesPerBomb;
		public bool hideTutorialHand;
		public bool collectEnable;

		[Space(10)] [Header("Gameplay related booleans")]
		public bool isFirstTap;
		
		[Space(10)] [Header("Player related")] 
		public Ball ball;
		public bool hasPlayerDied;
		public Transform ballHookPoint;
		public float playerDropForceMultiplier;
		
		[Space(10)][Header("Gameplay specific values")]
		public float distanceChangeQuantum;
		
		[Space(10)][Header("Hook related")]
		public HookData hookData;

		[Space(10)] [Header("LineDraw related")]
		public DrawLine drawLine;

		[Space(10)] [Header("OnContinue Destroy these obstacles")]
		public List<GameObject> obstaclesToDestroy;

		[Space(10)] [Header("StartPosition Related")]
		public Transform ballBaseStart;
		public Transform ballStart;
		public Transform cameraStart;
	}

	[System.Serializable]
	public class SaveGame
	{
		public int currentLevel;
		public int currentLevelActual;
		public bool areLevelsExhausted;
		
		public bool isIntroVideoOver;
		
		public int currentTutorialValue;
		public bool isTutorialOver;
		
		public int coins;

		public bool adsInactive;

		public bool isMusicOn;
		public bool isSfxOn;
		public bool isVibrationOn;

		public int lastAdLevel;

		public int totalMoney;
		public int currentMoney;

		public bool isPromptShown;
	}
}
