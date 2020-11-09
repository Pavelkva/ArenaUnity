using characters;

namespace Abilities
{
    public class Buff : OverTimeEffect
    {
        public AttributeBonus.Bonus AttTarget { get; set; }
        public float Bonus { get; set; }

        public override void ApplyEffect(Fighter fighter, AbilityEffectEvent abilityEffectEvent)
        {
            fighter.CharacterAttributes.GetAttribute(AttTarget).Value = fighter.CharacterAttributes.GetAttribute(AttTarget).Value + abilityEffectEvent.RealValue;
            AddStackOrOverTime(fighter.SpellEffects);
            AbilityEffectEvent = abilityEffectEvent;
        }

        public override void RemoveEffect(Fighter fighter)
        {
            fighter.SpellEffects.Remove(this);
            fighter.CharacterAttributes.GetAttribute(AttTarget).Value = fighter.CharacterAttributes.GetAttribute(AttTarget).Value - AbilityEffectEvent.RealValue;
        }

        public override AbilityEffectEvent GetAbilityEffectEvent(Fighter caster, Fighter target)
        {
            abilityEffectEvent = new AbilityEffectEvent(this, caster, target, 1, 1, Bonus, 1, 1, AbilityEffectEvent.EventTargetType.OVERTIME);
            return abilityEffectEvent;
        }

        public override void Use(Fighter caster, Fighter target)
        {
            target.TakeOverTime(GetAbilityEffectEvent(caster, target));
        }
    }
}

