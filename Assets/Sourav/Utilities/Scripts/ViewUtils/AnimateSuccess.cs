using DG.Tweening;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.Utilities.Scripts.ViewUtils
{
    public class AnimateSuccess : GameElement, IAnimate
    {
        public void Animate(Transform transform)
        {
            float originalScale = transform.localScale.x;
            float scale = 0.5f * transform.localScale.x;
            transform.localScale = new Vector3(scale, scale, scale);
            transform.DOScale(new Vector3(originalScale, originalScale, originalScale), 0.1f).SetEase(Ease.OutBack);
        }
    }
}