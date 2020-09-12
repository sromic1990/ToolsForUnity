﻿#if ADMOB
using System;
using System.Collections.Generic;
using GoogleMobileAds.Api;
using MEC;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;
using UnityEngine.UI;
using Reward = GoogleMobileAds.Api.Reward;

namespace Sourav.Engine.Editable.ControllerRelated
{
	public class AdMobController : Controller
	{
		[SerializeField] private AdPackage[] ids;

		private string appId;
		
		private BannerView bannerView;
		private InterstitialAd interstitial;
		private RewardBasedVideoAd rewardBasedVideo;

		[SerializeField] private Button[] rewardButton;

		[SerializeField] private List<TypeOfID> typeOfAds;

		private bool isInitialized;
		
		private void Initialize ()
		{	
			DisableAllVideoButtons();
			
#if UNITY_ANDROID
			appId = GetID(TypeOfPlatform.Android, TypeOfID.App);
#elif UNITY_IPHONE
            appId = GetID(TypeOfPlatform.iOS, TypeOfID.App);
#else
            appId = "unexpected_platform";
#endif
			// Initialize the Google Mobile Ads SDK.
			MobileAds.Initialize(appId);
			
			//Banner
//			RequestBannerAd();

			RequestInterstitialAd();

			RequestRewardAd();
			
			isInitialized = true;
		}

		private void RequestRewardAd()
		{
			if (typeOfAds.Contains(TypeOfID.Reward))
			{
				this.rewardBasedVideo = RewardBasedVideoAd.Instance;

				rewardBasedVideo.OnAdLoaded += HandleRewardBasedVideoLoaded;
				// Called when an ad request failed to load.
//				rewardBasedVideo.OnAdFailedToLoad += HandleRewardBasedVideoFailedToLoad;
				// Called when an ad is shown.
				rewardBasedVideo.OnAdOpening += HandleRewardBasedVideoOpened;
				// Called when the ad starts to play.
				rewardBasedVideo.OnAdStarted += HandleRewardBasedVideoStarted;
				// Called when the user should be rewarded for watching a video.
				rewardBasedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
				// Called when the ad is closed.
				rewardBasedVideo.OnAdClosed += HandleRewardBasedVideoClosed;
				// Called when the ad click caused the user to leave the application.
				rewardBasedVideo.OnAdLeavingApplication += HandleRewardBasedVideoLeftApplication;

				this.RequestRewardBasedVideo();
			}
		}

		private void RequestInterstitialAd()
		{
			if (typeOfAds.Contains(TypeOfID.Interstitial))
			{
				if (!App.GetLevelData().AdsInactive)
				{
					this.RequestInterstitial();
				}
			}
		}

		private void RequestBannerAd()
		{
			if (typeOfAds.Contains(TypeOfID.Banner))
			{
				if (!App.GetLevelData().AdsInactive)
				{
					this.RequestBanner();
				}
			}
		}

		#region BANNER
		private void RequestBanner()
		{
			if(App.GetLevelData().AdsInactive)
				return;
			
#if UNITY_ANDROID
			string adUnitId = GetID(TypeOfPlatform.Android, TypeOfID.Banner);
#elif UNITY_IPHONE
            string adUnitId = GetID(TypeOfPlatform.iOS, TypeOfID.Banner);
#else
            string adUnitId = "unexpected_platform";
#endif

			// Create a 320x50 banner at the top of the screen.
			bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);
			
			// Called when an ad request has successfully loaded.
			bannerView.OnAdLoaded += HandleOnAdLoaded;
			// Called when an ad request failed to load.
			bannerView.OnAdFailedToLoad += HandleOnAdFailedToLoad;
			// Called when an ad is clicked.
			bannerView.OnAdOpening += HandleOnAdOpened;
			// Called when the user returned from the app after an ad click.
			bannerView.OnAdClosed += HandleOnAdClosed;
			// Called when the ad click caused the user to leave the application.
			bannerView.OnAdLeavingApplication += HandleOnAdLeavingApplication;
			
			AdRequest request = new AdRequest.Builder().Build();

			// Load the banner with the request.
			bannerView.LoadAd(request);
		}
		private void HandleOnAdLeavingApplication(object sender, EventArgs e)
		{
			Debug.Log("Banner : Leaving Application");
		}

		private void HandleOnAdClosed(object sender, EventArgs e)
		{
			Debug.Log("Banner : AdClosed");
		}

		private void HandleOnAdOpened(object sender, EventArgs e)
		{
			Debug.Log("Banner : AdOpened");
		}

		private void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
		{
			Debug.Log("Banner : AdFailedToLoad");
		}

		private void HandleOnAdLoaded(object sender, EventArgs e)
		{
			Debug.Log("Banner : AdLoaded");
		}
		
		private void RemoveBannerView()
		{
			bannerView.Destroy();
		}
		#endregion

		#region INTERSTITIAL
		private void RequestInterstitial()
		{
#if UNITY_ANDROID
			string adUnitId = GetID(TypeOfPlatform.Android, TypeOfID.Interstitial);
#elif UNITY_IPHONE
        string adUnitId = GetID(TypeOfPlatform.iOS, TypeOfID.Interstitial);
#else
        string adUnitId = "unexpected_platform";
#endif

			// Initialize an InterstitialAd.
			this.interstitial = new InterstitialAd(adUnitId);
			
			// Called when an ad request has successfully loaded.
			this.interstitial.OnAdLoaded += HandleInterstitialOnAdLoaded;
			// Called when an ad request failed to load.
//			this.interstitial.OnAdFailedToLoad += HandleInterstitialOnAdFailedToLoad;
			// Called when an ad is shown.
			this.interstitial.OnAdOpening += HandleInterstitialOnAdOpened;
			// Called when the ad is closed.
			this.interstitial.OnAdClosed += HandleInterstitialOnAdClosed;
			// Called when the ad click caused the user to leave the application.
			this.interstitial.OnAdLeavingApplication += HandleInterstitialOnAdLeavingApplication;
			
			// Create an empty ad request.
			AdRequest request = new AdRequest.Builder().Build();
			// Load the interstitial with the request.
			this.interstitial.LoadAd(request);
		}

		private void HandleInterstitialOnAdLeavingApplication(object sender, EventArgs e)
		{
			Debug.Log("Interstital : OnAdLeavingApplicaton");
		}

		private void HandleInterstitialOnAdClosed(object sender, EventArgs e)
		{
			Timing.RunCoroutine(RestartSounds());
			Debug.Log("Interstital : OnAdClosed");
			this.RequestInterstitial();
		}

		private void HandleInterstitialOnAdOpened(object sender, EventArgs e)
		{
			Debug.Log("Interstital : OnAdOpened");
		}

		private void HandleInterstitialOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs e)
		{
			Debug.Log("Interstital : OnAdFailedToLoad");
		}

		private void HandleInterstitialOnAdLoaded(object sender, EventArgs e)
		{
			Debug.Log("Interstital : OnAdLoaded");
		}

		private void ShowInterstitial()
		{
			#if UNITY_EDITOR
			D.Log("Interstitial Showing");
			#else
			if (this.interstitial.IsLoaded()) 
			{
				if (!App.GetLevelData().AdsInactive)
				{
					if (App.GetLevelData().IsMusicOn)
					{
						App.GetLevelData().turnOnMusic = true;
						App.GetLevelData().IsMusicOn = false;
					}
				
					if (App.GetLevelData().IsSfxOn)
					{
						App.GetLevelData().turnOnSfx = true;
						App.GetLevelData().IsSfxOn = false;
					}
					
					this.interstitial.Show();
				}
			}
			#endif
		}
		#endregion
		
		#region REWARDAD

		private void RequestRewardBasedVideo()
		{
#if UNITY_ANDROID
			string adUnitId = GetID(TypeOfPlatform.Android, TypeOfID.Reward);
#elif UNITY_IPHONE
            string adUnitId = GetID(TypeOfPlatform.iOS, TypeOfID.Reward);
#else
            string adUnitId = "unexpected_platform";
#endif

			// Create an empty ad request.
			AdRequest request = new AdRequest.Builder().Build();
			// Load the rewarded video ad with the request.
			this.rewardBasedVideo.LoadAd(request, adUnitId);
		}
		
		private void HandleRewardBasedVideoLeftApplication(object sender, EventArgs e)
		{
			Debug.Log("Reward : VideoLeftApplication");
		}

		private void HandleRewardBasedVideoClosed(object sender, EventArgs e)
		{
			Timing.RunCoroutine(RestartSounds());
			App.GetNotificationCenter().Notify(Notification.RewardAdClosed);
			this.RequestRewardBasedVideo();
		}

		private IEnumerator<float> RestartSounds()
		{
			yield return Timing.WaitForSeconds(0.5f);
			if (App.GetLevelData().turnOnMusic)
			{
				App.GetLevelData().turnOnMusic = false;
				App.GetLevelData().IsMusicOn = true;
			}
			
			if (App.GetLevelData().turnOnSfx)
			{
				App.GetLevelData().turnOnSfx = false;
				App.GetLevelData().IsSfxOn = true;
			}
		}

		private void HandleRewardBasedVideoRewarded(object sender, Reward e)
		{
			Debug.Log("Reward : VideoRewarded");
			App.GetNotificationCenter().Notify(Notification.AdRewarded);
		}

		private void HandleRewardBasedVideoStarted(object sender, EventArgs e)
		{
			Debug.Log("Reward : VideoStarted");
		}

		private void HandleRewardBasedVideoOpened(object sender, EventArgs e)
		{
			Debug.Log("Reward : VideoOpened");
		}

		private void HandleRewardBasedVideoFailedToLoad(object sender, AdFailedToLoadEventArgs e)
		{
			Debug.Log("----------------------------------------------------------------->Reward : VideoFailedToLoad");
			this.RequestRewardBasedVideo();
		}

		private void HandleRewardBasedVideoLoaded(object sender, EventArgs e)
		{
			Debug.Log("<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<<>Reward : VideoLoaded");
			if (rewardButton != null)
			{
				for (int i = 0; i < rewardButton.Length; i++)
				{
					if (rewardButton[i] != null)
					{
						rewardButton[i].interactable = true;
					}
				}
			}
		}
		
		private void ShowRewardAd()
		{
			#if UNITY_EDITOR
			App.GetNotificationCenter().Notify(Notification.AdRewarded);
			App.GetNotificationCenter().Notify(Notification.RewardAdClosed);
			
			#else
			if (rewardBasedVideo.IsLoaded()) 
			{
				App.Notify(Notification.ShowingRewardAd);
				if (App.GetLevelData().IsMusicOn)
				{
					App.GetLevelData().turnOnMusic = true;
					App.GetLevelData().IsMusicOn = false;
				}
				
				if (App.GetLevelData().IsSfxOn)
				{
					App.GetLevelData().turnOnSfx = true;
					App.GetLevelData().IsSfxOn = false;
				}
				
				rewardBasedVideo.Show();
				DisableAllVideoButtons();
			}
			#endif
		}

		private void DisableAllVideoButtons()
		{
			if (rewardButton != null)
			{
				for (int i = 0; i < rewardButton.Length; i++)
				{
					if (rewardButton[i] != null)
					{
						rewardButton[i].interactable = false;
					}
				}
			}
		}

		#endregion
		
		private string GetID(TypeOfPlatform platform, TypeOfID idType)
		{
			string id = "";

			for (int i = 0; i < ids.Length; i++)
			{
				if (ids[i].platform == platform)
				{
					if (ids[i].idType == idType)
					{
						id = ids[i].id;
						break;
					}
				}
			}
			
			return id;
		}

		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.GameLoaded:
					if (!App.GetLevelData().AdsInactive)
					{
						if (!isInitialized)
						{
							Initialize();
						}
					}
					break;
				
				case Notification.DataChanged:
					if (!App.GetLevelData().AdsInactive)
					{
						if (!isInitialized)
						{
							Initialize();
						}
					}
					break;
				
				case Notification.RemoveAds:
					if (typeOfAds.Contains(TypeOfID.Banner))
					{
						RemoveBannerView();
					}
					break;
				
				case Notification.ShowInterstitial:
					ShowInterstitial();
					break;
				
				case Notification.ShowRewardVideo:
					ShowRewardAd();
					break;
				
				case Notification.ShowBanner:
					RequestBannerAd();
					break;
				
				case Notification.HideBanner:
					RemoveBannerView();
					break;
			}
		}
	}

	[System.Serializable]
	public class AdPackage
	{
		public TypeOfPlatform platform;
		public TypeOfID idType;
		public string id;
	}

	public enum TypeOfPlatform
	{
		None,
		Android,
		iOS
	}

	public enum TypeOfID
	{
		None,
		App,
		Banner,
		Interstitial,
		Reward
	}
}
#endif
