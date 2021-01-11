using System.Collections.Generic;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Utilities.Scripts.ViewUtils;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Sourav.Engine.UIPresets.PopUp
{
    [RequireComponent(typeof(IAnimate))]
    public class PopUpButton : GameElement, IPointerClickHandler, IPopUpButton
    {
        private PopUpView _popUp;
        private System.Action<PopUpButtonActions> _onPress;
        private IAnimate _animate;
        private List<PopUpButtonActions> _actionsForThisButton;

        private void Start()
        {
            _animate = GetComponent<IAnimate>();
        }
        
        public void SetUp(PopUpView popUp, List<PopUpButtonActions> actions, System.Action<PopUpButtonActions> onPress)
        {
            _popUp = popUp;
            _actionsForThisButton = new List<PopUpButtonActions>();
            for (int i = 0; i < actions.Count; i++)
            {
                _actionsForThisButton.Add(actions[i]);
            }
            _onPress = onPress;
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            OnClick();
        }

        public void OnClick()
        {
            if(!_popUp.IsPopUpReady)
                return;
            
            _animate.Animate(transform);
            for (int i = 0; i < _actionsForThisButton.Count; i++)
            {
                _onPress?.Invoke(_actionsForThisButton[i]);   
            }
        }
    }
}