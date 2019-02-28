using System;
using UnityEngine;

namespace Sourav.Tools.ReferenceFinder.Editor.Styles
{
    [Serializable]
    internal class ContentStylePair
    {
        public GUIContent Content = new GUIContent();
        public GUIStyle Style = new GUIStyle();
    }
}