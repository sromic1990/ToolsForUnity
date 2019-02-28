using System;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.DataRelated
{
	//Serialize this class and store in the disk for savegames
	public class LevelData : Data
	{
		[SerializeField] private SaveGame game;

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

		public bool IsSFXOn
		{
			get { return game.isSfxOn; }
			set
			{
				game.isSfxOn = value;
				DataChanged();
			}
		}

		public bool isAdOn
		{
			get { return game.isAdOn; }
			set
			{
				game.isAdOn = value;
				DataChanged();
			}
		}

		public DateTime dateOfNextReward
		{
			get { return game.dateOfNextReward; }
			set
			{
				game.dateOfNextReward = value;
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
		public bool isDataChanged;
		
		public int currentTutorial;
		
		public int maxTutorialValue;

		[Range(0, 60)]
		public int timerValue;
		
		public bool isTutorialOn;
		public bool isStageClear;
		
		public bool isTimerStarted;
		public bool pauseTimer;
		public float waitAfterTimesUp;

		public int levelOverCoins;
		public int perBonusWordCoins;

		public bool videoFromLevelOver;
		public bool videoFromMainMenu;
		public int coinsFromThisLevel;

		public int firstAdLevel;
		public int adAfterEachLevel;
		

		public int coins1;
		public int coins2;
		public int coins3;
		public int coins4;
		public int coins5;
		public int coins6;

		public int coinAdd;

		public bool hasGameStarted;

		public bool isPopUpOpenDuringBonusLevel;

		public bool alreadyRewarded;

		public int rewardPerVideo;
		public int dailyReward;

		public int hours, minutes, seconds;

		public bool turnOnMusic;
		public bool turnOnSFX;
		
		public bool isFromRestore;
		public bool isVideoRewarded;

		private void DataChanged()
		{
			isDataChanged = true;
			App.GetNotificationCenter().Notify(Notification.DataChanged);
			#if UNITY_EDITOR
			App.GetNotificationCenter().Notify(Notification.SaveData);
			#endif
		}

		public SaveGame GetCurrentData()
		{
			return game;
		}

		public void LoadData(SaveGame game)
		{
			this.game = game;
		}

		public void SetDefault()
		{
			DateTime startTime = DateTime.UtcNow;
			dateOfNextReward = startTime.AddDays(1);
			
			TimeSpan timeRemaining = dateOfNextReward-startTime;

			hours   = (int) timeRemaining.TotalHours; // truncate partial hours
			minutes = timeRemaining.Minutes;
			seconds = timeRemaining.Seconds;
		}
	}

	[System.Serializable]
	public class SaveGame
	{
		public bool isTutorialOver;

		public int coins;

		public bool adsInactive;

		public bool isMusicOn;
		public bool isSfxOn;

		public bool isAdOn;

		public DateTime dateOfNextReward;

		public int lastAdLevel;
	}
}
