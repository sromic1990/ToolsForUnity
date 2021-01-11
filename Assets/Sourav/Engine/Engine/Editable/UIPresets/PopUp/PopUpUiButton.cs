using System.Collections.Generic;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Utilities.Scripts.ViewUtils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Sourav.Engine.UIPresets.PopUp
{
    [RequireComponent(typeof(Button))]
    public class PopUpUiButton : GameElement, IPopUpButton 
    {
        private PopUpView _popUp;
        private System.Action<PopUpButtonActions> _onPress;
        private IAnimate _animate;
        private List<PopUpButtonActions> _actionsForThisButton;

        private Button _button;

        private void Start()
        {
            _animate = GetComponent<IAnimate>();
            _button = GetComponent<Button>();
            _button.onClick.AddListener(() => { OnClick();});
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