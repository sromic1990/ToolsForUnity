using System.Collections.Generic;
using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.NotificationRelated;
using UnityEngine;

namespace Sourav.IdleGameEngine.UpdateRelated
{
    public class UpdateElement : GameElement
    {
        [SerializeField] private List<Updater> updateActions;
        [SerializeField] private List<Updater> lateUpdateActions;
        [SerializeField] private List<Updater> fixedUpdateActions;
        
        private void Update()
        {
            App.GetNotificationCenter().Notify(Notification.Update);

            if (updateActions != null)
            {
                for (int i = 0; i < updateActions.Count; i++)
                {
                    if (updateActions[i].action != null)
                    {
                        updateActions[i].action.Invoke();
                    }
                }
            }
        }

        private void LateUpdate()
        {
            App.GetNotificationCenter().Notify(Notification.LateUpdate);
            
            if (lateUpdateActions != null)
            {
                for (int i = 0; i < lateUpdateActions.Count; i++)
                {
                    if (lateUpdateActions[i].action != null)
                    {
                        lateUpdateActions[i].action.Invoke();
                    }
                }
            }
        }

        private void FixedUpdate()
        {
            App.GetNotificationCenter().Notify(Notification.FixedUpdate);
            
            if (fixedUpdateActions != null)
            {
                for (int i = 0; i < fixedUpdateActions.Count; i++)
                {
                    if (fixedUpdateActions[i].action != null)
                    {
                        fixedUpdateActions[i].action.Invoke();
                    }
                }
            }
        }

        public bool AddAction(Updater updater, UpdateType type)
        {
            if (DoesIdExist(updater.id))
            {
                D.LogError($"id {updater.id} already exists, thus not adding");
                return false;
            }
            else
            {
                switch (type)
                {
                    case UpdateType.Update:
                        if (updateActions == null)
                        {
                            updateActions = new List<Updater>();
                        }
                        updateActions.Add(updater);
                        break;
                    
                    case UpdateType.FixedUpdate:
                        if (fixedUpdateActions == null)
                        {
                            fixedUpdateActions = new List<Updater>();
                        }
                        fixedUpdateActions.Add(updater);
                        break;
                    
                    case UpdateType.LateUpdate:
                        if (lateUpdateActions == null)
                        {
                            lateUpdateActions = new List<Updater>();
                        }
                        lateUpdateActions.Add(updater);
                        break;
                }

                return true;
            }
        }

        public void RemoveAction(string id)
        {
            if (updateActions != null)
            {
                List<Updater> newUpdateActions = new List<Updater>();
                for (int i = 0; i < updateActions.Count; i++)
                {
                    if (updateActions[i].id == id)
                    {
                        continue;
                    }
                    newUpdateActions.Add(updateActions[i]);
                }

                updateActions = newUpdateActions;
            }
            
            if (lateUpdateActions != null)
            {
                List<Updater> newLateUpdateActions = new List<Updater>();
                for (int i = 0; i < lateUpdateActions.Count; i++)
                {
                    if (lateUpdateActions[i].id == id)
                    {
                        continue;
                    }
                    newLateUpdateActions.Add(lateUpdateActions[i]);
                }
                lateUpdateActions = newLateUpdateActions;
            }

            if (fixedUpdateActions != null)
            {
                List<Updater> newFixedUpdateAction = new List<Updater>();
                for (int i = 0; i < fixedUpdateActions.Count; i++)
                {
                    if (fixedUpdateActions[i].id == id)
                    {
                        continue;
                    }
                    newFixedUpdateAction.Add(fixedUpdateActions[i]);
                }
                fixedUpdateActions = newFixedUpdateAction;
            }
        }
        
        private bool DoesIdExist(string id)
        {
            if(updateActions != null)
            {
                for (int i = 0; i < updateActions.Count; i++)
                {
                    if (updateActions[i].id == id)
                    {
                        return true;
                    }
                }
            }
            
            
            if(fixedUpdateActions != null)
            {
                for (int i = 0; i < fixedUpdateActions.Count; i++)
                {
                    if (fixedUpdateActions[i].id == id)
                    {
                        return true;
                    }
                }
            }
            
            
            if (lateUpdateActions != null)
            {
                for (int i = 0; i < lateUpdateActions.Count; i++)
                {
                    if (lateUpdateActions[i].id == id)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }

    [System.Serializable]
    public struct Updater
    {
        public System.Action action;
        public string id;
    }

    public enum UpdateType
    {
        Update,
        LateUpdate,
        FixedUpdate,
    }
}
