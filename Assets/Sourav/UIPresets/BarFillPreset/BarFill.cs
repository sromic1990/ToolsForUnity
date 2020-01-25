using UnityEngine;
using UnityEngine.UI;

namespace Sourav.UIPresets.BarFillPreset
{
    public class BarFill : MonoBehaviour
    {
        [SerializeField] private Text percentageText;
        [SerializeField] private Image barImage;
        [SerializeField] private GameObject movement;
        [SerializeField] private Transform startPoint;
        [SerializeField] private Transform endPoint;

        private int directionOfFill = 1;
        [SerializeField] private int currentValue = 0;

        public void ResetBar(bool isReverse = false)
        {
            if (isReverse)
            {
                directionOfFill = -1;
                ChangeBar(100);
            }
            else
            {
                directionOfFill = 1;
                ChangeBar(0);
            }
        }

        public int GetCurrentValue()
        {
            return currentValue;
        }

        public void IncreaseValue(int value)
        {
            currentValue += (value * directionOfFill);
        }
        
        public void ChangeBar(int percentage)
        {
            if (percentage >= 100)
            {
                percentage = 100;
            }

            if (percentage <= 0)
            {
                percentage = 0;
            }

            float value = (float)percentage / 100.0f;
            barImage.fillAmount = value;
            currentValue = percentage;
            
            if (percentageText != null)
            {
                percentageText.text = percentage + "%";
            }
            if (movement != null && startPoint != null && endPoint != null)
            {
                movement.transform.position = Vector3.Lerp(startPoint.position, endPoint.position, value);
            }
        }

        private void OnValidate()
        {
            ChangeBar(currentValue);
        }
    }
}
