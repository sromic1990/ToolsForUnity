using DG.Tweening;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.Utilities.Scripts.ViewUtils
{
    public class AppearPop : GameElement, IAppearAction
    {
        [SerializeField] private Vector3 scale;
        
        public void Appear()
        {
            transform.localScale = Vector3.zero;
            transform.DOScale(scale, 0.5f).SetEase(Ease.OutBack);
        }
    }
}