using System.Collections;
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
        private bool canMove;
        private bool isPaused;

        public UnityEvent onDestinationReached;

        public void StartMovingToPosition(Vector3 positionToMoveTo, float durationOfMovement)
        {
            destinationPosition = positionToMoveTo;
            steps = 0.0f;
            currentStep = 0.0f;
            if (cameraMovingCoroutine != null)
            {
                StopCoroutine(cameraMovingCoroutine);
            }
            steps = Time.unscaledDeltaTime / durationOfMovement;
            canMove = true;
            isPaused = false;
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

        public void StopMovement()
        {
            canMove = false;
        }

        private IEnumerator StartMoving()
        {
            startPosition = bodyToMove.transform.position;
            yield return null;
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

            onDestinationReached?.Invoke();
        }
    }
}
