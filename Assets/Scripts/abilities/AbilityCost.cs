using Abilities;
using characters;
using System;
using System.Data;

namespace Abilities
{
    [Serializable]
    public class AbilityCost
    {
        public AttributeResources.Resources TargetAtt { get; set; }
        public bool Percentage { get; set; }
        public float Value { get; set; }

        public AbilityEffectEvent GetAbilityEffectEvent(Fighter caster)
        {
            AbilityEffectEvent abilityEffectEvent = new AbilityEffectEvent();
            abilityEffectEvent.Caster = caster;
            abilityEffectEvent.Target = caster;
            abilityEffectEvent.Hit = 1f;
            abilityEffectEvent.Modificator = 1f;
            abilityEffectEvent.Multiplier = 1f;
            abilityEffectEvent.Value = Value;
            abilityEffectEvent.Valid = 1;
            abilityEffectEvent.EventTarget = AbilityEffectEvent.EventTargetType.RESOURCES;
            abilityEffectEvent.AttTarget = (AttributeAbstract.Type)TargetAtt;
            return abilityEffectEvent;
        }
    }
}

