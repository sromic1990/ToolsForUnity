using DG.Tweening;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.Utilities.Scripts.ViewUtils
{
    public class DisappearPop : GameElement, IRemoveAction
    {
        [SerializeField] private Vector3 scale;
        
        public void Remove()
        {
            transform.localScale = scale;
            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InBack);
        }
    }
}