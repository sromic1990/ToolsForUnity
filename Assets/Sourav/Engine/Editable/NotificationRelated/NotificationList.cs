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
        
        //Transition Related
        TransitionBegan,
        TransitionMidWay,
        TransitionEnded,
        
        //Save Load Related
        LoadGame,
        SaveGame,
        GameLoaded,
        DataChanged,
        
        //Video Related
        StartVideo,
        VideoPrepared,
        PausedAtVideoFrame,
        ResumeVideo,
        VideoFinished,
        VideoTapped,
        
        //Safe related
        NormalSafeClicked,
        NormalSafeClosed,
        
        //Gameplay related
        PlayGame,
        AnswerButtonPressed,
        LevelDataSet,
        AnswerPlaceClicked,
        EraseButtonPressed,
        GameplaySafeHidden,
        ShowTutorialOver,
        ShowCorrectAnswer,
        ShowWrongAnswer,
        LevelComplete,
        CollectReward,
        ShowRewardCollected,
        RewardReachedBar,
        RewardCollected,
        
        //Tutorial Related
        SafeShowingStatusChanged,
        RequestStashLetterPosition,
        LetterPositionStashed,
        RequestStashHintPosition,
        RequestStashOfCollectButton,
        
        //Power-Up Related
        HintUsedTutorial,
        HintUsed,
        CheckHintButton,
        BombUsed,
        BombedButtons,
        CheckBombButton,
        
        //UI Related
        ButtonPressed,
        CoinAdded,
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
    }
}



