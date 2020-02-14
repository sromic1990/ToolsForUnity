using Sourav.Engine.Core.DebugRelated;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using IdleCurrency = Sourav.IdleGameEngine.IdleCurrency.IdleCurrency.IdleCurrency;

namespace Sourav.Test._IdleCurrencyTest._Scripts
{
    public class TestScript : MonoBehaviour
    {
        [SerializeField] private IdleCurrency unit;
        [SerializeField] private IdleCurrency buttonPrice;
        [SerializeField] private IdleCurrency rateOfChange;
        [SerializeField] private float rateOfPriceIncrease;
        [SerializeField] private float rateOfUnitIncrease;
        [SerializeField] private Text showText;
        [SerializeField] private Button priceButton;
        [SerializeField] private Text buttonText;
        [SerializeField] private Text rateOfChangeText;

        private float secondFraction;

        private void Start()
        {
            secondFraction = 0.0f;
            
            showText.text = unit.ToShortString();
            buttonText.text = buttonPrice.ToShortString();
            rateOfChangeText.text = rateOfChange.ToShortString() + "/sec";
            
            CompareAndHandleButtonActivation(unit, buttonPrice);
        }

        private void Update()
        {
            secondFraction += Time.deltaTime;
            if (secondFraction >= 1.0f)
            {
                SecondElapsed();
                secondFraction = 0.0f;
            }
        }

        private void SecondElapsed()
        {
            unit += rateOfChange;
            showText.text = unit.ToShortString();
            CompareAndHandleButtonActivation(unit, buttonPrice);
        }

        public void ButtonPressed()
        {
            buttonPrice += buttonPrice * rateOfPriceIncrease;
            buttonText.text = buttonPrice.ToShortString();

            rateOfChange += rateOfUnitIncrease;
            rateOfChangeText.text = rateOfChange.ToShortString() + "/sec";
            
            CompareAndHandleButtonActivation(unit, buttonPrice);
        }

        private void CompareAndHandleButtonActivation(IdleCurrency unit, IdleCurrency currentCost)
        {
            int a = IdleCurrency.Compare(unit, currentCost);
            D.Log($"a = {a}");
            
            if (a == -1)
            {
                // D.Log($"unit {unit.ToShortString()} is less than currentCost {currentCost.ToShortString()}");
                priceButton.interactable = false;
            }
            else
            {
                // D.Log($"unit {unit.ToShortString()} is greater than currentCost {currentCost.ToShortString()}");
                priceButton.interactable = true;
            }
        }
    }
}
