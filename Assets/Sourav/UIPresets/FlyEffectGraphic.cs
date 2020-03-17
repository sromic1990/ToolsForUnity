using DG.Tweening;
using Sourav.Engine.Core.GameElementRelated;
using Sourav.Utilities.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace _IdleWorkout._Scripts.ViewRelated
{
    public class FlyEffectGraphic : GameElement
    {
        [SerializeField] private Transform holderTransform;
        [SerializeField] private FlyObject[] objects;

        public void StartObjectsToFly()
        {
            for (int i = 0; i < objects.Length; i++)
            {
                GameObject gObj = Instantiate(objects[i].flyObject.gameObject, holderTransform);
                gObj.Show();
                gObj.transform.position = objects[i].startPosition.position;
                Graphic g = gObj.GetComponent<Graphic>();
                gObj.transform.DOMove(objects[i].endPosition.position, objects[i].durationOfFlight)
                    .SetEase(objects[i].flightEaseMovement);
                g.DOFade(0, objects[i].durationOfFade).SetEase(objects[i].flightEaseFade)
                    .SetDelay(objects[i].delayBeforeFading).OnComplete(() =>
                    {
                        Destroy(g.gameObject);
                    });
            }
        }
    }

    [System.Serializable]
    public class FlyObject
    {
        public Graphic flyObject;
        public Transform startPosition;
        public Transform endPosition;
        public float durationOfFlight;
        public float durationOfFade;
        public float delayBeforeFading;
        public Ease flightEaseMovement;
        public Ease flightEaseFade;
    }
}
