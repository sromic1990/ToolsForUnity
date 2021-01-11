#if IRONSOURCE
using Sourav.DebugRelated;
using Sourav.Engine.Editable.StaticScripts;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.Ads;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.Ads
{
    public class IronSourceAdProvider : GameElement, IAdProvider
    {
        #region FIELDS
        private string _appKey;
        private bool _isRVLoaded;
        private bool _isInterstitialLoaded;
        #endregion
        
        #region METHODS
        #region MONO METHODS
        void OnApplicationPause(bool isPaused) 
        {                 
            IronSource.Agent.onApplicationPause(isPaused);
        }
        #endregion
        
        #region INITIALIZE
        public void Initialize()
        {
            if (Application.platform == RuntimePlatform.Android)
            {
                _appKey = Constants.ISANDROID;
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                _appKey = Constants.ISIOS;
            }
            
            D.Log($"Current Platform = {Application.platform}, IS Ad Code = {_appKey}");

            _isRVLoaded = false;
            _isInterstitialLoaded = false;
            
            SubscribeToFSCallbacks();
            SubscribeToRVCallbacks();

            IronSource.Agent.init (_appKey, IronSourceAdUnits.REWARDED_VIDEO, IronSourceAdUnits.INTERSTITIAL);
            IronSource.Agent.validateIntegration();

            DelayLoadFS(0.5f);
        }

        private void SubscribeToFSCallbacks()
        {
            IronSourceEvents.onInterstitialAdReadyEvent += InterstitialAdReadyEvent;
            IronSourceEvents.onInterstitialAdLoadFailedEvent += InterstitialAdLoadFailedEvent;        
            IronSourceEvents.onInterstitialAdShowSucceededEvent += InterstitialAdShowSucceededEvent; 
            IronSourceEvents.onInterstitialAdShowFailedEvent += InterstitialAdShowFailedEvent; 
            IronSourceEvents.onInterstitialAdClickedEvent += InterstitialAdClickedEvent;
            IronSourceEvents.onInterstitialAdOpenedEvent += InterstitialAdOpenedEvent;
            IronSourceEvents.onInterstitialAdClosedEvent += InterstitialAdClosedEvent;
        }

        private void SubscribeToRVCallbacks()
        {
            IronSourceEvents.onRewardedVideoAdOpenedEvent += RewardedVideoAdOpenedEvent;
            IronSourceEvents.onRewardedVideoAdClickedEvent += RewardedVideoAdClickedEvent;
            IronSourceEvents.onRewardedVideoAdClosedEvent += RewardedVideoAdClosedEvent; 
            IronSourceEvents.onRewardedVideoAvailabilityChangedEvent += RewardedVideoAvailabilityChangedEvent;
            IronSourceEvents.onRewardedVideoAdStartedEvent += RewardedVideoAdStartedEvent;
            IronSourceEvents.onRewardedVideoAdEndedEvent += RewardedVideoAdEndedEvent;
            IronSourceEvents.onRewardedVideoAdRewardedEvent += RewardedVideoAdRewardedEvent; 
            IronSourceEvents.onRewardedVideoAdShowFailedEvent += RewardedVideoAdShowFailedEvent;
        }
        #endregion

        #region BANNER
        public void ShowBanner()
        {
        }

        public void HideBanner()
        {
        }
        #endregion

        #region FS
        public void ShowFS()
        {
            D.Log("Trying to Show FS");
            CancelInvoke("ShowActualFS");
            #if !UNITY_EDITOR
            if (!App.GetData<LevelCommonData>().AdsInactive)
            {
                D.Log("Showing FS");
                if (_isInterstitialLoaded)
                {
                    D.Log("Starting to show FS");
                    App.Notify(Notification.ShowingInterstitialAd);
                    App.Notify(Notification.ShowAdComplianceScreen);
                    AudioListener.volume = 0;
                    Invoke("ShowActualFS", 1);
                }
                else
                {
                    D.Log("FS not available");
                    DelayLoadFS();
                }
            }
            else
            {
                D.Log("NoAds Purchased, not showing FS");
            }
            #endif
        }

        private void ShowActualFS()
        {
            AudioListener.volume = 0;
            IronSource.Agent.showInterstitial();
        }
        
        private void DelayLoadFS(float delaySeconds = 1.0f)
        {
            CancelInvoke("RequestInterstitial");
            if (!App.GetData<LevelCommonData>().AdsInactive)
            {
                D.Log($"Delay load FS with {delaySeconds} sec");
                if (!_isInterstitialLoaded)
                {
                    Invoke("RequestInterstitial", delaySeconds);
                }
            }
            else
            {
                D.Log("No Ads Purchased. Not loading FS");
            }
        }
        
        private void ResetFSLoad()
        {
            _isInterstitialLoaded = false;
            DelayLoadFS();
        }

        private void RequestInterstitial()
        {
            D.Log("Requesting FS");
            IronSource.Agent.loadInterstitial();
        }
        
        private void InterstitialAdReadyEvent()
        {
            D.Log("FS Ready");
            _isInterstitialLoaded = true;
        }
        private void InterstitialAdLoadFailedEvent(IronSourceError error)
        {
            D.Log($"FS Load failed with code = {error.getCode()}, error code = {error.getErrorCode()}, description = {error.getDescription()}");
            ResetFSLoad();
        }

        private void InterstitialAdShowSucceededEvent()
        {
            D.Log("FS Successfully shown");
        }
        private void InterstitialAdShowFailedEvent(IronSourceError error)
        {
            D.Log($"FS Show failed with code = {error.getCode()}, error code = {error.getErrorCode()}, description = {error.getDescription()}");
            ResetFSLoad();
        }
        private void InterstitialAdClickedEvent()
        {
            D.Log("FS Clicked");
        }
        private void InterstitialAdOpenedEvent()
        {
            D.Log("FS Opened");
        }
        private void InterstitialAdClosedEvent()
        {
            D.Log("FS Closed");
            App.Notify(Notification.InterstitialClosed);
            App.Notify(Notification.HideAdComplianceScreen);
            AudioListener.volume = 1;
            ResetFSLoad();
        }

        #endregion
        
        #region RV
        public void ShowRV()
        {
            #if UNITY_EDITOR
            App.Notify(Notification.AdRewarded);
            App.Notify(Notification.RewardAdClosed);
            #else
            D.Log("RV Ad To Be Shown");
            if (_isRVLoaded)
            {
                D.Log("RV Ad Showing");
                AudioListener.volume = 0;
                App.Notify(Notification.ShowingRewardAd);
                IronSource.Agent.showRewardedVideo();
            }
            else
            {
                D.Log("RV Ad NotAvailable");
                App.Notify(Notification.NoAdsAvailable);
            }
            #endif
        }

        private void RewardedVideoAdOpenedEvent()
        {
            D.Log("RV Ad Opened");
        }
        private void RewardedVideoAdClickedEvent(IronSourcePlacement obj)
        {
            D.Log("RV Ad Clicked");
        }
        private void RewardedVideoAdClosedEvent()
        {
            D.Log("RV Ad Closed");
            AudioListener.volume = 1;
            App.Notify(Notification.RewardAdClosed);
        }
        private void RewardedVideoAvailabilityChangedEvent(bool available)
        {
            D.Log($"RV Availability Changed {available}");
            _isRVLoaded = available;
        }
        private void RewardedVideoAdStartedEvent()
        {
            D.Log("RV started");
        }
        private void RewardedVideoAdEndedEvent()
        {
            D.Log("RV ended");
        }
        private void RewardedVideoAdRewardedEvent(IronSourcePlacement placement)
        {
            D.Log("RV Rewarded");
            App.Notify(Notification.AdRewarded);
        }
        private void RewardedVideoAdShowFailedEvent(IronSourceError error)
        {
            D.Log($"RV Show failed with code = {error.getCode()}, error code = {error.getErrorCode()}, description = {error.getDescription()}");
        }
        #endregion

        #region DEBUGGER
        public void ShowDebugger()
        {
        }
        #endregion
        #endregion
    }
}
#endif