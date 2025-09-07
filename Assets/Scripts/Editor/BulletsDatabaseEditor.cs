using System.Linq;
using Shooter.Damage.Bullets;
using Shooter.Databases;
using Shooter.Inventory.Items;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [CustomEditor(typeof(BulletsDatabase))]
    public class BulletsDatabaseEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Find items"))
            {
                var guids = AssetDatabase.FindAssets($"t:{nameof(BulletConfig)}");
                var configs = guids
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .Select(AssetDatabase.LoadAssetAtPath<BulletConfig>)
                    .ToArray();

                var bulletsDatabase = (BulletsDatabase)target;
                var configsProperty = serializedObject.FindProperty(nameof(bulletsDatabase.BulletConfigs));

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