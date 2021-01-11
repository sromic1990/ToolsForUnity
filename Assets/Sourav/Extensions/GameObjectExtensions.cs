using UnityEngine;

namespace Sourav.Extensions
{
    public static class GameObjectExtensions
    {
        public static void Show(this GameObject gObj)
        {
            if (gObj != null)
            {
                gObj.SetActive(true);
            }
            else
            {
                // D.LogError($"gameobject is null");
            }
        }

        public static void Hide(this GameObject gObj)
        {
            if (gObj != null)
            {
                gObj.SetActive(false);
            }
            else
            {
                // D.LogError($"gameobject is null");
            }
        }
    }
}
