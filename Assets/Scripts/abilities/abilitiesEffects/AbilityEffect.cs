using characters;
using JetBrains.Annotations;
using System;
using System.Data;
using UnityEditor.Compilation;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public abstract class AbilityEffect
    {
        public int Id { get; set; }
        public bool AbleToMiss { get; set; }
        public EffectType.School? School { get; set; }
        public int RangeToUse { get; set; }
        public bool SelfCast { get; set; }
        public abstract void Use(Fighter caster, Fighter target);
        protected AbilityEffectEvent abilityEffectEvent;
        public abstract AbilityEffectEvent GetAbilityEffectEvent(Fighter caster, Fighter target);
        public bool IsAbleToUse(Fighter caster, Fighter target)
        {
            if (SelfCast)
            {
                return true;
            }
            if (target.Position - caster.Position > 0)
            {
                return target.Position - caster.Position <= RangeToUse;
            }
            else
            {
                return caster.Position - target.Position <= RangeToUse;
            }
        }

        public void ClearAbilityEffectEvent()
        {
            abilityEffectEvent = null;
        }
    }
}

