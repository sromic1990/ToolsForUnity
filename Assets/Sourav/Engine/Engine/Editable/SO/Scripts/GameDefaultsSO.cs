using UnityEngine;

namespace Sourav.Engine.SO
{
    [CreateAssetMenu()]
    public class GameDefaultsSO : ScriptableObject
    {
        public int currentLevelDefault;
        public bool isSfxOnDefault;
        public bool isMusicOnDefault;
        public bool isVibrationOnDefault;
        public bool isTutorialOverDefault;
        public bool isAdsInactiveDefault;
    }
}
