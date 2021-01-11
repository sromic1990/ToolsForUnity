namespace Sourav.Engine.Editable.NotificationRelated
{
    //Fill this up with game Notifications  
    
    public enum Notification
    {
        None,
        TestNotification,
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
        
        //Timer Related
        SecondTick,
        
        //Update Related
        Update,
        LateUpdate,
        FixedUpdate,

        //Idle Button Related
        IdleButtonPressed,
        UpdateIdleButton,

        //Haptic Related
        HapticSuccess,
        HapticFailure,
        HapticHeavy,
        HapticMild,
        HapticAndroid,
        HapticSelectionChange,
        HapticImpactLight,
        HapticImpactMedium,
        HapticImpactHeavy,
        HapticWarning,
        HapticSoft,
        HapticRigid,

        //UI Related
        ButtonPressed,
        HomeButtonPressed,
        ButtonPressedInternally,
        
        //Toggle Related
        ToggleOn,
        ToggleOff,

        //Purchase Related
        SetUpIAPStore,
        IAPPricesSet,
        Diamonds1,
        Diamonds2,
        Diamonds3,
        Diamonds4,
        Diamonds5,
        DiamondsCombo,
        RemoveAds,
        RestorePurchase,
        PurchaseConsumable,
        PurchaseNonConsumable,
        PurchaseFailed,
        ShowPurchaseSuccessful,
        RestorePurchaseSuccessful,
        RestorePurchaseFailed,
        ShowLoading,
        HideLoading,
        NoAdsPurchased,
        
        //Ad Related
        AdRewarded,
        RewardAdClosed,
        ShowBanner,
        HideBanner,
        ShowInterstitial,
        ShowRewardVideo,
        ShowDebugger,
        VideoCoinsRewarded,
        ShowingRewardAd,
        NoRVAvailable,
        ShowingInterstitialAd,
        ShowAdComplianceScreen,
        InterstitialClosed,
        HideAdComplianceScreen,
        NoAdsAvailable,

        //UI Screen Transition related
        ShowScreen,
        TransitionComplete,
        HidePopUpScreen,
        
        //Gameplay Related
        PlayGame,
        ContinueGame,
        Get2xCoins,
        SetUi,
        UiSet,
        SetUpIdleButtons,
        LevelComplete,
        
        StartGame,


        //Tutorial

        //View Related
        ButtonsUpdated,
        GenerateIdleButtons,
        IdleButtonsGenerated,
        StartBlockIng,
        IdleButtonFillStarted,
        IdleButtonFillComplete,
        TapperPressed,
        
        //Cheat Related
        CheatUnits,
        
        //Offline Time Related
        ShowOfflineTime,
        AddOfflineEarning,
        OfflineBoost1,
        OfflineBoost2,
        OfflineDiamondAttempt,

        //Game Related
        RestartGame,
        StartUnlocking,
        UnlockedNewLevel,

        FollowMan,
        TapperButtonClicked,
        RevealHistoryElements,
        CoinTapperTapped,
        UpdateUI,
        ShowBook,
        FlyingObjectsReached,
        CoinsCollected,
        ElementDropped,
        CoinsAdded,

        ShowStore,
        ShowTimeTravelStore,
        
        //Units and Gems Related
        UnitsUpdated,
        DiamondsUpdated,
        DiamondsAddedPopUp,
        DiamondsComboPopUp,
        DiamondsAdded,
        NotEnoughDiamonds,
        FreeDiamondsAdded,
        
        //Flying RV Related
        FlyingRVDataSet,
        ShowFRV,
        FlyingRVClicked,
        FRVPurchaseAttempt,
        RestartFlyingRVTimer,
        FlyingRVRewarded,
        
        //UpgradeRelated
        OfflineCoinsUpgraded,
        TapperCoinsUpgraded,
        UpgradeComplete,

        //Time Travel Related
        SetUpTimeTravel,
        ShowFreeTimeTravel,
        StartTimeTravel,
        StartShowingTimeTravel,
        StopShowingTimeTravel,
        CloseTimeTravelGainsPopUp,
        TimeTravelPurchaseAttempt,
        TimeTravelSecondsUpdated,
        
        //RC Related
        RCFetched,

        //Remote Config Related
        SetUpRemoteConfig,
        RemoteConfigSet,
        ShowServerFetch,
        HideServerFetch,

        //Cheat Related
        CheatActivated,
        
        //PopUp Related
        ShowPopup,
        HidePopUp,
        HideAllPopUps,
        PopUpHidden,
        PopUpButtonPressed,

        //ANALYTICS
        RewardVideoSeenAnalytics = 2000,
        HistoryUnlockedAnalytics = 2001,
        
        //TUTORIALS
        TutorialPressed = 5000,
        TutorialComplete = 5001,
        ShowTutorial = 5002,
        ShowNextTutorial = 5003,
        HistoryTutorialFinished = 5004,
        CoinTutorialFinished = 5005,
        PinchZoomTutorialFinished = 5006,
    }
}



