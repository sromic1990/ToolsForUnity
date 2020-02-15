namespace Sourav.IdleGameEngine.IdleGameData
{
    public enum IdleUnitType
    {
        None,
        ButtonType1,
        //Populate Idle Buttons type here
        InfoType1,
        InfoType2,
        InfoType3,
    }
    
    public enum IdlePurchasableItemType
    {
        OneTime,
        RecurringFixedUnits,
        RecurringWithLimit,
        RecurringForever,
        //Populate more Update Type here
    }

    public enum IdleUpgradeFunction
    {
        None,
        OfflineUnit,
        OfflineTimer,
        TapperUpgrade,
        UnitBoost,
    }

    public enum IdleLevelType
    {
        None,
        Level1,
        //Populate Idle Level types here
    }

    public enum IdleCostType
    {
        None,
        Price,
        Video,
        //Populate other cost types in any here
    }
    
    public enum LimitType
    {
        None,
        IdleCurrency,
        Float,
        Int,
    }
    
    public enum ActivationStatus
    {
        On,
        Off
    }

}
