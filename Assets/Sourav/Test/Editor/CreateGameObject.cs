﻿using UnityEditor;
using UnityEngine;

namespace Sourav.Test.Editor
{
    public class CreateGameObject
    {
        //# Shift, % Ctrl & Alt
        [MenuItem("ProjectUtility/Test/Create Game Object %#C")]
        public static void Create()
        {
            GameObject g = new GameObject("New GameObject");
            Debug.Log(Selection.activeObject.name);

            MonoBehaviour b = (MonoBehaviour)Selection.activeObject;
            if(b != null)
            {
            
            }

            // string[] types = AssetDatabase.FindAssets("t:Script");
            //
            // foreach (string str in types)
            // {
            //     D.Log(str);
            // }
            // D.Log(System.Type.GetType(typeof(CreateGameObject).ToString()).ToString());
        }
    }
}
