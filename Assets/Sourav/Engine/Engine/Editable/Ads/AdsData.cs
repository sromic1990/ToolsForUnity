using Sirenix.OdinInspector;
using Sourav.Engine.Editable.DataRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.Ads
{
    public class AdsData : CommonData    
    {
        [ReadOnly] public bool isAdRewarded;
        [ReadOnly] public RVType rvType;
        [ReadOnly] public string interstitialPlacement;

        [Space(10)] [Header("InterstitialClickCount")]
        [ReadOnly] public bool isFSOn;
        [ReadOnly] public int clicksPerAd;
        [ReadOnly] public int currentClicks;
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