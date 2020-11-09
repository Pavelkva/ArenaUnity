using characters;

namespace Abilities
{
    public class Heal : Damage
    {
        public override AbilityEffectEvent GetAbilityEffectEvent(Fighter caster, Fighter target)
        {
            if (abilityEffectEvent != null)
            {
                return abilityEffectEvent;
            }

            ICharacterAttributes statsToCalc = caster.CharacterAttributes;
            if (CalcFromTarget)
            {
                statsToCalc = target.CharacterAttributes;
            }

            // create new event and set default hit and modificator
            AbilityEffectEvent effectEvent = new AbilityEffectEvent(this, caster, target, 1, 1, 1, 1, 1, AbilityEffectEvent.EventTargetType.HEAL);
            effectEvent.AttTarget = (AttributeAbstract.Type)TargetAtt;

            float effect = MaxEffect;
            if (MinEffect != MaxEffect)
            {
                effect = Utils.GetRandomInt((int)MinEffect, (int)MaxEffect);
            }

            float multiAtt = statsToCalc.GetAttribute(MultiplierAtt).Value;
            float bonusMultiAtt = statsToCalc.GetAttribute(BonusAtt).Value;
            float heal;
            if (Multiplier > 0)
            {
                heal = effect * Multiplier * multiAtt + BonusMultiplier * bonusMultiAtt;
            }
            else
            {
                heal = effect + BonusMultiplier * bonusMultiAtt;
            }


            effectEvent.Value = heal;

            // Check if crit
            if (AbleToCrit)
            {
                float critChance = caster.CharacterAttributes.GetAttribute(AttributeSum.Sum.CRITCHANCE).Value;
                if (Utils.GetRandomInt(1, 100) <= critChance)
                {
                    effectEvent.Hit = 2f;
                }
            }

            // Check if miss
            if (AbleToMiss)
            {
                double missChance = target.CharacterAttributes.GetAttribute(AttributeSum.Sum.DODGECHANCE).Value;
                if (Utils.GetRandomInt(1, 100) <= missChance)
                {
                    effectEvent.Hit = 0f;
                }
            }
            abilityEffectEvent = effectEvent;
            return abilityEffectEvent;
        }
        public override void Use(Fighter caster, Fighter target)
        {
            target.TakeHeal(GetAbilityEffectEvent(caster, target));
        }
    }
}

