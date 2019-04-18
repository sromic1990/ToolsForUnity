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
        
        //Gameplay related
        PlayGame,
        StartLevel,
        LevelReadyToStart,
        LevelComplete,
        VanishBase,
        
        //Player Related
        PlayerDead,
        PlayerHitDeadObstacle,
        
        //Hook Related
        HookMovedOutOfCamera,
        InitializeHookController,
        SetNextHook,
        AttachedHook,
        DetachedHook,
        
        
        //Haptic Related
        HapticSuccess,
        HapticFailure,
        
        //Audio Related
        DeathSound,
        SwingSound,
        
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
        
        //Input related
        TapPress,
        TapRelease,
        
        //UI Screen Transition related
        ShowScreen,
        TransitionComplete,
    }
}



