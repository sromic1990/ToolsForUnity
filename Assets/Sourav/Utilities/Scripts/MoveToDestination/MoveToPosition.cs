using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;

namespace Sourav.Utilities.Scripts.MoveToDestination
{
    public class MoveToPosition : MonoBehaviour
    {
        [SerializeField] private GameObject bodyToMove;
        
        private Coroutine cameraMovingCoroutine;
        private Vector3 destinationPosition;
        private float steps;
        private float currentStep;
        private Vector3 startPosition;
        [SerializeField]private bool canMove;
        [SerializeField] [ReadOnly] private float durationOfMovement;
        private bool isPaused;

        public UnityEvent onDestinationReached;
        public Action onMovementComplete;
        private float currentStepOnStop;
        private bool isManual;
        private bool doNotRunEndEvents;

        public void StartMovingToPosition(GameObject bodyToMove, Vector3 positionToMoveTo, float durationOfMovement, Action OnMovementComplete = null)
        {
            this.bodyToMove = bodyToMove;
            destinationPosition = positionToMoveTo;
            this.durationOfMovement = durationOfMovement;
            steps = 0.0f;
            currentStep = 0.0f;
            doNotRunEndEvents = false;
            if (cameraMovingCoroutine != null)
            {
                StopCoroutine(cameraMovingCoroutine);
            }
            steps = Time.unscaledDeltaTime / durationOfMovement;
            // Debug.Log($"steps = {steps}");
            canMove = true;
            isPaused = false;

            if (OnMovementComplete != null)
            {
                onMovementComplete = OnMovementComplete;
            }
            
            cameraMovingCoroutine = StartCoroutine(StartMoving());
        }

        public void PauseMovement()
        {
            isPaused = true;
        }

        public void ResumeMovement()
        {
            isPaused = false;
        }

        public void StopMovement(bool doNotRunEndEvents = false, bool isManual = false)
        {
            this.isManual = isManual;
            this.doNotRunEndEvents = doNotRunEndEvents;
            canMove = false;
            currentStepOnStop = currentStep;
        }

        private IEnumerator StartMoving()
        {
            startPosition = bodyToMove.transform.position;
            while (Vector3.Distance(bodyToMove.transform.position, destinationPosition) > 0.01f && canMove)
            {
                if (isPaused)
                {
                    continue;
                }
                
                Vector3 currentPosition = Vector3.Lerp(startPosition, destinationPosition, currentStep);
                currentStep += steps;
                bodyToMove.transform.position = currentPosition;
                yield return null;
            }

            if (!doNotRunEndEvents)
            {
                onDestinationReached?.Invoke();
                onMovementComplete?.Invoke();
            }

            if (isManual)
            {
                MoveToLastKnownPosition();
            }
        }

        private void MoveToLastKnownPosition()
        {
            bodyToMove.transform.position = Vector3.Lerp(startPosition, destinationPosition, currentStepOnStop);
            isManual = false;
        }
    }
}
