using UnityEngine;
using UnityEngine.UI;

namespace Sourav.Utilities.Scripts.ViewUtils
{
    public class ColorUpdate : IUpdateColor
    {
        public void UpdateColor(string colorString, Graphic[] graphicsToColor)
        {
            Color color;
            colorString = "#" + colorString;
            if (ColorUtility.TryParseHtmlString(colorString, out color))
            {
                for (int i = 0; i < graphicsToColor.Length; i++)
                {
                    graphicsToColor[i].color = color;
                }
            }
        }
    }
}