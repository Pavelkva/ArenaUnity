using Abilities;
using Items;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Events;

namespace characters
{
    /// <summary>
    /// Fighter class represents fighter who have stats, can casts abilities and take effects.
    /// </summary>
    public class Fighter : MonoBehaviour
    {
        public event EventHandler ResourcesChanged;
        public event EventHandler OverTimeChanged;
        public event EventHandler EffectTaked;
        public event EventHandler PositionChanged;
        public delegate void EventHandler(Fighter m, FighterEvent e);

        private Equipment[] equipmentSlots = new Equipment[Enum.GetNames(typeof(Equipment.Parts)).Length];

        public string Name { get; set; }
        public int Position { get; set; }
        public int PositionLimit { get; set; }
        public int EnemyPosition { get; set; }
        private ICharacterAttributes characterAttributes = new CharacterAttributes();
        public ICharacterAttributes CharacterAttributes
        {
            get { return characterAttributes = (characterAttributes == null) ? new CharacterAttributes() : characterAttributes; }
            set { characterAttributes = value; }
        }
        private List<Ability> spellBook;
        public List<Ability> SpellBook
        {
            get { return spellBook = (spellBook == null) ? new List<Ability>() : spellBook; }
            set { spellBook = value; }
        }
        private List<OverTime> spellEffects;
        public List<OverTime> SpellEffects
        {
            get { return spellEffects = (spellEffects == null) ? new List<OverTime>() : spellEffects; }
            set { spellEffects = value; }
        }
        public bool IsAlive 
        { 
            get { return CharacterAttributes.GetAttribute(AttributeResources.Resources.ACTUALHP).Value > 0; }
            private set { }
        }

        public IAttribute GetAttribute(AttributeAbstract.Type type)
        {
            return CharacterAttributes.GetAttribute(type);
        }
        public AttributeSum GetAttribute(AttributeSum.Sum type)
        {
            return CharacterAttributes.GetAttribute(type);
        }
        public AttributeBonus GetAttribute(AttributeBonus.Bonus type)
        {
            return CharacterAttributes.GetAttribute(type);
        }
        public AttributeBase GetAttribute(AttributeBase.Base type)
        {
            return CharacterAttributes.GetAttribute(type);
        }
        public AttributeResources GetAttribute(AttributeResources.Resources type)
        {
            return CharacterAttributes.GetAttribute(type);
        }

        /// <summary>
        /// Equip equipment to fighter and add all bonuses from it.
        /// </summary>
        /// <param name="equipment">Equipment to equip</param>
        public void Equip(Equipment equipment)
        {
            equipmentSlots[(int)equipment.Part] = equipment;
            foreach (AttributeBonus bonus in equipment.Bonus)
            {
                GetAttribute(bonus.Attribute).Value += bonus.Value;
            }
        }

        /// <summary>
        /// Equip equipment to fighter and add all bonuses from it.
        /// </summary>
        /// <param name="equipment">Equipment to equip</param>
        public Dictionary<Equipment.Parts, Equipment> getEquiped()
        {
            Dictionary<Equipment.Parts, Equipment> dictionaryEq = new Dictionary<Equipment.Parts, Equipment>();
            foreach (Equipment.Parts part in (Equipment.Parts[])Enum.GetValues(typeof(Equipment.Parts)))
            {
                dictionaryEq.Add(part, equipmentSlots[(int)part]);
            }
            return dictionaryEq;
        }

        /// <summary>
        /// Unequip equipment to fighter and remove all bonuses from it.
        /// </summary>
        /// <param name="equipment">Equipment to unequip</param>
        public void Unequip(Equipment equipment)
        {
            equipmentSlots[(int)equipment.Part] = equipment;
            foreach (AttributeBonus bonus in equipment.Bonus)
            {
                GetAttribute(bonus.Attribute).Value -= bonus.Value;
            }
        }

        /// <summary>
        /// Check if it possible to use ability to target and use it.
        /// </summary>
        /// <param name="ability">Ability to use.</param>
        /// <param name="target">Target for ability.</param>
        public void UseAbility(Ability ability, Fighter target, bool consumeResources)
        {
            ability.Use(this, target, consumeResources);
        }

        /// <summary>
        /// Recive damage from event.
        /// </summary>
        /// <param name="abilityEffectEvent">Event which contains number attributes about damage.</param>
        public void TakeDamage(AbilityEffectEvent abilityEffectEvent)
        {
            GetAttribute(AttributeResources.Resources.ACTUALHP).Value -= abilityEffectEvent.RealValue;
            WhenResourceChanged(abilityEffectEvent);
            WhenTakeEffect(abilityEffectEvent);
        }

        /// <summary>
        /// Recive heal from event.
        /// </summary>
        /// <param name="abilityEffectEvent">Event which contains number attributes about heal.</param>
        public void TakeHeal(AbilityEffectEvent abilityEffectEvent)
        {
            GetAttribute(abilityEffectEvent.AttTarget).Value += abilityEffectEvent.RealValue;
            WhenResourceChanged(abilityEffectEvent);
            WhenTakeEffect(abilityEffectEvent);
        }

        /// <summary>
        /// Consume any type of resources
        /// </summary>
        /// <param name="abilityEffectEvent">Event which contains number attributes about resources consume.</param>
        public void ConsumeResources(AbilityEffectEvent abilityEffectEvent)
        {
            if (abilityEffectEvent != null)
            {
                float valueBefore = CharacterAttributes.GetAttribute(abilityEffectEvent.AttTarget).Value;
                GetAttribute(abilityEffectEvent.AttTarget).Value -= abilityEffectEvent.RealValue;
                EventOccured(abilityEffectEvent, EventsEnum.RESOURCEAFTERTAKE);
                WhenResourceChanged(abilityEffectEvent);
            }
        }

        /// <summary>
        /// Change position of fighter
        /// </summary>
        /// <param name="abilityEffectEvent">Event which contains number attributes about resources consume.</param>
        public void ChangePosition(AbilityEffectEvent abilityEffectEvent)
        {
            if (abilityEffectEvent != null)
            {
                EventOccured(abilityEffectEvent, EventsEnum.POSITIONBEFORECHANGE);
                Position += (int)abilityEffectEvent.RealValue; 
                EventOccured(abilityEffectEvent, EventsEnum.POSITIONAFTERCHANGE);
                WhenResourceChanged(abilityEffectEvent);
                WhenPositionChanged(abilityEffectEvent);
            }
        }

        public AttributeResources.Resources GetMissingResource(AbilityEffectEvent abilityEffectEvent) 
        {
            EventOccured(abilityEffectEvent, EventsEnum.RESOURCEBEFORETAKE);
            if (abilityEffectEvent != null && 
                CharacterAttributes.GetAttribute(abilityEffectEvent.AttTarget).Value < abilityEffectEvent.RealValue)
            {
                return (AttributeResources.Resources)abilityEffectEvent.AttTarget;
            } 
            else
            {
                return 0;
            }
        }


        /// <summary>
        /// Take save and apply overtime effect to fighter.
        /// </summary>
        /// <param name="abilityEffectEvent">Number attributes of overtime effects.</param>
        public void TakeOverTime(AbilityEffectEvent abilityEffectEvent)
        {
            EventOccured(abilityEffectEvent, EventsEnum.OVERTIMEBEFORETAKE);
            if (abilityEffectEvent.Valid == null || abilityEffectEvent.Valid.Value == 1)
            {
                OverTime overTime = (OverTime)abilityEffectEvent.AbilityEffect;
                overTime.Caster = abilityEffectEvent.Caster;
                overTime.ApplyEffect(this, abilityEffectEvent);
                EventOccured(abilityEffectEvent, EventsEnum.OVERTIMEAFTERTAKE);
                WhenOverTimeChanged(abilityEffectEvent);
            }
            
        }

        public void EventOccured(AbilityEffectEvent abilityEffectEvent, EventsEnum eventEnum)
        {
            if (SpellEffects == null)
            {
                SpellEffects = new List<OverTime>();
            }
            foreach (AbilityEffect conditionEffect in SpellEffects)
            {
                if (conditionEffect is ConditionEffect)
                {
                    if (((ConditionEffect)conditionEffect).Event == eventEnum)
                    {
                        if (abilityEffectEvent == null)
                        {
                            abilityEffectEvent = new AbilityEffectEvent();
                            abilityEffectEvent.Caster = ((OverTime)conditionEffect).Caster;
                            abilityEffectEvent.Target = this;
                        }
                        ((ConditionEffect)conditionEffect).EventOccured(abilityEffectEvent);
                    }
                }
                
            }
        }

        public void CheckOverTimeEffects()
        {
            List<OverTime> overTimeToRemove = new List<OverTime>();
            SpellEffects.ForEach(overTime => overTime.ReamingTime -= 1) ;
            SpellEffects.ForEach(overTime => { if (overTime.ReamingTime == 0) overTimeToRemove.Add(overTime);});
            overTimeToRemove.ForEach(s => SpellEffects.Remove(s));
            WhenOverTimeChanged(null);
        }

        public void WhenOverTimeChanged(AbilityEffectEvent abilityEffectEvent)
        {
            OverTimeChanged?.Invoke(this, null);
        }
        public void WhenResourceChanged(AbilityEffectEvent abilityEffectEvent)
        {
            ResourcesChanged?.Invoke(this, new FighterEvent(Position, abilityEffectEvent));
        }
        public void WhenTakeEffect(AbilityEffectEvent abilityEffectEvent)
        {
            EffectTaked?.Invoke(this, new FighterEvent(Position, abilityEffectEvent));
        }

        public void WhenPositionChanged(AbilityEffectEvent abilityEffectEvent)
        {
            PositionChanged?.Invoke(this, new FighterEvent(Position, abilityEffectEvent));
        }

    }
}

