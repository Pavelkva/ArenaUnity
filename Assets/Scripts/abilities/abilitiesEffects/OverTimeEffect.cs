
using characters;
using System;
using System.Xml.Serialization;

namespace Abilities
{
    public abstract class OverTimeEffect : OverTime
    {
        public AttributeAbstract.Type ModAtt { get; set; }
        public float Multiplier { get; set; }
        public bool Percentage { get; set; }
        public bool CalcFromTarget { get; set; }
        protected AbilityEffectEvent AbilityEffectEvent { get; set; }
    }
}

