using Sourav.Engine.Editable.DataRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.Ads
{
    public class AdsData : CommonData    
    {
        public bool isAdRewarded;
        public RVType rvType;
        public string interstitialPlacement;

        [Space(10)] [Header("InterstitialClickCount")]
        public bool isFSOn;
        public int clicksPerAd;
        /*[ReadOnly]*/ public int currentClicks;
    }
    
    public enum RVType
    {
        Continue,
        Get2x,
        TimeTravel,
        Diamonds,
        FlyingRV,
    }
}