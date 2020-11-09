using Abilities;
using characters;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Abilities
{
    public class ChangePosition : AbilityEffect
    {
        public int HowFar { get; set; }
        public bool ReverseDirection { get; set; }
        public bool TrigerTraps { get; set; }
        public bool RunTrough { get; set; }
        [XmlIgnore]
        public int EnemyPosition { get; set; }

        public override AbilityEffectEvent GetAbilityEffectEvent(Fighter caster, Fighter target)
        {
            if (abilityEffectEvent != null)
            {
                return abilityEffectEvent;
            }

            AbilityEffectEvent abilityEffect = new AbilityEffectEvent();
            abilityEffect.AbilityEffect = this;
            abilityEffect.Caster = caster;
            abilityEffect.Target = target;
            abilityEffect.Value = HowFar;
            abilityEffect.EventTarget = AbilityEffectEvent.EventTargetType.POSITION;

            int howFarCanMove = HowFarCanMove(target);
            if (ReverseDirection)
            {
                abilityEffect.Value = HowFar * (-1);
                howFarCanMove *= (-1);
            }

            if (ReverseDirection && howFarCanMove > abilityEffect.RealValue || !ReverseDirection && howFarCanMove < abilityEffect.RealValue)
            {
                abilityEffect.Multiplier = 1;
                abilityEffect.Modificator = 1;
                abilityEffect.Value = howFarCanMove;
            }
            abilityEffectEvent = abilityEffect;
            return abilityEffectEvent;
        }

        public override void Use(Fighter caster, Fighter target)
        {
            target.ChangePosition(GetAbilityEffectEvent(caster, target));
        }

        public new bool IsAbleToUse(Fighter caster, Fighter target)
        {
            if (!base.IsAbleToUse(caster, target))
            {
                return false;
            }
            
            int maxMoveDistance = HowFarCanMove(target);
            
            AbilityEffectEvent abilityEffect = GetAbilityEffectEvent(caster, target);
            if (ReverseDirection)
            {
                abilityEffect.Value = HowFar * (-1);
            }


            if (HowFarCanMove(target) > 0)
            {
                return true;
            }
            int movingToPosition = (int)(target.Position + abilityEffect.RealValue);
            /*
            if (target.PositionLimit >= movingToPosition && movingToPosition >= 0 && EnemyPosition != movingToPosition)
            {
                return true;
            } 
            */
            return false;
        }

        private int HowFarCanMove(Fighter target)
        {
            int leftLimt = 0;
            int rightLimit = target.PositionLimit;
            int howFarCanMove;

            if (ReverseDirection)
            {
                if (RunTrough || target.EnemyPosition > target.Position)
                {
                    howFarCanMove = (leftLimt - target.Position) * -(1);
                }
                else
                {
                    howFarCanMove = (target.EnemyPosition - target.Position + 1) * -(1);
                }
            }
            else
            {
                if (RunTrough || target.EnemyPosition < target.Position)
                {
                    howFarCanMove = (rightLimit - target.Position);
                }
                else
                {
                    howFarCanMove = (target.EnemyPosition - target.Position - 1);
                }
            }

            return howFarCanMove;
        }
    }
}

