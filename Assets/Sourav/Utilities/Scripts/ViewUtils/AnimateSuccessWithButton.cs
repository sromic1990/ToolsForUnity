using DG.Tweening;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;
using UnityEngine.UI;

namespace Sourav.Utilities.Scripts.ViewUtils
{
    [RequireComponent(typeof(Button))]
    public class AnimateSuccessWithButton : GameElement, IAnimate
    {
        // [SerializeField] private GameObject soundPrefab;
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
            transform.localScale = Vector3.one;
            float originalScale = transform.localScale.x;
            float scale = 0.5f * transform.localScale.x;
            transform.localScale = new Vector3(scale, scale, scale);
            transform.DOScale(new Vector3(originalScale, originalScale, originalScale), 0.1f).SetEase(Ease.OutBack);
            
            // App.Notify(Notification.HapticFailure);
        }
    }
}