using UnityEngine;

namespace Sourav.Engine.UIPresets.PopUp
{
    public interface IPopUpView
    {
        Transform popUpTransform { get; }
        PopUpType Type { get; }
        void ShowPopUp();
        void HidePopUp();
        void ShowPopUpSilently();
        void HidePopUpSilently();
    }
}