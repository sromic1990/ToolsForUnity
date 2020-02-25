using Sourav.Engine.Core.ApplicationRelated;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.DebugRelated;
using UnityEditor;
using UnityEngine;

namespace Sourav.Utilities.Editor.PopulateControllerRelated
{
    public class PopulateAllControllers : UnityEditor.Editor
    {
        //% = cmd or ctrl, # = shift, & = alt
        [MenuItem("ProjectUtility/Utilities/Populate All Controllers %#&p")]
        public static void PopulateControllers()
        {
            ApplicationGame game = GameObject.FindObjectOfType<ApplicationGame>();
            Controller[] controllers = GameObject.FindObjectsOfType<Controller>();

            if (game == null)
            {
                D.LogError("Application prefab not present in scene");
            }
            else
            {
                if (controllers.Length == 0)
                {
                    D.LogError("No controllers present in scene");
                }
                else
                {
                    game.SetControllers(controllers);
                }
            }
        }
    }
}
