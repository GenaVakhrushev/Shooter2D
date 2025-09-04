using System.Reflection;

namespace DI.Utils
{
    public static class Settings
    {
        public const string PROJECT_CONTEXT_PREFAB_NAME = "ProjectContext";
        public const BindingFlags BINDING_FLAGS = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
    }
}