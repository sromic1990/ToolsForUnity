#if IDLEGAME
using System.Collections.Generic;
using Sourav.Engine.Core.DebugRelated;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.IdleGameEngine.IdleGameData;
using Sourav.Utilities.Extensions;
using UnityEngine;

namespace Sourav.Test.IdleGameTest._Scripts.ViewRelated.Elements
{
    public class IdleButtonGenerator : GameElement
    {
        [SerializeField] private Transform idleButtonHolder;
        [SerializeField] private GameObject idleButtonPrefab;

        public void Generate(IdleLevel level, out List<IdleButtonView> idleButtonViews)
        {
            // D.Log("IdleButtonGenerator::Generate");
            if (idleButtonHolder.childCount > 0)
            {
                idleButtonHolder.DeleteAllChildren();
            }
            
            idleButtonViews = new List<IdleButtonView>();
            
            UpdateButtonViews(level, idleButtonViews);
            
            App.Notify(Notification.IdleButtonsGenerated);
        }

        public void UpdateButtonViews(IdleLevel level, List<IdleButtonView> idleButtonViews)
        {
            for (int i = 0; i < level.idleInfos.Count; i++)
            {
                GameObject gObj = Instantiate(idleButtonPrefab, idleButtonHolder);
                IdleButtonView view = gObj.GetComponent<IdleButtonView>();
                view.SetUp(level.idleInfos[i]);

                idleButtonViews.Add(view);
            }
        }
    }
}
#endif
