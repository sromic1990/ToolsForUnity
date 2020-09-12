using Sourav.Engine.Editable.DataRelated;
using UnityEngine;

namespace Sourav.EngineDataSnippets
{
    public class AdCommonData : CommonData
    {
        [Header("Ad Related")]
        public int firstAdLevel;
        public int adAfterEachLevel;
        public bool turnOnMusic;
        public bool turnOnSfx;
        public bool isVideoRewarded;
        public int bannerRequestLevel;
    }
}
