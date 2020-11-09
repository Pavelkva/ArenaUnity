using characters;
using System;
using System.Xml.Serialization;
using UnityEngine;

namespace Abilities
{
    [Serializable]
    public class AbilityEffectEvent
    {
        public enum EventTargetType
        {
            DAMAGE,
            HEAL,
            RESOURCES,
            OVERTIME,
            POSITION,
        }

        [XmlElement("Damage", Type = typeof(Damage))]
        [XmlElement("Buff", Type = typeof(Buff))]
        [XmlElement("ConditionEffect", Type = typeof(ConditionEffect))]
        [XmlElement("Heal", Type = typeof(Heal))]
        [XmlElement("ChangePosition", Type = typeof(ChangePosition))]
        public AbilityEffect AbilityEffect { get; set; }
        [XmlIgnore]
        public Fighter Caster { get; set; }
        [XmlIgnore]
        public Fighter Target { get; set; }
        public bool? SelfCast { get; set; }
        public float? Modificator { get; set; }
        public float? Multiplier { get; set; }
        public float? Hit { get; set; }
        public float? Value { get; set; }
        public AttributeAbstract.Type AttTarget {get;set;}
        public int? Valid { get; set; }
        public EventTargetType? EventTarget {get; set;}
        private float realValue;
        public float RealValue 
        {
            get
            {
                float modificator = Modificator == null ? 1 : Modificator.Value;
                float multiplier = Multiplier == null ? 1 : Multiplier.Value;
                float hit = Hit == null ? 1 : Hit.Value;
                float value = Value == null ? 1 : Value.Value;
                return (value * modificator) * hit * multiplier;
            }
            set
            {
                realValue = value;
            } 
        }

        public AbilityEffectEvent()
        {

        }
        public AbilityEffectEvent (AbilityEffect abilityEffect, Fighter caster, Fighter target, float hit, int valid, float value, float multiplier, float modificator, EventTargetType eventTarget)
        {
            AbilityEffect = abilityEffect;
            Caster = caster;
            Target = target;
            Hit = hit;
            Valid = valid;
            Value = value;
            Multiplier = multiplier;
            Modificator = modificator;
            EventTarget = eventTarget;
        }
    }
}

