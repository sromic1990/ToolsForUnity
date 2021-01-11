using UnityEngine;

namespace Sourav.Engine.Editable.ButtonRelated
{
    //FIXME Fill this up with button types
    public enum ButtonType : int
    {
        //Gameplay Buttons
        None = 0,
        Play = 1,
        Pause = 2,
        Hint = 3,
        Continue = 4,
        GetOfflineCoinDiamond = 5,
        Get2xCoins = 6,
        TapperButton = 7,
        CoinTapperButton = 8,
        ShowBook = 9,
        GoToCurrentPointOfInterest = 10,
        NoThanksVideoOfflineEarning = 11,
        NoThanksVideoRV = 12,
        OfflineCoinUpgrade = 13,
        CoinTapperUpgrade = 14,
        FRV = 15,
        FRVVideo = 16,
        FRVDiamond = 17,
        DiamondsAdded = 18,

        //PopUp Related
        Settings = 50,
        Store = 51,
        Upgrades = 52,
        TimeTravel = 53,
        TimeTravelStore = 54,

        //In Apps Related
        RestorePurchase = 100,
        RemoveAds = 101,
        Diamonds1 = 102,
        Diamonds2 = 103,
        Diamonds3 = 104,
        Diamonds4 = 105,
        Diamonds5 = 106,
        DiamondsCombo = 107,
        DiamondsFree = 108,
        
        //Ads Related
        WatchVideoAd = 150,
        ShowFSAd = 151,
        ShowAdsOffline = 152,
        
        //Time Travel Related
        TimeTravelFree = 200,
        TimeTravelVideo = 201,
        TimeTravel1 = 202,
        TimeTravel2 = 203,
        TimeTravel3 = 204,
        TimeTravel4 = 205,
        TimeTravelDeal = 206,

    }
}
