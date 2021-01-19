#if ADJUST
using com.adjust.sdk;
using Sourav.Engine.Editable.StaticScripts;
using Sourav.DebugRelated;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.Engine.Editable.Analytics
{
    public class AdjustElement : GameElement
    {
        private void Start()
        {
            string adjustAppToken = Constants.ADJUST_TOKEN_IOS;
            if (Application.platform == RuntimePlatform.Android)
            {
                adjustAppToken = Constants.ADJUST_TOKEN_ANDROID;
            }
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                adjustAppToken = Constants.ADJUST_TOKEN_IOS;
            }
            
            AdjustEnvironment environment = AdjustEnvironment.Production;

            if (Debug.isDebugBuild)
                environment = AdjustEnvironment.Sandbox;
            else
                environment = AdjustEnvironment.Production;


            AdjustConfig config = new AdjustConfig(adjustAppToken, environment, true);

            config.setDeferredDeeplinkDelegate(DeferredDeeplinkCallback);

            if (Debug.isDebugBuild)
                config.setLogLevel(AdjustLogLevel.Verbose);
            else
                config.setLogLevel(AdjustLogLevel.Info);
            config.setSendInBackground(true);
            new GameObject("Adjust").AddComponent<Adjust>();
            config.setAttributionChangedDelegate(this.attributionChangedDelegate);
            Adjust.start(config);
        }

        private void attributionChangedDelegate(AdjustAttribution obj)
        {
            // DDNAAnalyticsIntegration.DDNAEvent_AdjustAttribute(attribution.network, "", attribution.adgroup, attribution.campaign, attribution.creative, attribution.network, attribution.trackerName, attribution.trackerToken);
        }

        private void DeferredDeeplinkCallback(string deeplinkURL)
        {
            D.Log("Deeplink URL: " + deeplinkURL);
        }
    }
}
#endif