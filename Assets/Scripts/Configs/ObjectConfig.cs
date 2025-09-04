using System;
using Shooter.Views;
using UnityEngine;

namespace TopDownShooter.Configs
{
    public abstract class ObjectConfig : ScriptableObject
    {
        public ObjectView ViewPrefab;
        public abstract object GetObject();
        public abstract Type GetObjectType();
    }
    
    public abstract class ObjectConfig<T> : ObjectConfig
    {
        [SerializeReference] 
        private T obj;
        
        public T Object { get => obj; private set => obj = value; }

        public override object GetObject() => obj;

        public override Type GetObjectType() => typeof(T);
    }
}