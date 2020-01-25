using DG.Tweening;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Utilities.Extensions;
using UnityEngine;

namespace Sourav.UIPresets.CommonSettings.Scripts
{
    public class SettingsView : GameElement
    {
        [SerializeField] private GameObject settingsClose;
        [SerializeField] private GameObject bgShow;
        [SerializeField] private GameObject panel;
        [SerializeField] private float durationOfShow;
        [SerializeField] private Ease ease;
        

        public void ShowSettings()
        {
            // DOTween.Restart(bgShow, "out");
            panel.Show();
            bgShow.transform.DOScale(new Vector3(1, 1, 1), durationOfShow).SetEase(ease).OnComplete(SettingsShown);
        }
        public void SettingsShown()
        {
            settingsClose.Show();
        }

        public void HideSettings()
        {
            settingsClose.Hide();
            bgShow.transform.localScale = new Vector3(1, 0, 1);
            panel.Hide();
        }
    }
}
