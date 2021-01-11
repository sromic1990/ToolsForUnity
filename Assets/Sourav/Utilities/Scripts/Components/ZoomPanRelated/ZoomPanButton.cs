using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sourav.Utilities.Scripts.Components.ZoomPanRelated
{
    public class ZoomPanButton : GameElement, IPointerDownHandler, IPointerExitHandler, IPointerUpHandler
    {
        public Spinner spinner;
        private bool wasSpinning;
        [SerializeField] private PanZoomScript panZoomScript;
        [SerializeField] private bool isSpinnerSpinning;

        public void OnPointerDown(PointerEventData eventData)
        {
            panZoomScript.StartInput();
            wasSpinning = spinner.IsSpinning;
            if (isSpinnerSpinning)
            {
                if (spinner != null)
                {
                    spinner.StopSpin();
                }
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            panZoomScript.StopInput();

            if (isSpinnerSpinning)
            {
                if (spinner != null)
                {
                    if (wasSpinning)
                    {
                        spinner.StartSpinning();
                    }       
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            panZoomScript.StopInput();
            
            if (isSpinnerSpinning)
            {
                if (spinner != null)
                {
                    if (wasSpinning)
                    {
                        spinner.StartSpinning();
                    }       
                }
            }
        }
    }
}
