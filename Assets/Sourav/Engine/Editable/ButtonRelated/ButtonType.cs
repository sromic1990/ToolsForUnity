using UnityEngine;

namespace Sourav.Engine.Editable.ButtonRelated
{
    //FIXME Fill this up with button types
    public enum ButtonType : int
    {
        Play = 0,
        Pause = 1,
        Settings = 2,
        SoundButtonToggle = 3,
        SFXButtonToggle = 4,
        RestorePurchase = 5,
        
        //Main Menu Video Related
        TapToContinue = 6,
        Safe = 7,
        Erase = 8,
        Bomb = 9,
        Hint = 10,
        CloseSafe = 11,
        Collect = 12,
        CollectVideo = 13,
        PopUpClose = 14,
        Store = 15,
        Home = 16,
        
        Coin1,
        Coin2,
        Coin3,
        Coin4,
        Coin5,
        Coin6,
        RemoveAds,
        WatchVideoAd,
    }
}
