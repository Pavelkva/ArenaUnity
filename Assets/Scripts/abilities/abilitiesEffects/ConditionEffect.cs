
using characters;
using System.Collections.Generic;
using System.Diagnostics;
using System.Xml.Serialization;

namespace Abilities
{
    public class ConditionEffect : OverTime
    {
        public float RemoveAfterTrigger { get; set; }
        public int CastMode { get; set; }
        [XmlArray(ElementName = "EffectsToUse", IsNullable = true)]
        [XmlArrayItem("Damage", Type = typeof(Damage))]
        [XmlArrayItem("Buff", Type = typeof(Buff))]
        [XmlArrayItem("ConditionEffect", Type = typeof(ConditionEffect))]
        [XmlArrayItem("Heal", Type = typeof(Heal))]
        public List<AbilityEffect> EffectsToUse { get; set; }
        public EventsEnum Event { get; set; }

        [XmlArray(ElementName = "GreaterCheck", IsNullable = true)]
        public List<AbilityEffectEvent> GreaterCheck;
        [XmlArray(ElementName = "LowerCheck", IsNullable = true)]
        public List<AbilityEffectEvent> LowerCheck;
        public AbilityEffectEvent EffectEventChange { get; set; }

        public void EventOccured(AbilityEffectEvent abilityEffectEvent)
        {
            bool proceed = true;

            if (GreaterCheck != null)
            {
                foreach (AbilityEffectEvent abee in GreaterCheck)
                {
                    if (!StaticCheck(abee, abilityEffectEvent))
                    {
                        proceed = false;
                    }
                    if (abee.Hit != null && abee.Hit < abilityEffectEvent.Hit)
                    {
                        proceed = false;
                    }
                    if (abee.AbilityEffect != null)
                    {
                        if (abee.AbilityEffect.Id < abilityEffectEvent.AbilityEffect.Id)
                        {
                            proceed = false;
                        }
                    }
                }
            }
            
            if (LowerCheck != null)
            {
                foreach (AbilityEffectEvent abee in LowerCheck)
                {
                    if (!StaticCheck(abee, abilityEffectEvent))
                    {
                        proceed = false;
                    }
                    if (abee.Hit != null && abee.Hit > abilityEffectEvent.Hit)
                    {
                        proceed = false;
                    }
                    if (abee.AbilityEffect != null)
                    {
                        if (abee.AbilityEffect.Id > abilityEffectEvent.AbilityEffect.Id)
                        {
                            proceed = false;
                        }
                    } 
                }
            }
            

            if (proceed)
            {
                Fighter caster;
                Fighter target;

                switch (CastMode)
                {
                    case 1:
                        caster = abilityEffectEvent.Target;
                        target = abilityEffectEvent.Caster;
                        break;
                    case 2:
                        caster = abilityEffectEvent.Target;
                        target = abilityEffectEvent.Target;
                        break;
                    case 3:
                        caster = abilityEffectEvent.Caster;
                        target = abilityEffectEvent.Caster;
                        break;
                    default:
                        caster = abilityEffectEvent.Caster;
                        target = abilityEffectEvent.Target;
                        break;
                }

                if (EffectsToUse != null && EffectsToUse.Count > 0)
                {
                    EffectsToUse.ForEach(e => e.Use(caster, target));
                }

                if (EffectEventChange != null)
                {
                    if (EffectEventChange.Modificator != null)
                    {
                        abilityEffectEvent.Modificator += EffectEventChange.Modificator;
                    }

                    if (EffectEventChange.Multiplier != null)
                    {
                        abilityEffectEvent.Multiplier += EffectEventChange.Multiplier;
                    }

                    if (EffectEventChange.AbilityEffect != null && EffectEventChange.AbilityEffect.School != null)
                    {
                        abilityEffectEvent.AbilityEffect.School = EffectEventChange.AbilityEffect.School;
                    }

                    if (EffectEventChange.EventTarget != null)
                    {
                        switch (EffectEventChange.EventTarget)
                        {
                            case AbilityEffectEvent.EventTargetType.DAMAGE:
                                target.TakeDamage(abilityEffectEvent);
                                break;
                            case AbilityEffectEvent.EventTargetType.HEAL:
                                target.TakeHeal(abilityEffectEvent);
                                break;
                        }
                    }

                    if (EffectEventChange.Valid != null && EffectEventChange.Valid == 0)
                    {
                        abilityEffectEvent = null;
                    }

                    if (RemoveAfterTrigger > Utils.GetRanomFloat())
                    {

                    }
                }
            }

            

        }
        public override void ApplyEffect(Fighter fighter, AbilityEffectEvent abilityEffectEvent)
        {
            AddStackOrOverTime(fighter.SpellEffects);
        }

        public override void RemoveEffect(Fighter fighter)
        {
            fighter.SpellEffects.Remove(this);
        }

        public override AbilityEffectEvent GetAbilityEffectEvent(Fighter caster, Fighter target)
        {
            if (abilityEffectEvent != null)
            {
                return abilityEffectEvent;
            }
            abilityEffectEvent = new AbilityEffectEvent(this, caster, target, 1, 1, 1, 1, 1, AbilityEffectEvent.EventTargetType.OVERTIME);
            return abilityEffectEvent;
        }

        public override void Use(Fighter caster, Fighter target)
        {
            target.TakeOverTime(GetAbilityEffectEvent(caster, target));
        }

        private bool StaticCheck(AbilityEffectEvent abeeCheck, AbilityEffectEvent abeeReceived)
        {
            bool proceed = true;

            if (abeeCheck.AbilityEffect != null)
            {
                if (abeeCheck.AbilityEffect.School != abeeReceived.AbilityEffect.School)
                {
                    proceed = false;
                }
            }

            if (abeeCheck.EventTarget != null && abeeCheck.EventTarget != abeeReceived.EventTarget)
            {
                proceed = false;
            }

            return proceed;
        }
    }
}

