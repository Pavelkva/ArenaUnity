using characters;
using System;
using System.Dynamic;

namespace Abilities
{
    public class Damage : AbilityEffect
    {
        public float MinEffect { get; set; }
        public float MaxEffect { get; set; }
        public float Multiplier { get; set; }
        public AttributeAbstract.Type MultiplierAtt { get; set; }
        public float BonusMultiplier { get; set; }
        public AttributeAbstract.Type BonusAtt { get; set; }
        public bool CalcFromTarget { get; set; }
        public bool AbleToCrit { get; set; }
        public AttributeResources.Resources TargetAtt { get; set; }
        public float RealValue { get; set; }

        public override void Use(Fighter caster, Fighter target)
        {
            ICharacterAttributes statsToCalc = caster.CharacterAttributes;
            if (CalcFromTarget)
            {
                statsToCalc = target.CharacterAttributes;
            }

            // create new event and set default hit and modificator
            AbilityEffectEvent effectEvent = new AbilityEffectEvent(this, caster, target, 1, 1, 1, 1, 1, AbilityEffectEvent.EventTargetType.DAMAGE);

            float effect = MaxEffect;
            if (MinEffect != MaxEffect)
            {
                effect = Utils.GetRandomInt((int)MinEffect, (int)MaxEffect);
            }

            float multiAtt = statsToCalc.GetAttribute(MultiplierAtt).Value;
            float bonusMultiAtt = statsToCalc.GetAttribute(BonusAtt).Value;
            float damage;
            if (Multiplier > 0)
            {
                damage = effect * Multiplier * multiAtt + BonusMultiplier * bonusMultiAtt;
            }
            else
            {
                damage = effect + BonusMultiplier * bonusMultiAtt;
            }


            effectEvent.Value = damage;

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
            target.TakeDamage(effectEvent);

        }
    }

}
