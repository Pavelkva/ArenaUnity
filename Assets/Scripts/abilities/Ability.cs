﻿
using characters;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Abilities
{
    [Serializable]
    public class Ability
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [XmlElement("AbilityCost")]
        public List<AbilityCost> AbilityCost { get; set; }
        [XmlElement("Damage", Type = typeof(Damage))]
        [XmlElement("Buff", Type = typeof(Buff))]
        [XmlElement("ConditionEffect", Type = typeof(ConditionEffect))]
        [XmlElement("Heal", Type = typeof(Heal))]
        [XmlElement("ChangePosition", Type = typeof(ChangePosition))]
        public List<AbilityEffect> AbilityEffects { get; set; }
        public int Cooldown { get; set; }
        public int CooldownReaming { get; set; }
        public string Description { get; set; }
        public bool AbleToMiss { get; set; }
        public int RangeToUse { get; set; }

        public bool IsAbleTouse(Fighter caster, Fighter target)
        {
            bool allEffectsUnableToUse = true;
            foreach (AbilityEffect abe in AbilityEffects)
            {
                Fighter targetForEffect = target;
                if (abe.SelfCast)
                {
                    targetForEffect = caster;
                }
                if (abe is ChangePosition)
                {
                    if (((ChangePosition)abe).IsAbleToUse(caster, targetForEffect))
                    {
                        allEffectsUnableToUse = false;
                    }
                } 
                else
                {
                    if (abe.IsAbleToUse(caster, targetForEffect))
                    {
                        allEffectsUnableToUse = false;
                    }
                }
            }
            if (allEffectsUnableToUse)
            {
                return false;
            }


            foreach (AbilityCost abilityCost in AbilityCost)
            {   
                if (caster.GetMissingResource(abilityCost.GetAbilityEffectEvent(caster)) != 0)
                {
                    return false;
                }
            }
            if (RangeToUse != 0)
            {
                if (target.Position - caster.Position > 0)
                {
                    return target.Position - caster.Position <= RangeToUse;
                }
                else
                {
                    return caster.Position - target.Position <= RangeToUse;
                }
            }
            return true;
        }

        public void Use(Fighter caster, Fighter target, bool consumeResources)
        {
            if (consumeResources)
            {
                foreach (AbilityCost abilityCost in AbilityCost)
                {
                    caster.ConsumeResources(abilityCost.GetAbilityEffectEvent(caster));
                }
            }
            else
            {
                       
                foreach (AbilityEffect abe in AbilityEffects)
                {
                    
                    Fighter targetForEffect = target;
                    if (abe.SelfCast)
                    {
                        targetForEffect = caster;
                    }


                    if (abe is ChangePosition)
                    {
                        ((ChangePosition)abe).EnemyPosition = caster.EnemyPosition;
                    }
                    if (abe.IsAbleToUse(caster, targetForEffect))
                    {
                        caster.EventOccured(abe.GetAbilityEffectEvent(caster, target), EventsEnum.ABILITYBEFOREUSE);
                        abe.Use(caster, targetForEffect);
                        caster.EventOccured(abe.GetAbilityEffectEvent(caster, target), EventsEnum.ABILITYAFTERUSE);
                        abe.ClearAbilityEffectEvent();
                    }
                }
            }
        }
    }

}


