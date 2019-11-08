#if APPSFLYER
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.NotificationRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.ControllerRelated
{
	public class AppsFlyerController : Controller
	{

		[SerializeField] private string developerKey;
		[SerializeField] private string iOSKey;
		[SerializeField] private string androidPackageName;
		
		// Use this for initialization
		void Start () {
/* Mandatory - set your AppsFlyer’s Developer key. */
			AppsFlyer.setAppsFlyerKey (developerKey);
/* For detailed logging */
/* AppsFlyer.setIsDebug (true); */
#if UNITY_IOS
			/* Mandatory - set your apple app ID
			 NOTE: You should enter the number only and not the "ID" prefix */
			AppsFlyer.setAppID (iOSKey);
			AppsFlyer.trackAppLaunch ();
#elif UNITY_ANDROID
  /* Mandatory - set your Android package name */
  AppsFlyer.setAppID (androidPackageName);
  /* For getting the conversion data in Android, you need to add the "AppsFlyerTrackerCallbacks" listener.*/
  AppsFlyer.init ("YOUR_APPSFLYER_DEV_KEY","AppsFlyerTrackerCallbacks");
#endif
		}

		public override void OnNotificationReceived(Notification notification, NotificationParam param = null)
		{
			switch (notification)
			{
				case Notification.LevelComplete:
					System.Collections.Generic.Dictionary<string, string> levelComplete = new  
						System.Collections.Generic.Dictionary<string, string> ();
					levelComplete.Add ("LevelComplete", App.GetLevelData().CurrentLevel.ToString());
					AppsFlyer.trackRichEvent ("LevelComplete", levelComplete);

					break;
                
				case Notification.AdRewarded:
					System.Collections.Generic.Dictionary<string, string> rewardAdSeen = new  
						System.Collections.Generic.Dictionary<string, string> ();
					AppsFlyer.trackRichEvent ("Reward Ad Viewed", rewardAdSeen);
					break;
			}
		}
	}
}
#endif
