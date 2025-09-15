using System;
using UnityEngine;

namespace Shooter.HP
{
    public class HPModel
    {
        public float CurrentHP { get; private set; }

        public float MaxHP { get; private set; }

        public event Action<float> HpChanged;

        public HPModel(float initialHP, float maxHP)
        {
            CurrentHP = initialHP;
            MaxHP = maxHP;
        }

        public void SetHP(float hp)
        {
            CurrentHP = Mathf.Clamp(hp, 0, MaxHP);
            
            HpChanged?.Invoke(CurrentHP);
        }
    }
}