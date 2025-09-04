using System;
using Editor.EditorUtils;
using TopDownShooter.Configs;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ObjectConfig<>), true)]
    public class ObjectConfigEditor : UnityEditor.Editor
    {
        protected const string OBJ_PROPERTY_NAME = "obj";
        
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            
            var objProperty = serializedObject.FindProperty(OBJ_PROPERTY_NAME);
            var objectType = ((ObjectConfig)target).GetObjectType();
            GUIFunctions.TypeSelector("Select object", objectType, type => SetObject(objProperty, type));
            
            var obj = objProperty.managedReferenceValue;
            if (obj != null)
            {
                EditorGUILayout.PropertyField(objProperty, new GUIContent(obj.GetType().Name), true);
            }
            else
            {
                EditorGUILayout.HelpBox("Object is null. Select object", MessageType.Error);
            }
            
            DrawPropertiesExcluding(serializedObject, OBJ_PROPERTY_NAME, "m_Script");

            serializedObject.ApplyModifiedProperties();
        }
        
        private void SetObject(SerializedProperty objProperty, Type itemType)
        {
            var item = Activator.CreateInstance(itemType);

            objProperty.managedReferenceValue = item;

            serializedObject.ApplyModifiedProperties();
        }
    }
}