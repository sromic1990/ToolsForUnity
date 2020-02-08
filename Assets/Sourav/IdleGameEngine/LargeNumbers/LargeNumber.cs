using System;
using System.Collections.Generic;
using System.Globalization;
using Sourav.Engine.Core.DebugRelated;
using Sourav.Utilities.Extensions;
using UnityEngine;

namespace Sourav.IdleGameEngine.LargeNumbers
{
    [System.Serializable]
    public class LargeNumber
    {
        public string numberToBeSetUpAsLargeNumber;
        
        [Space(10)]
        [Header("Representation")]
        public string finalNumber;
        public string finalNumberWithFormat;


        [Space(10)]
        [Header("Lists")]
        [HideInInspector] public List<int> numberBreakDown;
        [HideInInspector] public List<string> numberInString;

        #region SETUP RELATED
        
        #region CONSTRUCTORS
        public LargeNumber() : this(0)
        {}
        public LargeNumber(int number) : this(number.ToString())
        {}
        public LargeNumber(string number)
        {
            SetUpNumber(number);
        }
        public LargeNumber(List<string> numberList)
        {
            SetUpNumberFromList(numberList);
        }
        public LargeNumber(LargeNumber l1)
        {
            SetUpNumberFromList(l1.numberInString);
        }
        #endregion
        
        #region UTILITY TO ENTER A NUMBER FROM INSPECTOR AND SETUP
        [Sirenix.OdinInspector.Button()]
        public void SetUp()
        {
            SetUpNumber(numberToBeSetUpAsLargeNumber);
        }
        #endregion
       
        #region SETUP AND SUPPORTING METHODS
        public void SetUp(int number)
        {
            SetUpNumber(number.ToString());
        }
        
        public void SetUpNumber(string numberInput)
        {
            D.Log("number input = "+numberInput);
            InitializeAllLists();
            #region VERIFY THAT THE INPUT IS A PARSABLE NUMBER

            for (int i = 0; i < numberInput.Length; i++)
            {
                int someNumber = 0;
                string numberInputString = "" + numberInput[i];
                bool isNumberParsable = int.TryParse(numberInputString, out someNumber);

                if (!isNumberParsable)
                {
                    D.LogError("NUMBER IS NOT PARSABLE. FOUND "+numberInputString+" , which is NOT A NUMBER");
                    return;
                }
            }
            #endregion

            ParseNumberIntoFinalNumber(numberInput);
            SetVariousNumberFormats();
            SetFinalNumber();

        }
        
        private void InitializeAllLists()
        {
            numberBreakDown = new List<int>();
            numberInString = new List<string>();
            finalNumber = "";
            finalNumberWithFormat = "";
        }
        
        private void ParseNumberIntoFinalNumber(string numberInput)
        {
//            D.Log("numberInput = "+numberInput);
            
            InitializeAllLists();
            int currentNumberCount = 0;
            
            string numberString = "";
            int numberInt = 0;
            
            for (int i = numberInput.Length - 1; i >= 0; i--)
            {
                int number = 0;
                int.TryParse("" + numberInput[i], out number);

                numberString += "" + numberInput[i];
                numberInt += number * (int)Mathf.Pow(10, currentNumberCount);

                currentNumberCount++;

                if (currentNumberCount == 3)
                {
                    //thousandth place reached
                    numberString = numberString.Reverse();
                    numberInString.Add(numberString);
                    numberBreakDown.Add(numberInt);

                    currentNumberCount = 0;
                    numberInt = 0;
                    numberString = "";
                }
            }

            if (currentNumberCount != 0)
            {
                numberString = numberString.Reverse();
                numberInString.Add(numberString);
                numberBreakDown.Add(numberInt);
                
            }
            numberBreakDown.Reverse();
            numberInString.Reverse();
        }
        
        private void SetVariousNumberFormats()
        {
            string denomination = GetCorrectDenomination(numberInString.Count - 1);
            if (numberBreakDown.Count == 1)
            {
                finalNumberWithFormat = ""+numberBreakDown[0];
            }
            else if (numberBreakDown.Count > (int) Denomination.V)
            {
                string numberString = "";
                int difference = numberBreakDown.Count - (int) Denomination.V;

                for (int i = 0; i < difference; i++)
                {
                    numberString += numberInString[i];
                    if (i < difference - 1)
                    {
                        numberString += ",";
                    }

                    else if (i == difference - 1)
                    {
                        if (numberBreakDown[difference] > 0)
                        {
                            numberString += ".";
                            string numberAfterDecimal = numberInString[difference];
                            for (int j = 0; j < 2; j++)
                            {
                                numberString += "" + numberAfterDecimal[j];
                            }
                        }
                    }
                }

                numberString += Denomination.V;
                finalNumberWithFormat = numberString;

            }
            else
            {
                string tempFinalNumber = numberInString[0];

                if (numberBreakDown[0] < 100)
                {
                    if (numberBreakDown[0] < 10)
                    {
                        if (numberBreakDown[1] < 10)
                        {
                            tempFinalNumber += " " + denomination;
                        }
                        else
                        {
                            string tempSecondNumber = numberInString[1];
                            tempFinalNumber += "." + tempSecondNumber[0] + tempSecondNumber[1] + tempSecondNumber[2] + " " + denomination;
                        }
                    }
                    else
                    {
                        if (numberBreakDown[1] < 10)
                        {
                            tempFinalNumber += " " + denomination;
                        }
                        else
                        {
                            string tempSecondNumber = numberInString[1];
                            tempFinalNumber += "." + tempSecondNumber[0] + tempSecondNumber[1] + " " + denomination;
                        }
                    }
                }
                else
                {
                    if (numberBreakDown[1] < 10)
                    {
                        tempFinalNumber += " " + denomination;
                    }
                    else
                    {
                        string tempSecondNumber = numberInString[1];
                        tempFinalNumber += "." + tempSecondNumber[0] + " " + denomination;
                    }
                }

                finalNumberWithFormat = tempFinalNumber;
            }
        }
        
        private string GetCorrectDenomination(int count)
        {
            if (count > (int)Denomination.V)
            {
                return (Denomination.V).ToString();
            }
            else
            {
                return ((Denomination) count).ToString();
            }
        }
        
        private void SetFinalNumber()
        {
            for (int i = 0; i < numberInString.Count; i++)
            {
                finalNumber += numberInString[i];
                if (i != numberInString.Count - 1)
                {
                    finalNumber += ",";
                }
            }

//            finalNumber = finalNumber.Reverse();
        }
        
        private void SetUpNumberFromList(List<string> numberListString)
        {
            string fullNumber = "";
            for (int i = 0; i < numberListString.Count; i++)
            {
                fullNumber += numberListString[i];
            }
            InitializeAllLists();
            SetUpNumber(fullNumber);
        }
        #endregion
        
        #endregion
        
        #region OPERATIONS RELATED
        
        #region COMPARISION OPERATION
        public Comparision Compare(LargeNumber number2)
        { 
            Comparision compare = Comparision.Equal;
            if (numberBreakDown.Count < number2.numberBreakDown.Count)
            {
                compare = Comparision.Lesser;
            }
            else
            {
                if (numberBreakDown.Count > number2.numberBreakDown.Count)
                {
                    compare = Comparision.Greater;
                }
                else //equal
                {
                    if (numberBreakDown[0] < number2.numberBreakDown[0])
                    {
                        compare = Comparision.Lesser;;
                    }
                    else
                    {

                        for (int i = 0; i < numberBreakDown.Count; i++)
                        {
                            if (numberBreakDown[i] < number2.numberBreakDown[i])
                            {
                                compare = Comparision.Lesser;;
                                break;
                            }
                            else if(numberBreakDown[i] > number2.numberBreakDown[i])
                            {
                                compare = Comparision.Greater;
                                break;
                            }
                        }
                    }
                }
            }

            return compare;
        }
        #endregion
        
        #region ARITHMATIC OPERATIONS
        public static LargeNumber operator +(LargeNumber l1, LargeNumber l2)
        {
            LargeNumber largerNumber = new LargeNumber();
            LargeNumber smallerNumber = new LargeNumber();
            int counter = 0;
            
//            D.Log("l1 compare l2 = "+l1.Compare(l2));
            
            if (l1.Compare(l2) == Comparision.Lesser)
            {
                counter = l1.numberBreakDown.Count;
                largerNumber.SetUpNumberFromList(l2.numberInString);
                smallerNumber.SetUpNumberFromList(l1.numberInString);
            }
            else if(l1.Compare(l2) == Comparision.Greater)
            {
                counter = l2.numberBreakDown.Count;
                largerNumber.SetUpNumberFromList(l1.numberInString);
                smallerNumber.SetUpNumberFromList(l2.numberInString);
            }
            else
            {
                counter = l1.numberBreakDown.Count;
                largerNumber.SetUpNumberFromList(l2.numberInString);
                smallerNumber.SetUpNumberFromList(l1.numberInString);
            }

            int carryForward = 0;

            largerNumber.numberBreakDown.Reverse();
            smallerNumber.numberBreakDown.Reverse();

            for (int i = 0; i < counter; i++)
            {
                int resultOfAddition = largerNumber.numberBreakDown[i] + smallerNumber.numberBreakDown[i] + carryForward;
                int resultToPut = resultOfAddition % 1000;
                carryForward = resultOfAddition / 1000;
                largerNumber.numberBreakDown[i] = resultToPut;
                
                
                string resultToPutString = resultToPut.ToString();
                
                if (resultToPut < 10)
                {
                    resultToPutString = "00" + resultToPutString;
                }
                else if (resultToPut < 100)
                {
                    resultToPutString = "0" + resultToPut;
                }
                largerNumber.numberInString[i] = resultToPutString;
            }

            if (largerNumber.numberBreakDown.Count > counter)
            {
                for (int i = counter; i < largerNumber.numberBreakDown.Count; i++)
                {
                    int resultOfAddition = largerNumber.numberBreakDown[i] + carryForward;
                    int resultToPut = resultOfAddition % 1000;
                    carryForward = resultOfAddition / 1000;
                    largerNumber.numberBreakDown[i] = resultToPut;
                    
                    string resultToPutString = resultToPut.ToString();
                
                    if (resultToPut < 10)
                    {
                        resultToPutString = "00" + resultToPutString;
                    }
                    else if (resultToPut < 100)
                    {
                        resultToPutString = "0" + resultToPut;
                    }
                    largerNumber.numberInString[i] = resultToPutString;
                }
            }

            if (carryForward > 0)
            {
                largerNumber.numberBreakDown.Add(carryForward);
                largerNumber.numberInString.Add("" + carryForward);
            }
            
            largerNumber.numberInString.Reverse();
            largerNumber.numberBreakDown.Reverse();

            largerNumber.numberInString[0] = ""+largerNumber.numberBreakDown[0];
            
            largerNumber.SetUpNumberFromList(largerNumber.numberInString);

            return largerNumber;
        }
        public static LargeNumber operator ++(LargeNumber l)
        {
            LargeNumber l1 = new LargeNumber(1);

            l = l + l1;

            return l;
        }

        public static LargeNumber operator -(LargeNumber l1, LargeNumber l2)
        {
            LargeNumber resultNumber = new LargeNumber();

            if (l1.Compare(l2) == Comparision.Lesser || l1.Compare(l2) == Comparision.Equal)
            {
                resultNumber.SetUpNumber("0");
            }
            else
            {
                LargeNumber largerNumber = new LargeNumber(l1.numberInString);
                LargeNumber smallerNumber = new LargeNumber(l2.numberInString);

                largerNumber.numberBreakDown.Reverse();
                smallerNumber.numberBreakDown.Reverse();

                List<string> resultOfSubtractionString = new List<string>();
                int carryForwardSubtraction = 0;
                int resultOfSubtraction = 0;
                
                for (int i = 0; i < smallerNumber.numberBreakDown.Count; i++)
                {
                    resultOfSubtraction = largerNumber.numberBreakDown[i] - smallerNumber.numberBreakDown[i] - carryForwardSubtraction;
//                    D.Log("l1 = "+largerNumber.numberBreakDown[i]+" , l2 = "+smallerNumber.numberBreakDown[i]+" , carryforward = "+carryForwardSubtraction);
                    if (resultOfSubtraction < 0)
                    {
                        resultOfSubtraction *= -1;
                        
                        carryForwardSubtraction = 1;
                        
                        resultOfSubtraction = 1000 - resultOfSubtraction;
                    }
                    else
                    {
                        carryForwardSubtraction = 0;
                    }
//                    D.Log("Carry forward = "+carryForwardSubtraction);
//                    D.Log("result of subtraction = "+resultOfSubtraction);

                    string resultString = "" + resultOfSubtraction;
                    if (resultOfSubtraction < 10)
                    {
                        resultString = "00" + resultString;
                    }
                    else if (resultOfSubtraction < 100)
                    {
                        resultString = "0" + resultString;
                    }
                    resultOfSubtractionString.Add(resultString);
                }

                for (int i = smallerNumber.numberBreakDown.Count; i < largerNumber.numberBreakDown.Count; i++)
                {
                    resultOfSubtraction = largerNumber.numberBreakDown[i] - carryForwardSubtraction;
                    if (resultOfSubtraction < 0)
                    {
                        resultOfSubtraction *= -1;
                        
                        carryForwardSubtraction = 1;
                        
                        resultOfSubtraction = 1000 - resultOfSubtraction;
                    }
                    else
                    {
                        carryForwardSubtraction = 0;
                    }

                    string resultString = "" + resultOfSubtraction;
                    if (resultOfSubtraction < 10)
                    {
                        resultString = "00" + resultString;
                    }
                    else if (resultOfSubtraction < 100)
                    {
                        resultString = "0" + resultString;
                    }
                    resultOfSubtractionString.Add(resultString);
                }
                
                resultOfSubtractionString.Reverse();
                resultOfSubtractionString[0] = (Mathf.Max(0, resultOfSubtraction)).ToString();

                int indexAfter0 = -1;
                for (int i = 0; i < resultOfSubtractionString.Count; i++)
                {
                    indexAfter0++;
                    if (resultOfSubtractionString[indexAfter0] != "0" &&
                        resultOfSubtractionString[indexAfter0] != "000")
                    {
                        break;
                    }
                }
//                D.Log("indexAfter0 = "+indexAfter0);
                List<string> rectifiedResultOfSubtractionString = new List<string>();
                for (int i = indexAfter0; i < resultOfSubtractionString.Count; i++)
                {
                    rectifiedResultOfSubtractionString.Add(resultOfSubtractionString[i]);
                }

                resultOfSubtractionString = rectifiedResultOfSubtractionString;

                string firstValue = resultOfSubtractionString[0];
                int index = -1;
                for (int i = 0; i < firstValue.Length; i++)
                {
                    if (firstValue[i] == '0')
                    {
                        index++;
                    }
                    else
                    {
                        break;
                    }
                }
//                D.Log("index = "+index);

                if (index >= 0)
                {
                    string rectifiedFirstValue = "";
                    for (int i = index + 1; i < firstValue.Length; i++)
                    {
                        rectifiedFirstValue += firstValue[i];
                    }
                    resultOfSubtractionString[0] = rectifiedFirstValue;
                }
                
                resultNumber.SetUpNumberFromList(resultOfSubtractionString);
            }

            return resultNumber;
        }
        public static LargeNumber operator --(LargeNumber l)
        {
            LargeNumber l1 = new LargeNumber(1);

            l = l - l1;

            return l;
        }

//        public static LargeNumber operator *(LargeNumber l1, LargeNumber l2)
//        {
//            LargeNumber resultNumber = new LargeNumber();
//            resultNumber.SetUpNumberFromList(l1.numberInString);
//
//            LargeNumber comparisonNumber = new LargeNumber(0);
//
//            if (l2.Compare(comparisonNumber) == Comparision.Equal || l1.Compare(comparisonNumber) == Comparision.Equal)
//            {
//                resultNumber.SetUpNumber("0");
//            }
//            else
//            {
//                while (comparisonNumber.Compare(l2) != Comparision.Equal)
//                {
//                    l2--;
//                    resultNumber = resultNumber + l1;
////                    D.Log("answer = "+resultNumber.finalNumberWithFormat);
//                }
//
//                resultNumber = resultNumber - l1;
//            }
//            return resultNumber;
//        }
//
//        public static LargeNumber operator *(LargeNumber l1, float l2)
//        {
////            D.Log("CALLED");
//            LargeNumber l = new LargeNumber(l1);
//
//            if (l2 % 1 == 0)
//            {
//                LargeNumber multiplier = new LargeNumber((int)l2);
//                l = l * multiplier;
//            }
//            else
//            {
//                int beforeDecimal = (int) l2;
//                float afterDecimal = (l2 % 1);
//                string numberDecimal = l2.ToString(CultureInfo.InvariantCulture);
//                int length = numberDecimal.Substring(numberDecimal.IndexOf(".", StringComparison.Ordinal)+1).Length;
//                afterDecimal = afterDecimal * Mathf.Pow(10, length);
//                int afterDecimalInt = (int) afterDecimal;
//
//                int numberOfDecimalPlaces = (int)Mathf.Pow(10, length);
//
//                LargeNumber resultAfterDecimal = l1 * new LargeNumber(afterDecimalInt);
//                LargeNumber powerOfTen = new LargeNumber(numberOfDecimalPlaces);
//                resultAfterDecimal = resultAfterDecimal / powerOfTen;
////                D.Log("resultAfterDecimal = "+resultAfterDecimal.finalNumberWithFormat);
//
//                LargeNumber result = new LargeNumber();
//                result = l1 * new LargeNumber(beforeDecimal);
//                LargeNumber decimalResult = new LargeNumber(resultAfterDecimal);
//                l = result + decimalResult;
//            }
//            
//            return l;
//        }

        public static LargeNumber operator*(LargeNumber l1, LargeNumber l2)
        {
            LargeNumber result = new LargeNumber();
            
            LargeNumber number1 = new LargeNumber();
            LargeNumber number2 = new LargeNumber();

            if (l1.Compare(l2) == Comparision.Greater)
            {
                number1.SetUpNumberFromList(l1.numberInString);
                number2.SetUpNumberFromList(l2.numberInString);
            }
            else
            {
                number1.SetUpNumberFromList(l2.numberInString);
                number2.SetUpNumberFromList(l1.numberInString);
            }

            int count = number2.numberInString.Count - 1;
            List<string> resultNumbers = new List<string>();
            string insertZero = "";
            while (count >= 0)
            {
                string multiplier = number2.numberInString[count];
                int multiplierLength = multiplier.Length - 1;
                int multiplierInt = 0;
                while (multiplierLength >= 0)
                {
                    int resultInt = 0;
                    int carryForward = 0;
                    string resultString = "";
                    
                    int.TryParse("" + multiplier[multiplierLength], out multiplierInt);
                    for (int i = number1.numberInString.Count - 1; i >= 0; i--)
                    {
                        string multiplicand = number1.numberInString[i];
                        int l1Count = multiplicand.Length - 1;
                        int multiplicandInt = 0;

                        while (l1Count >= 0)
                        {
                            int.TryParse(""+multiplicand[l1Count], out multiplicandInt);
                            resultInt = (multiplicandInt * multiplierInt) + carryForward;
                            carryForward = resultInt / 10;
                            resultInt = resultInt % 10;
                            resultString += resultInt.ToString();
                            l1Count--;
                        }

                        if (i == 0)
                        {
                            if (carryForward > 0)
                            {
                                resultString += carryForward;
                            }
                        }
                    }

//                    D.Log("resultString = "+resultString);
                    resultString = resultString.Reverse();
//                    D.Log("resultString = "+resultString);
                    resultString += insertZero;
                    insertZero += "0";
                    resultNumbers.Add(resultString);
                    
                    multiplierLength--;
                }
                count--;
            }

            result = new LargeNumber(resultNumbers[0]);
            for (int i = 1; i < resultNumbers.Count; i++)
            {
                result += new LargeNumber(resultNumbers[i]);
            }
            
            return result;
        }
        
        public static LargeNumber operator *(LargeNumber l1, float l2)
        {
            LargeNumber l = new LargeNumber(l1);

            if (l2 % 1 == 0)
            {
                LargeNumber multiplier = new LargeNumber((int)l2);
                l = l * multiplier;
            }
            else
            {
                int beforeDecimal = (int) l2;
//                D.Log("BEFORE DECIMAL = "+beforeDecimal);
                float afterDecimal = (l2 % 1);
//                D.Log("AFTER DECIMAL = "+afterDecimal);
                string numberDecimal = l2.ToString(CultureInfo.InvariantCulture);
                int lengthOfDecimalNumbers = numberDecimal.Substring(numberDecimal.IndexOf(".", StringComparison.Ordinal)+1).Length;
                afterDecimal = afterDecimal * Mathf.Pow(10, lengthOfDecimalNumbers);
                int afterDecimalInt = (int) afterDecimal;
                string number = "" + beforeDecimal + afterDecimalInt;
//                D.Log("Number without decimal = "+number);

                l = l * new LargeNumber(number);
                string numberAfterMultipliaction = "";

                for (int i = l.numberInString.Count - 1; i >= 0; i--)
                {
                    string currentNumber = l.numberInString[i];
                    int index = currentNumber.Length - 1;
                    while (index >= 0)
                    {
                        numberAfterMultipliaction += "" + currentNumber[index];
                        index--;
                    }
                }

                if (numberAfterMultipliaction.Length > lengthOfDecimalNumbers)
                {
//                    D.Log("numberAfterMultipliaction = "+numberAfterMultipliaction);
                    string numberAfterTruncation = numberAfterMultipliaction.Substring(lengthOfDecimalNumbers);
                    numberAfterTruncation = numberAfterTruncation.Reverse();
//                    D.Log("Number after truncation = "+numberAfterTruncation);
                    l.SetUpNumber(numberAfterTruncation);
                }
                else
                {
                    l = new LargeNumber();
                }
            }
            
            return l;
        }
        
        public static LargeNumber operator /(LargeNumber l1, LargeNumber l2)
        {
            LargeNumber resultNumber = new LargeNumber();

            LargeNumber zeroNumber = new LargeNumber();
            
            if (l2.Compare(zeroNumber) == Comparision.Equal)
            {
                D.LogError("Attempted division by zero");
                throw new System.DivideByZeroException();
            }
            else
            {
                if (l2.Compare(l1) == Comparision.Greater)
                {
                    resultNumber.SetUpNumber("0");
                }
                else if (l2.Compare(l1) == Comparision.Equal)
                {
                    resultNumber.SetUpNumber("1");
                }
                else
                {
                    if (l1.numberBreakDown.Count > l2.numberBreakDown.Count + 1)
                    {
                        List<string> l1Abbreviated = new List<string>();
                        for (int i = 0; i <= l2.numberBreakDown.Count; i++)
                        {
                            l1Abbreviated.Add(l1.numberInString[i]);
                        }
                        LargeNumber l1Abb = new LargeNumber();
                        l1Abb.SetUpNumberFromList(l1Abbreviated);
                        LargeNumber result = new LargeNumber();

                        while (l1Abb.Compare(zeroNumber) != Comparision.Equal)
                        {
                            l1Abb = l1Abb - l2;
                            result++;
                            //D.Log("result = "+result.finalNumberWithFormat);
                        }
                        for (int i = l2.numberBreakDown.Count + 1; i < l1.numberBreakDown.Count; i++)
                        {
                            result.numberInString.Add("000");
                        }
                        resultNumber.SetUpNumberFromList(result.numberInString);                        
                        
                    }
                    else
                    {
//                        D.Log("l1 = "+l1.finalNumberWithFormat+" , l2 = "+l2.finalNumberWithFormat);
                        while (l1.Compare(zeroNumber) != Comparision.Equal && (l1.Compare(l2) == Comparision.Greater || l1.Compare(l2) == Comparision.Equal))
                        {
                            l1 = l1 - l2;
                            resultNumber++;
                        }
                    }
                    
                }
            }
            return resultNumber;
        }

        #endregion
        
        #region PERCENTAGE OPERATION
        public static LargeNumber operator %(LargeNumber number, float percentage)
        {
            LargeNumber percentNumber = new LargeNumber();
            if (percentage % 1 == 0)
            {
                LargeNumber percent = new LargeNumber((int)percentage);
                LargeNumber number100 = new LargeNumber(100);

                percentNumber = number * percent;
                percentNumber = percentNumber / number100;
                
            }
            else
            {
                int integerPart = (int) percentage;
                LargeNumber part1 = new LargeNumber();
                part1 = number % integerPart;
//                D.Log("part 1 = "+part1.finalNumberWithFormat);
                
                float value = percentage % 1;
                string numberDecimal = value.ToString(CultureInfo.InvariantCulture);
                int length = numberDecimal.Substring(numberDecimal.IndexOf(".", StringComparison.Ordinal)+1).Length;
//                D.Log("percentage % 1 = "+percentage % 1);
//                D.Log("Length = "+length);

                int multiplier = 1;
                for (int i = 0; i < length; i++)
                {
                    multiplier *= 10;
                }

                value *= multiplier;
                int percentageValue = (int) value;
//                D.Log("integer of decimal part = "+percentageValue);
                LargeNumber part2 = new LargeNumber();
                part2 = number % percentageValue;
//                D.Log("part 2 = "+part2.finalNumberWithFormat);
                LargeNumber multiplierLargeNumber = new LargeNumber(multiplier);
                part2 = part2 / multiplierLargeNumber;

                percentNumber = part1 + part2;
            }
            return percentNumber;
        }
        #endregion
        
        #region UTILITY OPERATIONS

        public void Concatenate(LargeNumber l2)
        {
            string l2InString = "";

            for (int i = 0; i < l2.numberInString.Count; i++)
            {
                l2InString += l2.numberInString[i];
            }
//            D.Log("l2InString = "+l2InString);

            string currentNumber = "";
            for (int i = 0; i < numberInString.Count; i++)
            {
                currentNumber += numberInString[i];
            }

//            D.Log("Current number = "+currentNumber);

            currentNumber += l2InString;
//            D.Log("Current number = "+currentNumber);
            SetUpNumber(currentNumber);
        }
        #endregion
        
        #endregion
    }

    public enum Denomination : int
    {
        K = 1, //thousand
        M = 2, //million
        B = 3, //billion
        T = 4, //trillion
        Q = 5, //Quadrillion
        QI = 6, //Quintillion
        S = 7, //Sextillion
        SP = 8, //Septillion
        OC = 9, //Octillion
        N = 10, //Nonillion
        D = 11, //Decillion
        UD = 12, //Undecillion
        DD = 13, //Duodecillion
        TD = 14, //Tredecillion
        QT = 15, //Quattuordecillion
        QD = 16, //Quindecillion
        SD = 17, //Sexdecillion
        SPD = 18, //Septendecillion
        OCD = 19, //Octodecillion
        NVD = 20, //Novemdecillion
        V = 21, //Vigintillion
    }

    public enum Comparision
    {
        Greater,
        Lesser,
        Equal
    }
}
