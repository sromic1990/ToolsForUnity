﻿using Sirenix.OdinInspector;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;
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
            Engine.Engine.Core.ApplicationRelated.App.GetUpdater().AddAction(updater, type);
            // if (isAttached)
            // {
            //     updateType = type;
            // }
        }
        
        [Button()]
        public void DetachFromUpdate()
        {
            Engine.Engine.Core.ApplicationRelated.App.GetUpdater().RemoveAction("TestScript.UpdateAction");
        }

        private void UpdateAction()
        {
            switch (updateType)
            {
                case UpdateType.Update:
                    // D.Log("From Update");
                    break;
                
                case UpdateType.FixedUpdate:
                    // D.Log("From FixedUpdate");
                    break;
                
                case UpdateType.LateUpdate:
                    // D.Log("From LateUpdate");
                    break;
            }
        }
        
        
    }
}
