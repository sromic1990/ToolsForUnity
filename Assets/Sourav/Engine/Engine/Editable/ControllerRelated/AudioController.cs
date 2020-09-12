using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.ControllerRelated
{
	public class AudioController : Core.ControllerRelated.Controller
	{
		[SerializeField] private AudioInfo[] audios;

		private bool sfxMute;
		private bool musicMute;

		private bool wasMusicMute;
		
		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.GameLoaded:
					if (!App.GetLevelData().IsSfxOn)
					{
						sfxMute = true;
					}
					else
					{
						sfxMute = false;
					}
					
					if (!App.GetLevelData().IsMusicOn)
					{
						musicMute = true;
						wasMusicMute = true;
					}
					else
					{
						musicMute = false;
					}
					PlayPauseBGAsPerSavedData();
					break;
				
				case Notification.DataChanged:
					if (!App.GetLevelData().IsSfxOn)
					{
						sfxMute = true;
					}
					else
					{
						sfxMute = false;
					}
					
					if (!App.GetLevelData().IsMusicOn)
					{
						musicMute = true;
						wasMusicMute = true;
					}
					else
					{
						musicMute = false;
					}
					PlayPauseBGAsPerSavedData();
					break;
			}
		}

		private AudioInfo GetAudioInfoAsPerType(AudioType type)
		{
			AudioInfo audio = null;
			for (int i = 0; i < audios.Length; i++)
			{
				if (audios[i].type == type)
				{
					audio = audios[i];
				}
			}

			return audio;
		}

		private void PlayPauseBGAsPerSavedData()
		{
			if (musicMute)
			{
				GetAudioInfoAsPerType(AudioType.BackgroundMusic).source.Stop();
			}
			else
			{
				if (wasMusicMute)
				{
					GetAudioInfoAsPerType(AudioType.BackgroundMusic).source.Play();
					wasMusicMute = false;
				}
			}
		}
	}

	[System.Serializable]
	public class AudioInfo
	{
		public AudioType type;
		public AudioSource source;
	}

	public enum AudioType
	{
		None,
		BackgroundMusic,
		Button,
		Recall,
		Error,
		LevelComplete,
		Bomb,
		CoinCollected,
		GameWin
	}
}
