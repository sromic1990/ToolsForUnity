using Sirenix.OdinInspector;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sourav.UIPresets
{
    public class ButtonPressEffect : GameElement, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private GameObject pushDownObject;
        [SerializeField] private Vector3 onPointerDownPosition;
        [SerializeField] private Vector3 onPointerUpPosition;

        [SerializeField] private bool isLocal;
        [SerializeField] private bool isChangeImage;

        [ShowIf("isChangeImage", true)] [SerializeField]
        private Image image;

        [ShowIf("isChangeImage", true)] [SerializeField]
        private Sprite pressUpImage;

        [ShowIf("isChangeImage", true)] [SerializeField]
        private Sprite pressDownImage;
        
        public void OnPointerDown(PointerEventData eventData)
        {
            if (isChangeImage)
            {
                if (image != null)
                {
                    if (pressDownImage != null)
                    {
                        image.sprite = pressDownImage;
                    }
                }
            }
            else
            {
                if (isLocal)
                {
                    pushDownObject.transform.localPosition = onPointerDownPosition;
                }
                else
                {
                    pushDownObject.transform.position = onPointerDownPosition;
                }
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (isChangeImage)
            {
                if (image != null)
                {
                    if (pressUpImage != null)
                    {
                        image.sprite = pressUpImage;
                    }
                }
            }
            else
            {
                if (isLocal)
                {
                    pushDownObject.transform.localPosition = onPointerUpPosition;
                }
                else
                {
                    pushDownObject.transform.position = onPointerUpPosition;
                }
            }
        }
    }
}
