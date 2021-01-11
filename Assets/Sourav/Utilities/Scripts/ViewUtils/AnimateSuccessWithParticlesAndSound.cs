using DG.Tweening;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Engine.Editable.DataRelated;
using Sourav.Engine.Editable.NotificationRelated;
using Sourav.Engine.Engine.Core.ApplicationRelated;
using UnityEngine;

namespace Sourav.Utilities.Scripts.ViewUtils
{
    public class AnimateSuccessWithParticlesAndSound : GameElement, IAnimate
    {
        [SerializeField] private GameObject _particlePrefab;
        [SerializeField] private GameObject soundPrefab;
        
        public void Animate(Transform transform)
        {
            float originalScale = 1.0f;
            float smallScale = 0.5f * originalScale;
            transform.localScale = new Vector3(smallScale, smallScale, smallScale);
            transform.DOScale(new Vector3(originalScale, originalScale, originalScale), 0.1f);
            GameObject gObj = Instantiate(_particlePrefab, transform);
            Destroy(gObj, 2f);
            if (Sourav.Engine.Engine.Core.ApplicationRelated.App.GetData<LevelCommonData>().IsSfxOn)
            {
                GameObject gObjSound = Instantiate(soundPrefab, transform);
                Destroy(gObjSound, 2f);
            }
            Sourav.Engine.Engine.Core.ApplicationRelated.App.Notify(Notification.HapticFailure);
        }
    }
}
