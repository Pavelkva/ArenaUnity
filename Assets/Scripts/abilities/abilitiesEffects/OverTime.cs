using characters;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Abilities
{
    public abstract class OverTime : AbilityEffect
    {
        [XmlIgnore]
        public Fighter Caster { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
        public int ReamingTime {get;set;}
        public int Stackable { get; set; }
        public int Stacks { get; set; }

        public abstract void RemoveEffect(Fighter fighter);
        public abstract void ApplyEffect(Fighter fighter, AbilityEffectEvent abilityEffectEvent);
        /// <summary>
        /// Add one stacka or new over time effect to fighter.
        /// </summary>
        /// <param name="spellEffects">Fighter's spell effects</param>
        protected void AddStackOrOverTime(List<OverTime> spellEffects) 
        {
            bool overTimeOnTarget = false;
            foreach (OverTime overTime in spellEffects)
            {
                if (overTime.Id == Id)
                {
                    overTimeOnTarget = true;
                    if (overTime.Stackable > overTime.Stacks)
                    {
                        overTime.Stacks++;
                    }
                    overTime.ReamingTime = overTime.Duration;
                }
            }
            if (!overTimeOnTarget)
            {
                Stacks = 1;
                ReamingTime = Duration;
                spellEffects.Add(this);
            } 
        }

    }
}

