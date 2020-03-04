using Sourav.Engine.Core.ApplicationRelated;
using Sourav.Engine.Core.ControllerRelated;
using Sourav.Engine.Core.DebugRelated;
using Sourav.Utilities.Scripts.Utilities;
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
            SingletonObject[] objects = GameObject.FindObjectsOfType<SingletonObject>();
            ApplicationGame game = GameObject.FindObjectOfType<ApplicationGame>();
            Sourav.Engine.Core.ControllerRelated.Controller[] controllers = GameObject.FindObjectsOfType<Sourav.Engine.Core.ControllerRelated.Controller>();

            if (objects == null)
            {
                D.LogError("No Singleton Objects ");
            }
            else
            {
                for (int i = 0; i < objects.Length; i++)
                {
                    if (objects[i].type == SingletonTypes.CoreEngine)
                    {
                        if (PrefabUtility.IsAnyPrefabInstanceRoot(objects[i].gameObject))
                        {
                            PrefabUtility.UnpackPrefabInstance(objects[i].gameObject, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
                        }
                        break;
                    }
                }
            }
            
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
