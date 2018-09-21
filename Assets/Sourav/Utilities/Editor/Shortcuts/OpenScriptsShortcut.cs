using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Sourav.Utilities.EditorUtils
{
    public class OpenScriptsShortcut
    {
        //# Shift, % Ctrl & Alt
        [MenuItem("ProjectUtility/Shortcuts/Open C# Project #C")]
	    public static void Show()
        {
            EditorApplication.ExecuteMenuItem("Assets/Open C# Project");
        }
    }
}
