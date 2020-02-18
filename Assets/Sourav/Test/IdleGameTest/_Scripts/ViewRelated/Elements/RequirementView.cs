#if IDLEGAME
using Sirenix.OdinInspector;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.IdleGameEngine.IdleGameData;
using Sourav.Utilities.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Sourav.Test.IdleGameTest._Scripts.ViewRelated.Elements
{
    public class RequirementView : GameElement
    {
        [SerializeField] private GameObject[] incompleteItems;
        [SerializeField] private GameObject[] completeItems;
        [SerializeField] private Text[] valueText;
        [SerializeField] [ReadOnly] private IdleUnitType idleUnitType;

        public void SetUp(SingleDependency dependency)
        {
            idleUnitType = dependency.unitType;
            for (int i = 0; i < valueText.Length; i++)
            {
                valueText[i].text = dependency.countToUnlock.ToString();
            }

            HideAll();
            
            if (dependency.isComplete)
            {
                for (int i = 0; i < completeItems.Length; i++)
                {
                    completeItems[i].Show();
                }
            }
            else
            {
                for (int i = 0; i < incompleteItems.Length; i++)
                {
                    incompleteItems[i].Show();
                }
            }
        }

        private void HideAll()
        {
            for (int i = 0; i < incompleteItems.Length; i++)
            {
                incompleteItems[i].Hide();
            }
            
            for (int i = 0; i < completeItems.Length; i++)
            {
                completeItems[i].Hide();
            }
        }
    }
}
#endif
