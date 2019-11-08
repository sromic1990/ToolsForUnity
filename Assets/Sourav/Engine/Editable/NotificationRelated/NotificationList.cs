namespace Sourav.Engine.Editable.NotificationRelated
{
    //Fill this up with game Notifications  
    
    public enum Notification
    {
        None,
        //Pause Related
        PauseGame,
        ResumeGame,
        GamePaused,
        GameResumed,
        PauseResumeToggle,
        
        //Camera Related
        StartCameraScript,
        StartZoomingCamera,
        StopZoomingCamera,
        StopCameraScript,
        
        //Data Related
        RecordPositionData,
        FetchPositionData,
        PositionData,
        
        //Save Load Related
        LoadGame,
        SaveGame,
        GameLoaded,
        DataChanged,

        //Haptic Related
        HapticSuccess,
        HapticFailure,
        HapticHeavy,
        HapticMild,
        HapticAndroid,

        //UI Related
        ButtonPressed,
        HomeButtonPressed,
        
        //Toggle Related
        ToggleOn,
        ToggleOff,
        
        //PopUp Related
        ClosePopUp,
        OpenPopUp,
        
        //Purchase Related
        Coins1,
        Coins2,
        Coins3,
        Coins4,
        Coins5,
        Coins6,
        RemoveAds,
        RestorePurchase,
        PurchaseSuccessful,
        ShowPurchaseSuccessful,
        RestorePurchaseSuccessful,
        ShowLoading,
        HideLoading,
        
        //Ad Related
        AdRewarded,
        RewardAdClosed,
        ShowBanner,
        HideBanner,
        ShowInterstitial,
        ShowRewardVideo,
        VideoCoinsRewarded,

        //UI Screen Transition related
        ShowScreen,
        TransitionComplete,
        HidePopUpScreen,
        
        //Gameplay Related
        PlayGame,
        LevelComplete,
    }
}



