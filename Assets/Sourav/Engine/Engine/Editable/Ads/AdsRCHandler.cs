#if SET
using System.Collections.Generic;
using Sourav.Engine.Editable.RemoteConfig;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using UnityEngine.Purchasing;

namespace Sourav.Engine.Editable.Ads
{
    public class AdsRCHandler : RCHandlerBase
    {
        public override void HandleRC(string data, RCType type)
        {
            base.HandleRC(data, type);
            
            var wrapper = (Dictionary<string, object>)MiniJson.JsonDecode(data);
            if (null == wrapper)
            {
                return;
            }
            
            string isFSOnString = wrapper["isFSOn"].ToString();
            bool.TryParse(isFSOnString, out App.GetData<AdsData>().isFSOn);

            string clicksPerAdString = wrapper["clicksPerFS"].ToString();
            int.TryParse(clicksPerAdString, out App.GetData<AdsData>().clicksPerAd);
            App.GetData<AdsData>().currentClicks = 0;
            
            
            
            base.RCSet();

        }
    }
}
#endif