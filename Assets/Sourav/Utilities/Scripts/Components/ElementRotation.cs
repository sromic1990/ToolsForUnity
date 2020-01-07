using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.Utilities.Scripts.Components
{
    public class ElementRotation : GameElement
    {
        [SerializeField] private Transform pivot;
        [SerializeField] private float startAngle;
        [SerializeField] private float endAngle;
        [SerializeField] private Vector2 minMaxRandomSpeed;
        [SerializeField] private List<float> speedsList;
        [SerializeField] private bool useSpeedList;
        private int lastIndexOfSpeedList;
        [SerializeField] private Axis axisOfRotation;
        [SerializeField] private Vector2 minMaxWaitBeforeNextMovement;
        [SerializeField] private float delayBeforeNext;
        [SerializeField] private bool isDelayFront;
        [SerializeField] private bool isDelayRear;
        
        [SerializeField][ReadOnly] private float currentSpeed;
        [SerializeField][ReadOnly] private float currentAngle;
        [SerializeField][ReadOnly] private float nextAngle;
        

        [SerializeField][ReadOnly] private bool rotationManuallyStopped;
        [SerializeField][ReadOnly] private int currentDirectionMultiplier;
        [SerializeField][ReadOnly] private bool startToFinish;
        
        private bool canRotate;
        private bool isPaused;
        private Coroutine rotationCoroutine;
        private bool isDifferenceNegative;

        public void SetUp(Vector2 minMaxRandomSpeed, Vector2 minMaxWaitBeforeNextMovement, bool delayBeforeFrontLoop, bool delayBeforeRearLoop)
        {
            ResetRotation();

            this.minMaxRandomSpeed = minMaxRandomSpeed;
            useSpeedList = false;
            
            SetUpMinMaxWait(minMaxWaitBeforeNextMovement, delayBeforeFrontLoop, delayBeforeRearLoop);
        }

        public void ResetRotation()
        {
            Vector3 startRotation = new Vector3(
                (axisOfRotation == Axis.X) ? startAngle : pivot.rotation.eulerAngles.x,
                (axisOfRotation == Axis.Y) ? startAngle : pivot.rotation.eulerAngles.y,
                (axisOfRotation == Axis.Z) ? startAngle : pivot.rotation.eulerAngles.z
            );
            pivot.transform.rotation = Quaternion.Euler(startRotation);
        }

        public void SetUp(List<float> minMaxRandomSpeed, Vector2 minMaxWaitBeforeNextMovement, bool delayBeforeFrontLoop, bool delayBeforeRearLoop)
        {
            speedsList = new List<float>();
            for (int i = 0; i < minMaxRandomSpeed.Count; i++)
            {
                speedsList.Add(minMaxRandomSpeed[i]);
            }

            useSpeedList = true;

            SetUpMinMaxWait(minMaxWaitBeforeNextMovement, delayBeforeFrontLoop, delayBeforeRearLoop);
        }
        
        private void SetUpMinMaxWait(Vector2 minMaxDelay, bool isFrontDelay, bool isRearDelay)
        {
            this.minMaxWaitBeforeNextMovement = minMaxDelay;
            isDelayFront = isFrontDelay;
            isDelayRear = isRearDelay;
        }
        
        [Button()]
        public void StartRotation()
        {
            if (isDelayFront)
            {
                StartCoroutine(WaitBeforeStart(delayBeforeNext));
            }
            else
            {
                StartCoroutine(WaitBeforeStart(0));
            }
        }

        private IEnumerator WaitBeforeStart(float delay)
        {
            yield return new WaitForSeconds(delay);
            rotationManuallyStopped = false;
            currentAngle = startAngle;
            nextAngle = endAngle;
            startToFinish = true;
            RenewRotation();
        }

        [Button()]
        public void StopRotation()
        {
            rotationManuallyStopped = true;
            canRotate = false;
            Time.timeScale = 1;
        }

        [Button()]
        public void PauseRotation()
        {
            Time.timeScale = 0;
            isPaused = true;
        }
        
        [Button()]
        public void ResumeRotation()
        {
            Time.timeScale = 1;
            isPaused = false;
        }

        private void OnRotationEnd()
        {
            if (isDelayRear)
            {
                StartCoroutine(WaitBeforeRestart(delayBeforeNext));
            }
            else
            {
                StartCoroutine(WaitBeforeRestart(0));
            }
            
        }

        private IEnumerator WaitBeforeRestart(float delay)
        {
            yield return new WaitForSecondsRealtime(delay);
            currentAngle = endAngle;
            nextAngle = startAngle;
            startToFinish = false;
            RenewRotation();
        }

        private void RenewRotation()
        {
            DetermineSpeed();
            DetermineNextAngleStopper();
            DetermineNextDelay();
            
            Vector3 startRotation = new Vector3(
                (axisOfRotation == Axis.X) ? currentAngle : pivot.rotation.eulerAngles.x,
                (axisOfRotation == Axis.Y) ? currentAngle : pivot.rotation.eulerAngles.y,
                (axisOfRotation == Axis.Z) ? currentAngle : pivot.rotation.eulerAngles.z
            );

            pivot.transform.rotation = Quaternion.Euler(startRotation);

            if (nextAngle > currentAngle)
            {
                currentDirectionMultiplier = 1;
            }
            else
            {
                currentDirectionMultiplier = -1;
            }

            ResumeRotation();
            canRotate = true;

            if (rotationCoroutine != null)
            {
                StopCoroutine(rotationCoroutine);
            }

            rotationCoroutine = StartCoroutine(RotationCoroutine());
        }

        private void DetermineNextDelay()
        {
            delayBeforeNext = Random.Range(minMaxWaitBeforeNextMovement.x, minMaxWaitBeforeNextMovement.y);
        }

        private void DetermineNextAngleStopper()
        {
            if (currentAngle > nextAngle)
            {
                isDifferenceNegative = false;
            }
            else
            {
                isDifferenceNegative = true;
            }
        }

        private void DetermineSpeed()
        {
            if (!useSpeedList || speedsList == null || speedsList.Count == 0)
            {
                currentSpeed = Random.Range(minMaxRandomSpeed.x, minMaxRandomSpeed.y);
            }
            else
            {
                int index = lastIndexOfSpeedList;
                do
                {
                    index = Random.Range(0, speedsList.Count);
                } 
                while (index == lastIndexOfSpeedList);

                lastIndexOfSpeedList = index;

                currentSpeed = speedsList[lastIndexOfSpeedList];
            }
        }

        private IEnumerator RotationCoroutine()
        {
            yield return null;
            while (canRotate)
            {
                if (isPaused)
                {
                    yield return null;
                }
                
                currentAngle += (Time.unscaledDeltaTime * currentSpeed * currentDirectionMultiplier);
                Vector3 rotation = new Vector3(
                    (axisOfRotation == Axis.X) ? currentAngle : pivot.rotation.eulerAngles.x,
                    (axisOfRotation == Axis.Y) ? currentAngle : pivot.rotation.eulerAngles.y,
                    (axisOfRotation == Axis.Z) ? currentAngle : pivot.rotation.eulerAngles.z
                    );
                pivot.transform.rotation = Quaternion.Euler(rotation);

                if (isDifferenceNegative)
                {
                    if (currentAngle > nextAngle)
                    {
                        canRotate = false;
                    }
                }
                else
                {
                    if (currentAngle < nextAngle)
                    {
                        canRotate = false;
                    }
                }

                if (canRotate)
                {
                    yield return null;
                }
            }

            if (!rotationManuallyStopped)
            {
                // Debug.Log($"rotationManuallyStopped = {rotationManuallyStopped}");
                if (!startToFinish)
                {
                    StartRotation();
                }
                else
                {
                    OnRotationEnd();
                }
            }
        }
    }
}
