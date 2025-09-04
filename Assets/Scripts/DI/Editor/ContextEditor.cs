using DI.Contexts;
using DI.Installers;
using UnityEditor;
using UnityEngine;

namespace DI.Editor
{
    [CustomEditor(typeof(Context), true)]
    [CanEditMultipleObjects]
    public class ContextEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Find installers"))
            {
                var context = (Context)target;
                var monoInstallersProperty = serializedObject.FindProperty("monoInstallers");
                var monoInstallers = context.GetComponentsInChildren<MonoInstaller>();

                monoInstallersProperty.arraySize = monoInstallers.Length;

                for (var i = 0; i < monoInstallers.Length; i++)
                {
                    monoInstallersProperty.GetArrayElementAtIndex(i).boxedValue = monoInstallers[i];
                }

                serializedObject.ApplyModifiedProperties();
            }
            
            base.OnInspectorGUI();
        }
    }
}