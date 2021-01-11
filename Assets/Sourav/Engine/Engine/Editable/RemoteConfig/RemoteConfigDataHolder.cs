using UnityEngine;

namespace Sourav.Engine.Editable.RemoteConfig
{
    public class RemoteConfigDataHolder : MonoBehaviour
    {
        private bool isRemoteConfigSet;

        public bool IsRemoteConfigSet
        {
            get => isRemoteConfigSet;
            set => isRemoteConfigSet = value;
        }
    
    }
}
