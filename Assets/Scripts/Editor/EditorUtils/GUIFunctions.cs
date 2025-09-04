using System;
using Shooter.Utils;
using UnityEditor;
using UnityEngine;

namespace Editor.EditorUtils
{
    public static class GUIFunctions
    {
        public static void TypeSelector(string label, Type baseType, Action<Type> selectCallback)
        {
            if (EditorGUILayout.DropdownButton(new GUIContent(label), FocusType.Keyboard))
            {
                var menu = new GenericMenu();
                var types = baseType.GetInheritors();
            
                foreach (var type in types)
                {
                    menu.AddItem(new GUIContent(type.Name), false, () => selectCallback?.Invoke(type));
                }
            
                menu.ShowAsContext();
            }
        }
    }
}