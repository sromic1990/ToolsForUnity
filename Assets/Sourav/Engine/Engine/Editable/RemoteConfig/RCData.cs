// using Sirenix.OdinInspector;
using Sourav.Engine.Editable.DataRelated;

namespace Sourav.Engine.Editable.RemoteConfig
{
    public class RCData : CommonData
    {
        public RCValueDefault[] rcDefaultValues;

        public bool IsRCFetched(RCType type)
        {
            for (int i = 0; i < rcDefaultValues.Length; i++)
            {
                if (rcDefaultValues[i].type == type)
                {
                    return rcDefaultValues[i].isFetched;
                }
            }

            return false;
        }
    }
    
    public enum RCType
    {
        None,
        CameraRC,
        FactsRC,
        CoinProgressionRC,
        ClicksProgressionRC,
        CoinUpgradeRC,
        OfflineUpgradeRC,
        CheatRC,
        AdsRC,
        IapRC,
        TimeTravelRC,
        RVRC,
    }

    [System.Serializable]
    public class RCValueDefault
    {
        // [FoldoutGroup("$nameOfField")]
        public RCType type;
        // [FoldoutGroup("$nameOfField")]
        public string nameOfField;
        // [FoldoutGroup("$nameOfField")]
        public TypeOfValue valueType;
        // [FoldoutGroup("$nameOfField")]
        // [ShowIf("valueType", TypeOfValue.Integer)]
        public int intValue;
        // [FoldoutGroup("$nameOfField")]
        // [ShowIf("valueType", TypeOfValue.Float)]
        public float floatValue;
        // [FoldoutGroup("$nameOfField")]
        // [ShowIf("valueType", TypeOfValue.String)]
        public string stringValue;
        // [FoldoutGroup("$nameOfField")]
        // [ShowIf("valueType", TypeOfValue.Bool)]
        public bool boolValue;
        // [FoldoutGroup("$nameOfField")]
        public RCHandlerBase rcHandler;
        // [FoldoutGroup("$nameOfField")] 
        /*[ReadOnly]*/ public bool isFetched;
    }

    public enum TypeOfValue
    {
        None,
        Integer,
        Float,
        String,
        Bool,
    }
}