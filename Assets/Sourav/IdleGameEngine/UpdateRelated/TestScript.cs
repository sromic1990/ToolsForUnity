using Sirenix.OdinInspector;
using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.IdleGameEngine.UpdateRelated
{
    public class TestScript : GameElement
    {
        [SerializeField][ReadOnly] private UpdateType updateType;
        
        [Button()]
        public void AttachToUpdate(UpdateType type)
        {
            Updater updater = new Updater();
            updater.action = UpdateAction;
            updater.id = "TestScript.UpdateAction";
            // bool isAttached = App.GetUpdater().AddAction(updater, type);
            App.GetUpdater().AddAction(updater, type);
            // if (isAttached)
            // {
            //     updateType = type;
            // }
        }
        
        [Button()]
        public void DetachFromUpdate()
        {
            App.GetUpdater().RemoveAction("TestScript.UpdateAction");
        }

        private void UpdateAction()
        {
            switch (updateType)
            {
                case UpdateType.Update:
                    D.Log("From Update");
                    break;
                
                case UpdateType.FixedUpdate:
                    D.Log("From FixedUpdate");
                    break;
                
                case UpdateType.LateUpdate:
                    D.Log("From LateUpdate");
                    break;
            }
        }
        
        
    }
}
