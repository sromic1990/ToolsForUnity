using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.Engine.Core.ControllerRelated
{
    public class AndroidHapticManager : Controller
    {
        public class HapticFeedbackManager
        {
            #if UNITY_ANDROID && !UNITY_EDITOR
                private int hapticFeedbackConstantsKey;
                private AndroidJavaObject UnityPlayer;
            #endif

            public HapticFeedbackManager()
            {
                #if UNITY_ANDROID && !UNITY_EDITOR
                hapticFeedbackConstantsKey = new AndroidJavaClass("android.view.HapticFeedbackConstants").GetStatic<int>("VIRTUAL_KEY");
                UnityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer").GetStatic<AndroidJavaObject>("currentActivity").Get<AndroidJavaObject>("mUnityPlayer");
                #endif
            }

            public bool Execute()
            {
                #if UNITY_ANDROID && !UNITY_EDITOR
                return UnityPlayer.Call<bool>("performHapticFeedback", hapticFeedbackConstantsKey);
                #endif
                return false;
            }
            
        }

        private static HapticFeedbackManager _mHapticFeedbackManager;

        public static bool HapticFeedback()
        {
            if (_mHapticFeedbackManager == null)
            {
                _mHapticFeedbackManager = new HapticFeedbackManager();
            }

            return _mHapticFeedbackManager.Execute();
        }
        
        public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
        {
            switch (notification)
            {
                case Notification.HapticAndroid:
                    #if UNITY_ANDROID && !UNITY_EDITOR
                    HapticFeedback();
                    #endif
                    break;
            }
        }
    }
}
