using System.Linq;
using Shooter.Databases;
using Shooter.Inventory.Items;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(ItemsDatabase))]
    public class ItemsDatabaseEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Find items"))
            {
                var guids = AssetDatabase.FindAssets($"t:{nameof(ItemConfig)}");
                var configs = guids
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<ItemConfig>)
                    .ToArray();

                var itemsDatabase = (ItemsDatabase)target;
                var configsProperty = serializedObject.FindProperty(nameof(itemsDatabase.ItemConfigs));

                configsProperty.arraySize = configs.Length;
                for (var i = 0; i < configs.Length; i++)
                {
                    configsProperty.GetArrayElementAtIndex(i).objectReferenceValue = configs[i];
                }

                serializedObject.ApplyModifiedProperties();
            }
            
            base.OnInspectorGUI();
        }
    }
}