using DG.Tweening;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;
using UnityEngine.UI;

namespace Sourav.Utilities.Scripts.ViewUtils
{
    [RequireComponent(typeof(Button))]
    public class AnimateSuccessWithButtonAndOptions : GameElement, IAnimate
    {
        [SerializeField] private float scaleDownMultiplier;
        [SerializeField] private float timeDuration;
        
        private void Start()
        {
            GetComponent<Button>().onClick.AddListener(AnimateSelf);
        }
        public void AnimateSelf()
        {
            Animate(transform);
        }
        public void Animate(Transform transform)
        {
            float originalScale = transform.localScale.x;
            float scale = scaleDownMultiplier * transform.localScale.x;
            transform.localScale = new Vector3(scale, scale, scale);
            transform.DOScale(new Vector3(originalScale, originalScale, originalScale), timeDuration).SetEase(Ease.OutBack);
            
            // App.Notify(Notification.HapticFailure);
        }
    }
}