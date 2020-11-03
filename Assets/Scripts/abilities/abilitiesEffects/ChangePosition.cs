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

        public override void Use(Fighter caster, Fighter target)
        {
            AbilityEffectEvent abilityEffect = GetAbilityEffectEvent(caster, target);
            int howFarCanMove = HowFarCanMove(target);
            if (ReverseDirection)
            {
                abilityEffect.Value = HowFar * (-1);
                howFarCanMove *= (-1);
            }
            int movingToPosition = (int)(target.Position + abilityEffect.RealValue);
            Debug.Log(Id + " how far can move: " + HowFarCanMove(target) + " realvalue: " + abilityEffect.RealValue);
            
            if (ReverseDirection && howFarCanMove > abilityEffect.RealValue || !ReverseDirection && howFarCanMove < abilityEffect.RealValue)
            {
                abilityEffect.Multiplier = 1;
                abilityEffect.Modificator = 1;
                abilityEffect.Value = howFarCanMove;
            }
            Debug.Log(" realvalue: " + abilityEffect.RealValue);
            target.ChangePosition(abilityEffect);
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

        private AbilityEffectEvent GetAbilityEffectEvent(Fighter caster, Fighter target)
        {
            AbilityEffectEvent abilityEffectEvent = new AbilityEffectEvent();
            abilityEffectEvent.AbilityEffect = this;
            abilityEffectEvent.Caster = caster;
            abilityEffectEvent.Target = target;
            abilityEffectEvent.Value = HowFar;
            abilityEffectEvent.EventTarget = AbilityEffectEvent.EventTargetType.POSITION;
            return abilityEffectEvent;
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
                    Debug.Log(Id + " leftLimt " + leftLimt + " - " + target.Position);
                    howFarCanMove = (leftLimt - target.Position) * -(1);
                }
                else
                {
                    Debug.Log(Id + " EnemyPosition " + target.EnemyPosition + " - " + target.Position) ;
                    howFarCanMove = (target.EnemyPosition - target.Position + 1) * -(1);
                }
            }
            else
            {
                if (RunTrough || target.EnemyPosition < target.Position)
                {
                    Debug.Log(Id + " rightLimit " + rightLimit + " - " + target.Position);
                    howFarCanMove = (rightLimit - target.Position);
                }
                else
                {
                    Debug.Log(Id + " EnemyPosition " + target.EnemyPosition + " - " + target.Position);
                    howFarCanMove = (target.EnemyPosition - target.Position - 1);
                }
            }

            return howFarCanMove;
        }
    }
}

