using System;
using System.Linq;
using DI.Attributes;

namespace DI.Utils
{
    public static class Extensions
    {
        public static bool IsInjectable(this object target)
        {
            var members = target.GetType().GetMembers(Settings.BINDING_FLAGS);
            return members.Any(member => Attribute.IsDefined(member, typeof(InjectAttribute)));
        }
    }
}