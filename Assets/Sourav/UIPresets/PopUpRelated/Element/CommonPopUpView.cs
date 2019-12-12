using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;
using UnityEngine.UI;

namespace _IdleArtistDots._Scripts.View.PopUpRelated.Element
{
    public class CommonPopUpView : GameElement
    {
        [SerializeField] private Text header;
        [SerializeField] private Text secondaryText;
        [SerializeField] private Image mainImage;
        [SerializeField] private Text mainText;

        public void SetUp(string header, string secondaryText, Sprite image, string mainText)
        {
            this.header.text = header;
            this.secondaryText.text = secondaryText;
            mainImage.sprite = image;
            this.mainText.text = mainText;
        }
    }
}
