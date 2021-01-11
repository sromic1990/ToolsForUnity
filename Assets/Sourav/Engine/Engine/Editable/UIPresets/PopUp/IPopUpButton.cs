using System.Collections.Generic;

namespace Sourav.Engine.UIPresets.PopUp
{
    public interface IPopUpButton
    {
        void SetUp(PopUpView popUp, List<PopUpButtonActions> actions, System.Action<PopUpButtonActions> onPress);
        void OnClick();
    }
}