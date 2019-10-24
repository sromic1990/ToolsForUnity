using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sourav.Engine.Core.GameElementRelated;
using UnityEngine;

namespace Sourav.Utilities.Scripts.RandomColors
{
    public class RandomizeColor : GameElement
    {
        private static List<Color> colors;

        [Button()]
        public static List<Color> GetColors(int numberOfColors)
        {
            colors = new List<Color>(numberOfColors);

            SetUpColors(numberOfColors);

            return colors;
        }
        
        private static void SetUpColors(int numberOfColors)
        {
            List<float> hues = GetRandom(numberOfColors, 0.1f, 0.1f, 1.0f);

            for (int i = 0; i < numberOfColors; i++)
            {
                Color color = Color.HSVToRGB(hues[i], Random.Range(0.7f, 1f), 1.0f);
                colors.Add(color);
            }

        }

        private static List<float> GetRandom(int itemsNumber, float difference, float rangeMin, float rangeMax)
        {
            List<float> random = new List<float>(itemsNumber);

            SetUpRandomList(itemsNumber, difference, rangeMin, rangeMax, random);

            return random;
        }
        
        private static void SetUpRandomList(int itemsNumber1, float difference, float rangeMin1, float rangeMax1, List<float> floats)
        {
            for (int i = 0; i < itemsNumber1; i++)
            {
                float randomNumber = Random.Range(rangeMin1, rangeMax1);
                if (floats.Count > 0)
                {
                    if (!floats.Contains(randomNumber))
                    {
                        while (floats.Contains(randomNumber) || Mathf.Abs(floats[floats.Count - 1] - randomNumber) < difference)
                        {
                            randomNumber = Random.Range(rangeMin1, rangeMax1);
                        }

                        floats.Add(randomNumber);
                    }
                    else
                    {
                        if (Mathf.Abs(floats[floats.Count - 1] - randomNumber) > difference)
                        {
                            floats.Add(randomNumber);
                        }
                        else
                        {
                            while (floats.Contains(randomNumber) || Mathf.Abs(floats[floats.Count - 1] - randomNumber) < difference)
                            {
                                randomNumber = Random.Range(rangeMin1, rangeMax1);
                            }

                            floats.Add(randomNumber);
                        }
                    }
                }
                else
                {
                    floats.Add(randomNumber);
                }
            }
        }
    }
}
