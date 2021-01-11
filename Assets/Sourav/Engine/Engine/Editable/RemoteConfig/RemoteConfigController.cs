using System;
using System.Collections;
using System.Threading.Tasks;
using Firebase.Extensions;
using Sourav.DebugRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.RemoteConfig
{
    public class RemoteConfigController : Core.ControllerRelated.Controller
    {
        Firebase.DependencyStatus dependencyStatus = Firebase.DependencyStatus.UnavailableOther;
        protected bool isFirebaseInitialized = false;
        protected bool isFirebaseFetched = false;

        [SerializeField] private RemoteConfigDataHolder dataHolder;
        
        private IEnumerator StartFirebase()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    isFirebaseInitialized = true;
                }
                else
                {
                    D.LogError("Could not resolve Firebase dependencies: "+dependencyStatus);
                }
            });

            yield return StartCoroutine(HandleFirebaseInitialization());
        }

        private IEnumerator HandleFirebaseInitialization()
        {
            while (!isFirebaseInitialized)
            {
                yield return null;
            }
            InitializeFirebase();
            FetchDataAsync();
            while (!isFirebaseFetched)
            {
                yield return null;
            }
            if (Firebase.RemoteConfig.FirebaseRemoteConfig.ActivateFetched())
            {
                D.Log("Remote Value");   
            }
            else
            {
                D.Log("Local Value");
            }
            SetFirebaseValues();
        }

        private void InitializeFirebase()
        {
            D.Log("RemoteConfig configured and ready!");
            
            // [START set_defaults]
            System.Collections.Generic.Dictionary<string, object> defaults =
                new System.Collections.Generic.Dictionary<string, object>();

            // These are the values that are used if we haven't fetched data from the
            // server
            // yet, or if we ask for values that the server doesn't have:
            for (int i = 0; i < App.GetData<RCData>().rcDefaultValues.Length; i++)
            {
                switch (App.GetData<RCData>().rcDefaultValues[i].valueType)
                {
                    case TypeOfValue.Integer:
                        defaults.Add(App.GetData<RCData>().rcDefaultValues[i].nameOfField, App.GetData<RCData>().rcDefaultValues[i].intValue);
                        break;
                    
                    case TypeOfValue.Float:
                        defaults.Add(App.GetData<RCData>().rcDefaultValues[i].nameOfField, App.GetData<RCData>().rcDefaultValues[i].floatValue);
                        break;
                    
                    case TypeOfValue.String:
                        defaults.Add(App.GetData<RCData>().rcDefaultValues[i].nameOfField, App.GetData<RCData>().rcDefaultValues[i].stringValue);
                        break;
                    
                    case TypeOfValue.Bool:
                        defaults.Add(App.GetData<RCData>().rcDefaultValues[i].nameOfField, App.GetData<RCData>().rcDefaultValues[i].boolValue);
                        break;
                }
            }
            // SetUpDefaultValues();

            Firebase.RemoteConfig.FirebaseRemoteConfig.SetDefaults(defaults);
            // [END set_defaults]
            D.Log("RemoteConfig configured and ready!");
            isFirebaseInitialized = true;
        }

        public Task FetchDataAsync() 
        {
            System.Threading.Tasks.Task fetchTask = Firebase.RemoteConfig.FirebaseRemoteConfig.FetchAsync(
                TimeSpan.Zero);
            return fetchTask.ContinueWithOnMainThread((Task task) =>
            {
                isFirebaseFetched = true;
            });
        }

        private void SetFirebaseValues()
        {
            for (int i = 0; i < App.GetData<RCData>().rcDefaultValues.Length; i++)
            {
                // D.Log($"type:  {App.GetData<RCData>().rcDefaultValues[i].type} , data = {Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(App.GetData<RCData>().rcDefaultValues[i].nameOfField).StringValue}");
                App.GetData<RCData>().rcDefaultValues[i].stringValue = Firebase.RemoteConfig.FirebaseRemoteConfig
                    .GetValue(App.GetData<RCData>().rcDefaultValues[i].nameOfField).StringValue;
                App.GetData<RCData>().rcDefaultValues[i].rcHandler.HandleRC(App.GetData<RCData>().rcDefaultValues[i].stringValue, App.GetData<RCData>().rcDefaultValues[i].type);
                
                // switch (rcDefaultValues[i].type)
                // {
                //     // case RCType.FSTimerAfterRV:
                //     //     App.GetLevelData().maxCoolDownAfterRVSeconds = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(rcDefaultValues[i].nameOfField).LongValue;
                //     //     break;
                //     //
                //     // case RCType.FSFrquency:
                //     //     App.GetLevelData().maxFSSeconds = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(rcDefaultValues[i].nameOfField).LongValue;
                //     //     break;
                //     //
                //     // case RCType.FSLaunchDelay:
                //     //     App.GetLevelData().maxGapOnLaunchSeconds = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(rcDefaultValues[i].nameOfField).LongValue;
                //     //     break;
                //     //
                //     // case RCType.FloatingRVFrequency:
                //     //     App.GetLevelData().rrvIntervalSecondsFixed = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(rcDefaultValues[i].nameOfField).LongValue;
                //     //     break;
                //     //
                //     // case RCType.DayTimerDuration:
                //     //     App.GetLevelData().secondsPerWeek = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(rcDefaultValues[i].nameOfField).LongValue;
                //     //     App.GetLevelData().secondsToCalculateEarning = App.GetLevelData().secondsPerWeek / 2;
                //     //     break;
                //     //
                //     // case RCType.TappersLimit:
                //     //     App.GetLevelData().maxTaps = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(rcDefaultValues[i].nameOfField).LongValue;
                //     //     App.GetLevelData().midTapLimit = App.GetLevelData().maxTaps / 2;
                //     //     break;
                //     
                //     // case RCType.TriviaURL:
                //     //     string URL = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(rcDefaultValues[i].nameOfField)
                //     //         .StringValue;
                //     //     App.GetLevelData().isTriviaSet = false;
                //     //     if (URL != App.GetLevelData().TriviaURL)
                //     //     {
                //     //         App.GetLevelData().TriviaURL = URL;
                //     //         
                //     //         App.Notify(Notification.DownloadTriviaData);
                //     //     }
                //     //     else
                //     //     {
                //     //         App.Notify(Notification.FetchTriviaData);
                //     //     }
                //     //     break;
                //     
                //     // case RCType.TriviaOn:
                //     //     D.Log($"FB BOOLEAN FOR TRIVIA ON : {Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(rcDefaultValues[i].nameOfField).BooleanValue}");
                //     //     App.GetLevelData().isTriviaOn = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(rcDefaultValues[i].nameOfField).BooleanValue;
                //     //     break;
                //     //
                //     // case RCType.TriviaInterval:
                //     //     D.Log($"FB TRIVIA INTERVAL : {Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(rcDefaultValues[i].nameOfField).LongValue}");
                //     //     App.GetLevelData().maxTriviaTime = (int)Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(rcDefaultValues[i].nameOfField).LongValue;
                //     //     break;
                //
                //     // case RCType.AbTest:
                //     //     string ABTest = App.GetLevelData().ABTest;
                //     //     App.GetLevelData().ABTest = Firebase.RemoteConfig.FirebaseRemoteConfig.GetValue(rcDefaultValues[i].nameOfField).StringValue;
                //     //     if (App.GetLevelData().ABTest != "A")
                //     //     {
                //     //         if (App.GetLevelData().ABTest != ABTest)
                //     //         {
                //     //             //FIRE AB TEST FIRST TIME
                //     //             //ANALYTICS
                //     //             NotificationParam paramJoinFirst = new NotificationParam(Mode.stringData);
                //     //             paramJoinFirst.stringData.Add(App.GetLevelData().ABTest);
                //     //             App.Notify(Notification.ABPanelFirstJoin, paramJoinFirst);
                //     //             //ANALYTICS
                //     //         }
                //     //         //FIRE AB TEST
                //     //         //ANALYTICS
                //     //         NotificationParam paramJoin = new NotificationParam(Mode.stringData);
                //     //         paramJoin.stringData.Add(App.GetLevelData().ABTest);
                //     //         App.Notify(Notification.ABPanelJoin, paramJoin);
                //     //         //ANALYTICS
                //     //     }
                //     //
                //     //     if (!App.GetLevelData().IsFirstAppLaunchTriggered)
                //     //     {
                //     //         App.GetLevelData().IsFirstAppLaunchTriggered = true;
                //     //         App.Notify(Notification.IsFirstAppLaunch);
                //     //     }
                //     //     break;
                // }
            }
            // App.Notify(Notification.HideServerFetch);
            App.Notify(Notification.RemoteConfigSet);
        }

        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.SetUpRemoteConfig:
                    App.Notify(Notification.ShowServerFetch);
                    if (!DoesRMExist())
                    {
                        StartCoroutine(StartFirebase());
                    }
                    else
                    {
                        ProcessDataFromRM();
                    }
                    break;
            }
        }

        private bool DoesRMExist()
        {
            RemoteConfigDataHolder dataHolder = FindObjectOfType<RemoteConfigDataHolder>();
            if (dataHolder != null)
            {
                this.dataHolder = dataHolder;
                return true;
            }
            return false;
        }

        private void ProcessDataFromRM()
        {
            if (dataHolder != null)
            {
                if (dataHolder.IsRemoteConfigSet)
                {
                    isFirebaseInitialized = dataHolder.IsRemoteConfigSet;
                    StartCoroutine(HandleFirebaseInitialization());
                }
                else
                {
                    StartCoroutine(StartFirebase());
                }
            }
            else
            {
                StartCoroutine(StartFirebase());
            }
        }
    }
}