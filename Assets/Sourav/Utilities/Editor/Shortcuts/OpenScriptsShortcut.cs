using UnityEditor;

namespace Sourav.Utilities.Editor.Shortcuts
{
    public class OpenScriptsShortcut
    {
        //# Shift, % Ctrl & Alt
        [MenuItem("ProjectUtility/Shortcuts/Open C# Project #%c")]
	    public static void Show()
        {
            EditorApplication.ExecuteMenuItem("Assets/Open C# Project");
        }
    }
}
