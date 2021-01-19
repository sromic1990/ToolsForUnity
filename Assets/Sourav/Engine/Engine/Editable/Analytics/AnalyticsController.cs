#if ANALYTICS
using System.Collections.Generic;
using Facebook.Unity;
using Firebase.Analytics;
using GameAnalyticsSDK;
using Sourav.Engine.Editable.Ads;
using Sourav.DebugRelated;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;

namespace Sourav.Engine.Editable.Analytics
{
    public class AnalyticsController : Sourav.Engine.Core.ControllerRelated.Controller
    {
        private void Start()
        {
            InitializeSDKs();
        }
        
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                // // case Notification.GameLoaded:
                // //     D.Log("GAME LOADED");
                // //     
                // //     break;
                //
                case Notification.RewardVideoSeenAnalytics:
                    SendRVAnalytics(App.GetData<AdsData>().rvType);
                    break;
                
                case Notification.HistoryUnlockedAnalytics:
                    SendHistoryUnlockedAnalytics(param.intData["level"]);
                    break;

                //
                // case Notification.RRVSeenAnalytics:
                //     SendRRVAnalytics(App.GetLevelData().rvPlacementType, (RRVType) param.intData[0]);
                //     break;
                //
                // case Notification.InterstitialSeenAnalytics:
                //     SendInterstitialAnalytics(RewardPlacementType.GameplayScreen);
                //     break;
                //
                // case Notification.TutorialCompletedAnalytics:
                //     SendTutorialCompleteAnalytics();
                //     break;
                //
                // case Notification.ExerciseUnlockedAnalytics:
                //     SendExerciseAnalytics((int)param.floatData[0]);
                //     break;
                //
                // case Notification.WeightMilestoneAnalytics:
                //     SendWeightMilestoneAnalytics(param.floatData[0]);
                //     break;
                //
                // case Notification.DehydratedAnalytics:
                //     SendDehydrationAnalytics();
                //     break;
                //
                // case Notification.OfflineWeightAnalytics:
                //     SendOfflineWeightAnalytics(param.floatData[0], param.floatData[1], param.floatData[2]);
                //     break;
                //
                // case Notification.HardCurrencyAnalytics:
                //     SendHCUsed(param.stringData[0], param.stringData[1]);
                //     break;
                //
                // case Notification.TimeTravelAnalytics:
                //     SendTTUsed(param.floatData[0]);
                //     break;
                //
                // case Notification.FemaleAccessedAnalytics:
                //     SendFemaleAccessed();
                //     break;
                //
                // case Notification.BlackMaleAccessedAnalytics:
                //     SendBlackMaleAccessed();
                //     break;
                //
                // case Notification.BlackFemaleAccessedAnalytics:
                //     SendBlackFemaleAccessed();
                //     break;
                //
                // case Notification.RV20Analytics:
                //     SendRV20();
                //     break;
                //
                // case Notification.RV30Analytics:
                //     SendRV30();
                //     break;
                //
                // case Notification.IAPAnalyticsYes:
                //     SendIAPYes();
                //     break;
                //
                // case Notification.IAPAnalyticsNo:
                //     SendIAPNo();
                //     break;
                //
                // case Notification.ObjectiveCompleted:
                //     SendObjectiveCompleted(param.stringData[0]);
                //     break;
                //
                // case Notification.ClaimDailyRewardAnalytics:
                //     HandleDailyClaim(param.intData[0], param.stringData[0], ClaimType.Regular);
                //     break;
                //
                // case Notification.ClaimDailyRewardDiamondAnalytics:
                //     HandleDailyClaim(param.intData[0], param.stringData[0], ClaimType.Diamond);
                //     break;
                //
                // case Notification.ClaimDailyRewardVideoAnalytics:
                //     HandleDailyClaim(param.intData[0], param.stringData[0], ClaimType.Video);
                //     break;
                //
                // case Notification.IsFirstAppLaunch:
                //     SendFirstAppLaunch();
                //     break;
                //
                // case Notification.ABPanelFirstJoin:
                //     SendABFirstAnalytics(param.stringData[0]);
                //     break;
                //
                // case Notification.ABPanelJoin:
                //     SendABAnalytics(param.stringData[0]);
                //     break;
                //
                // case Notification.AccessoriesUnlocked:
                //     SendAccessoriesUnlocked((AccessoriesType) param.intData[0]);
                //     break;
                //
                // case Notification.AccessoriesEquip:
                //     SendAccessoriesEquipped((AccessoriesType) param.intData[0]);
                //     break;
                //
                // case Notification.AccessoriesUnequip:
                //     SendAccessoriesUnequipped((AccessoriesType) param.intData[0]);
                //     break;
            }
        }

        private void InitializeSDKs()
        {
            D.Log("Initializing Analytics SDK");
            
            #region FBSDK
            if (FB.IsInitialized) 
            {
                FB.ActivateApp();
            } 
            else 
            {
                //Handle FB.Init
                FB.Init( () => {
                    FB.ActivateApp();
                });
            }
            #endregion
            
            #region FIREBASESDK
            FirebaseAnalytics.SetAnalyticsCollectionEnabled(true);
            #endregion
            
            #region GAMEANALYTICS
            GameAnalytics.Initialize();
            #endregion

            // #region APPSFLYERSDK
            // /* Mandatory - set your AppsFlyer’s Developer key. */
            // AppsFlyer.setAppsFlyerKey (StaticScripts.Constants.APPSFLYER_DEVLOPER_KEY);
            // /* For detailed logging */
            // AppsFlyer.setIsDebug (true);
            // #if UNITY_IOS
            // /* Mandatory - set your apple app ID
            //  NOTE: You should enter the number only and not the "ID" prefix */
            // AppsFlyer.setAppID (StaticScripts.Constants.APPSFLYER_IOS_ID);
            // AppsFlyer.getConversionData();
            // AppsFlyer.trackAppLaunch ();
            // #elif UNITY_ANDROID
            // /* Mandatory - set your Android package name */
            // AppsFlyer.setAppID (Application.identifier);
            // /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
            // AppsFlyer.init (StaticScripts.Constants.APPSFLYER_DEVLOPER_KEY,"AppsFlyerTrackerCallbacks");
            // #endif
            // #endregion
        }

         private void SendRVAnalytics(RVType type)
         {
             D.Log("Sending RV Analytics");
             
             // //OLD EVENT
             // FB.LogAppEvent(
             //     "RewardVideoSeen"
             // );
             // Firebase.Analytics.FirebaseAnalytics
             //     .LogEvent("RewardVideoSeen");
             // System.Collections.Generic.Dictionary<string, string> rewardAdSeen = new  
             //     System.Collections.Generic.Dictionary<string, string> ();
             // AppsFlyer.trackRichEvent ("Reward Ad Viewed", rewardAdSeen);
             
             //NEW EVENTS
             var dict = new Dictionary<string, object>();
             dict.Add("placement", type.ToString());
             FB.LogAppEvent ("ad_rewarded_shown", 0, dict);
             
             Parameter paramP = new Parameter("placement", type.ToString());
             FirebaseAnalytics.LogEvent("ad_rewarded_shown", paramP);
             
             GameAnalytics.NewAdEvent(GAAdAction.RewardReceived, GAAdType.RewardedVideo, "IronSource", type.ToString());
         }

         private void SendHistoryUnlockedAnalytics(int level)
         {
             D.Log("Sending Level Complete Analytics");
             
             var dict = new Dictionary<string, object>();
             dict.Add("level", level.ToString());
             FB.LogAppEvent ("level_complete", 0, dict);
             
             Parameter paramP = new Parameter("level", level.ToString());
             FirebaseAnalytics.LogEvent("level_complete", paramP);

             GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, "level_complete " + level.ToString(), 0);
         }
//         
//         private void SendRRVAnalytics(RewardPlacementType type, RRVType rrv)
//         {
//             D.Log("Sending RV Analytics");
//             
//             //OLD EVENT
//             FB.LogAppEvent(
//                 "RewardVideoSeen"
//             );
//             Firebase.Analytics.FirebaseAnalytics
//                 .LogEvent("RewardVideoSeen");
//             // System.Collections.Generic.Dictionary<string, string> rewardAdSeen = new  
//             //     System.Collections.Generic.Dictionary<string, string> ();
//             // AppsFlyer.trackRichEvent ("Reward Ad Viewed", rewardAdSeen);
//             
//             //NEW EVENTS
//             var dict = new Dictionary<string, object>();
//             dict.Add("character", App.GetIdleData().GetPlayerId());
//             dict.Add("placement", type.ToString());
//             dict.Add("rrvType", rrv.ToString());
//             FB.LogAppEvent ("ad_rewarded_shown", 0, dict);
//             
//             Parameter paramC = new Parameter("character", App.GetIdleData().GetPlayerId());
//             Parameter paramP = new Parameter("placement", type.ToString());
//             Parameter paramT = new Parameter("rrvType", rrv.ToString());
//             FirebaseAnalytics.LogEvent("ad_rewarded_shown", paramC, paramP, paramT);
//             
//             AppsFlyer.trackRichEvent("ad_rewarded_shown", new Dictionary<string, string>());
//         }
//         
//         private void SendInterstitialAnalytics(RewardPlacementType type)
//         {
//             D.Log("Sending interstitial Analytics");
//             
//             var dict = new Dictionary<string, object>();
//             dict.Add("character", App.GetIdleData().GetPlayerId());
//             dict.Add("placement", type.ToString());
//             FB.LogAppEvent ("ad_interstitial_shown", 0, dict);
//             
//             Parameter paramC = new Parameter("character", App.GetIdleData().GetPlayerId());
//             Parameter paramP = new Parameter("placement", type.ToString());
//             FirebaseAnalytics.LogEvent("ad_interstitial_shown", paramC, paramP);
//             
//             AppsFlyer.trackRichEvent("ad_interstitial_shown", new Dictionary<string, string>());
//         }
//
//         private void SendTutorialCompleteAnalytics()
//         {
//             D.Log("Sending Tutorial Completed Analytics");
//             
//             FB.LogAppEvent ("tutorial_completed");
//             
//             FirebaseAnalytics.LogEvent("tutorial_completed");
//         }
//
//         private void SendExerciseAnalytics(int exerciseNumber)
//         {
//             D.Log($"Sending Exercise Analytics , exercise Number {exerciseNumber}, character {App.GetIdleData().GetPlayerId()}");
//             
//             var dict = new Dictionary<string, object>();
//             dict.Add("character", App.GetIdleData().GetPlayerId());
//             dict.Add("exerciseNumber", exerciseNumber.ToString());
//             FB.LogAppEvent ("exercise_unlocked", 0, dict);
//             
//             Parameter paramC = new Parameter("character", App.GetIdleData().GetPlayerId());
//             Parameter paramE = new Parameter("exerciseNumber", exerciseNumber.ToString());
//             FirebaseAnalytics.LogEvent("exercise_unlocked", paramC, paramE);
//         }
//
//         private void SendWeightMilestoneAnalytics(float milestone)
//         {
//             D.Log($"Sending Weight Milestone Analytics , weight milestone {milestone}, character {App.GetIdleData().GetPlayerId()}");
//             
//             var dict = new Dictionary<string, object>();
//             dict.Add("character", App.GetIdleData().GetPlayerId());
//             dict.Add("weight", milestone.ToString());
//             FB.LogAppEvent ("weight_milestone", 0, dict);
//             
//             Parameter paramC = new Parameter("character", App.GetIdleData().GetPlayerId());
//             Parameter paramW = new Parameter("weight", milestone.ToString());
//             FirebaseAnalytics.LogEvent("weight_milestone", paramC, paramW);
//         }
//
//         private void SendDehydrationAnalytics()
//         {
//             D.Log($"Sending Dehydration Analytics , character {App.GetIdleData().GetPlayerId()}");
//             
//             var dict = new Dictionary<string, object>();
//             dict["character"] = App.GetIdleData().GetPlayerId();
//             FB.LogAppEvent ("dehydrated", 0, dict);
//             
//             Parameter param = new Parameter("character", App.GetIdleData().GetPlayerId());
//             FirebaseAnalytics.LogEvent("dehydrated", param);
//         }
//         
//         private void SendOfflineWeightAnalytics(float baseWeight, float returnWeight, float gainedWeight)
//         {
//             D.Log($"Sending Offline weight Analytics , base weight {baseWeight}, return weight {returnWeight}, gained weight{gainedWeight}, character {App.GetIdleData().GetPlayerId()}");
//             
//             var dict = new Dictionary<string, object>();
//             dict["character"] = App.GetIdleData().GetPlayerId();
//             dict["baseAmount"] = baseWeight.ToString();
//             dict["returnAmount"] = returnWeight.ToString();
//             dict["weightGained"] = gainedWeight.ToString();
//             FB.LogAppEvent ("offline_weight", 0, dict);
//             
//             Parameter paramC = new Parameter("character", App.GetIdleData().GetPlayerId());
//             Parameter paramB = new Parameter("baseAmount", baseWeight.ToString());
//             Parameter paramR = new Parameter("returnAmount", returnWeight.ToString());
//             Parameter paramG = new Parameter("weightGained", gainedWeight.ToString());
//             FirebaseAnalytics.LogEvent("offline_weight", paramC, paramB, paramR, paramG);
//         }
//         
//         private void SendAccessoriesUnlocked(AccessoriesType type)
//         {
//             D.Log($"Sending Accessory {type} Unlocked for character {App.GetIdleData().GetPlayerId()}");
//             
//             var dict = new Dictionary<string, object>();
//             dict["character"] = App.GetIdleData().GetPlayerId();
//             dict["accessory"] = type.ToString();
//             FB.LogAppEvent ("accessory_unlocked", 0, dict);
//             
//             Parameter paramC = new Parameter("character", App.GetIdleData().GetPlayerId());
//             Parameter paramB = new Parameter("accessory", type.ToString());
//             FirebaseAnalytics.LogEvent("accessory_unlocked", paramC, paramB);
//         }
//         
//         private void SendAccessoriesEquipped(AccessoriesType type)
//         {
//             D.Log($"Sending Accessory {type} Equipped for character {App.GetIdleData().GetPlayerId()}");
//             
//             var dict = new Dictionary<string, object>();
//             dict["character"] = App.GetIdleData().GetPlayerId();
//             dict["accessory"] = type.ToString();
//             FB.LogAppEvent ("accessory_equipped", 0, dict);
//             
//             Parameter paramC = new Parameter("character", App.GetIdleData().GetPlayerId());
//             Parameter paramB = new Parameter("accessory", type.ToString());
//             FirebaseAnalytics.LogEvent("accessory_equipped", paramC, paramB);
//         }
//         
//         private void SendAccessoriesUnequipped(AccessoriesType type)
//         {
//             D.Log($"Sending Accessory {type} Unequipped for character {App.GetIdleData().GetPlayerId()}");
//             
//             var dict = new Dictionary<string, object>();
//             dict["character"] = App.GetIdleData().GetPlayerId();
//             dict["accessory"] = type.ToString();
//             FB.LogAppEvent ("accessory_unequipped", 0, dict);
//             
//             Parameter paramC = new Parameter("character", App.GetIdleData().GetPlayerId());
//             Parameter paramB = new Parameter("accessory", type.ToString());
//             FirebaseAnalytics.LogEvent("accessory_unequipped", paramC, paramB);
//         }
//         
//         private void SendHCUsed(string amount, string nameOfPlacement)
//         {
//             D.Log($"Sending HC Used Analytics , amount {amount}, plavement {nameOfPlacement}");
//             
//             var dict = new Dictionary<string, object>();
//             dict.Add("character", App.GetIdleData().GetPlayerId());
//             dict.Add("amount", amount);
//             dict.Add("placement", nameOfPlacement);
//             FB.LogAppEvent ("hc_used", 0, dict);
//             
//             Parameter paramC = new Parameter("character", App.GetIdleData().GetPlayerId());
//             Parameter paramA = new Parameter("amount", amount);
//             Parameter paramP = new Parameter("placement", nameOfPlacement);
//             FirebaseAnalytics.LogEvent("hc_used", paramC, paramA, paramP);
//         }
//
//         private void SendTTUsed(float amountOfTT)
//         {
//             D.Log($"Sending Time travel Analytics, {amountOfTT} seconds of time travel, character {App.GetIdleData().GetPlayerId()}");
//             
//             var dict = new Dictionary<string, object>();
//             dict.Add("character", App.GetIdleData().GetPlayerId());
//             dict.Add("amount", amountOfTT.ToString() + " seconds of time travel");
//             FB.LogAppEvent ("time_travel_used", 0, dict);
//             
//             Parameter paramC = new Parameter("character", App.GetIdleData().GetPlayerId());
//             Parameter paramA = new Parameter("amount", amountOfTT.ToString() + " seconds of time travel");
//             FirebaseAnalytics.LogEvent("time_travel_used", paramC, paramA);
//         }
//
//         private void SendFemaleAccessed()
//         {
//             D.Log($"Sending Female accessed Analytics");
//             
//             FB.LogAppEvent ("female_accessed");
//             FirebaseAnalytics.LogEvent("female_accessed");
//         }
//
//         private void SendBlackMaleAccessed()
//         {
//             D.Log($"Sending Black Male accessed Analytics");
//             
//             FB.LogAppEvent ("black_male_accessed");
//             FirebaseAnalytics.LogEvent("black_male_accessed");
//         }
//
//         private void SendBlackFemaleAccessed()
//         {
//             D.Log($"Sending Black Female accessed Analytics");
//             
//             FB.LogAppEvent ("black_female_accessed");
//             FirebaseAnalytics.LogEvent("black_female_accessed");
//         }
//
//         private void SendRV20()
//         {
//             D.Log("Sending 20 RVs analytics");
//             
//             AppsFlyer.trackRichEvent("20th_rewarded", new Dictionary<string, string>());
//         }
//         
//         private void SendRV30()
//         {
//             D.Log("Sending 30 RVs analytics");
//             
//             AppsFlyer.trackRichEvent("30th_rewarded", new Dictionary<string, string>());
//         }
//
//         private void SendIAPYes()
//         {
//             D.Log($"Sending IAP analytics SUCCESS id: {App.GetLevelData().currentProduct.iapId}, name: {App.GetLevelData().currentProduct.iapName}, currency: {App.GetLevelData().currentProduct.iapCurrency}, price: {App.GetLevelData().currentProduct.iapValue}");
//             
//             var dict = new Dictionary<string, object>();
//             dict.Add("product_id", App.GetLevelData().currentProduct.iapId);
//             dict.Add("product_name", App.GetLevelData().currentProduct.iapName);
//             dict.Add("currency", App.GetLevelData().currentProduct.iapCurrency);
//             dict.Add("price", App.GetLevelData().currentProduct.iapValue);
//             dict.Add("value", App.GetLevelData().currentProduct.iapValue);
//             dict.Add("validated", "Yes");
//             FB.LogAppEvent ("in_app_purchase", 0, dict);
//             
//             Parameter paramid = new Parameter("product_id", App.GetLevelData().currentProduct.iapId);
//             Parameter paramname = new Parameter("product_name", App.GetLevelData().currentProduct.iapName);
//             Parameter paramcurrency = new Parameter("currency", App.GetLevelData().currentProduct.iapCurrency);
//             Parameter paramprice = new Parameter("price", App.GetLevelData().currentProduct.iapValue);
//             Parameter paramvalue = new Parameter("value", App.GetLevelData().currentProduct.iapValue);
//             Parameter paramvalidation = new Parameter("validated", "Yes");
//             FirebaseAnalytics.LogEvent("in_app_purchase", paramid, paramname, paramcurrency, paramprice, paramvalue, paramvalidation);
//             
//             Dictionary<string, string> eventValue = new Dictionary<string,string> ();
//             float value = 0.7f * App.GetLevelData().currentProduct.iapValue;
//             eventValue.Add("af_purchase",value.ToString());
//
//             PurchaseEventArgs args = App.GetLevelData().args;
//             
//             D.Log($"id = {App.GetLevelData().currentProduct.iapId}");
//             D.Log($"price = {args.purchasedProduct.metadata.localizedPrice.ToString(CultureInfo.InvariantCulture)}");
//             D.Log($"currencyCode = {args.purchasedProduct.metadata.isoCurrencyCode}");
//             D.Log($"currencyCode = {args.purchasedProduct.metadata.isoCurrencyCode}");
//             D.Log($"transaction id = {args.purchasedProduct.transactionID}");
//             
//             
//             #if UNITY_IOS
//             AppsFlyer.validateReceipt(App.GetLevelData().currentPrice.iapId, 
//                 args.purchasedProduct.metadata.localizedPrice.ToString(), 
//                 args.purchasedProduct.metadata.isoCurrencyCode, 
//                 args.purchasedProduct.transactionID, 
//                 new Dictionary<string,string> ());
//             #endif
//
//             #if UNITY_ANDROID
//             var wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(args.purchasedProduct.receipt);
//             if (null == wrapper)
//             {
//                 return;
//             }
//             // Corresponds to http://docs.unity3d.com/Manual/UnityIAPPurchaseReceipts.html
//             var store = (string)wrapper["Store"];
//             var payload = (string)wrapper["Payload"]; // For Apple this will be the base64 encoded ASN.1 receipt
//             // For GooglePlay payload contains more JSON
//             var gpDetails = (Dictionary<string, object>)MiniJson.JsonDecode(payload);
//             var gpJson = (string)gpDetails["json"];
//             var signature = (string)gpDetails["signature"];
//             
//             AppsFlyer.validateReceipt(
//                 StaticScripts.Constants.GOOGLE_PUBLIC_KEY, 
//                 App.GetLevelData().currentProduct.iapId, 
//                 signature, 
//                 args.purchasedProduct.metadata.localizedPrice.ToString(CultureInfo.InvariantCulture), 
//                 args.purchasedProduct.metadata.isoCurrencyCode, 
//                 new Dictionary<string,string> ());
//             #endif
//         }
//
//         private void SendIAPNo()
//         {
//             D.Log($"Sending IAP analytics FAILURE id: {App.GetLevelData().currentProduct.iapId}, name: {App.GetLevelData().currentProduct.iapName}, currency: {App.GetLevelData().currentProduct.iapCurrency}, price: {App.GetLevelData().currentProduct.iapValue}");
//             
//             var dict = new Dictionary<string, object>();
//             dict.Add("product_id", App.GetLevelData().currentProduct.iapId);
//             dict.Add("product_name", App.GetLevelData().currentProduct.iapName);
//             dict.Add("currency", App.GetLevelData().currentProduct.iapCurrency);
//             dict.Add("price", App.GetLevelData().currentProduct.iapValue);
//             dict.Add("value", App.GetLevelData().currentProduct.iapValue);
//             dict.Add("validated", "No");
//             FB.LogAppEvent ("in_app_purchase", 0, dict);
//             
//             Parameter paramid = new Parameter("product_id", App.GetLevelData().currentProduct.iapId);
//             Parameter paramname = new Parameter("product_name", App.GetLevelData().currentProduct.iapName);
//             Parameter paramcurrency = new Parameter("currency", App.GetLevelData().currentProduct.iapCurrency);
//             Parameter paramprice = new Parameter("price", App.GetLevelData().currentProduct.iapValue);
//             Parameter paramvalue = new Parameter("value", App.GetLevelData().currentProduct.iapValue);
//             Parameter paramvalidation = new Parameter("validated", "No");
//             FirebaseAnalytics.LogEvent("in_app_purchase", paramid, paramname, paramcurrency, paramprice, paramvalue, paramvalidation);
//             
//             // Dictionary<string, string> eventValue = new Dictionary<string,string> ();
//             // float value = 0.7f * App.GetLevelData().currentPrice.iapValue;
//             // eventValue.Add("af_revenue",value.ToString());
//             // AppsFlyer.trackRichEvent("af_purchase", eventValue);
//             // AppsFlyer.validateReceipt(paramid.ToString(), paramprice.ToString(), paramcurrency.ToString(), App.GetLevelData().currentPrice.args.purchasedProduct.transactionID, new Dictionary<string,string> ());
//         }
//         
// //         public void iOSVerifyIAP (string productIdentifier,
// //             float price, string currency, string transactionId,
// //             Dictionary<string, string> additionalParameters) 
// //         {
// // #if UNITY_IOS
// //             AppsFlyer.validateReceipt(productIdentifier, price.ToString(), currency, transactionId, additionalParameters);
// // #endif
// //         }
//
//         private void SendObjectiveCompleted(string Objective)
//         {
//             D.Log($"Sending Objective Analytics, Objective {Objective}");
//             
//             var dict = new Dictionary<string, object>();
//             dict.Add("character", App.GetIdleData().GetPlayerId());
//             dict.Add("objective", Objective);
//             FB.LogAppEvent ("objective_completed", 0, dict);
//             
//             Parameter paramC = new Parameter("character", App.GetIdleData().GetPlayerId());
//             Parameter paramA = new Parameter("objective", Objective);
//             FirebaseAnalytics.LogEvent("objective_completed", paramC, paramA);
//         }
//         
//         private void HandleDailyClaim(int dayIndex, string id, ClaimType type)
//         {
//             D.Log($"Claiming Day {dayIndex}, Reward {id}, as {type}");
//             switch (type)
//             {
//                 case ClaimType.Regular:
//                     var dict_N = new Dictionary<string, object>();
//                     dict_N.Add("reward_day", dayIndex);
//                     dict_N.Add("reward_id", id);
//                     dict_N.Add("reward_type", "normal_claim");
//                     FB.LogAppEvent ("daily_reward", 0, dict_N);
//             
//                     Parameter paramD_N = new Parameter("reward_day", dayIndex);
//                     Parameter paramI_N = new Parameter("reward_id", id);
//                     Parameter paramT_N = new Parameter("reward_type", "normal_claim");
//                     FirebaseAnalytics.LogEvent("daily_reward", paramD_N, paramI_N, paramT_N);
//                     break;
//                 
//                 case ClaimType.Diamond:
//                     var dict_D = new Dictionary<string, object>();
//                     dict_D.Add("reward_day", dayIndex);
//                     dict_D.Add("reward_id", id);
//                     dict_D.Add("reward_type", "diamond_claim");
//                     FB.LogAppEvent ("daily_reward", 0, dict_D);
//             
//                     Parameter paramD_D = new Parameter("reward_day", dayIndex);
//                     Parameter paramI_D = new Parameter("reward_id", id);
//                     Parameter paramT_D = new Parameter("reward_type", "diamond_claim");
//                     FirebaseAnalytics.LogEvent("daily_reward", paramD_D, paramI_D, paramT_D);
//                     break;
//                 
//                 case ClaimType.Video:
//                     var dict_V = new Dictionary<string, object>();
//                     dict_V.Add("reward_day", dayIndex);
//                     dict_V.Add("reward_id", id);
//                     dict_V.Add("reward_type", "video_claim");
//                     FB.LogAppEvent ("daily_reward", 0, dict_V);
//             
//                     Parameter paramD_V = new Parameter("reward_day", dayIndex);
//                     Parameter paramI_V = new Parameter("reward_id", id);
//                     Parameter paramT_V = new Parameter("reward_type", "video_claim");
//                     FirebaseAnalytics.LogEvent("daily_reward", paramD_V, paramI_V, paramT_V);
//                     break;
//             }
//         }
//
//         private void SendFirstAppLaunch()
//         {
//             D.Log($"Sending first_app_launch");
//             
//             FB.LogAppEvent ("first_app_launch");
//             
//             FirebaseAnalytics.LogEvent("first_app_launch");
//         }
//         
//         private void SendABFirstAnalytics(string value)
//         {
//             D.Log($"Sending AB Analytics First, Case {value}");
//             
//             var dict = new Dictionary<string, object>();
//             dict.Add("panel", value);
//             FB.LogAppEvent ("join_panel_first", 0, dict);
//             
//             Parameter paramP = new Parameter("panel", value);
//             FirebaseAnalytics.LogEvent("join_panel_first", paramP);
//         }
//
//         private void SendABAnalytics(string value)
//         {
//             D.Log($"Sending AB Analytics, Case {value}");
//             
//             var dict = new Dictionary<string, object>();
//             dict.Add("panel", value);
//             FB.LogAppEvent ("join_panel", 0, dict);
//             
//             Parameter paramP = new Parameter("panel", value);
//             FirebaseAnalytics.LogEvent("join_panel", paramP);
//         }
    
    }

    public enum ClaimType
    {
        None,
        Regular,
        Video,
        Diamond,
    }
}
#endif
