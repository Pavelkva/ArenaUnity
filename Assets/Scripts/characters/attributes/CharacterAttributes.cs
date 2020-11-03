using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

namespace characters
{
    public class CharacterAttributes : ICharacterAttributes
    {
        List<IAttribute> attributeMap;
        public CharacterAttributes()
        {
            attributeMap = new List<IAttribute>();
            foreach (AttributeBase.Base attributeBaseEnum in Enum.GetValues(typeof(AttributeBase.Base)))
            {
                AttributeBase attributeBase = new AttributeBase();
                attributeBase.Attribute = attributeBaseEnum;
                attributeBase.Value = 0;
                attributeMap.Add(attributeBase);
            }
            foreach (AttributeBonus.Bonus attributeBonusEnum in Enum.GetValues(typeof(AttributeBonus.Bonus)))
            {
                AttributeBonus attributeBonus = new AttributeBonus();
                attributeBonus.Attribute = attributeBonusEnum;
                attributeBonus.Value = 0;
                attributeMap.Add(attributeBonus);
            }
            foreach (AttributeSum.Sum attributeSumEnum in Enum.GetValues(typeof(AttributeSum.Sum)))
            {
                AttributeSum attributeSum = new AttributeSum();
                attributeSum.Attribute = attributeSumEnum;
                attributeSum.Value = 0;
                attributeMap.Add(attributeSum);
            }
            foreach (AttributeResources.Resources attributeResourcesEnum in Enum.GetValues(typeof(AttributeResources.Resources)))
            {
                AttributeResources attributeResources = new AttributeResources();
                attributeResources.Attribute = attributeResourcesEnum;
                switch (attributeResources.Attribute)
                {
                    case AttributeResources.Resources.ACTUALHP:
                        attributeResources.Value = GetAttribute(AttributeSum.Sum.MAXHP).Value;
                        break;
                    case AttributeResources.Resources.ACTUALMANA:
                        attributeResources.Value = GetAttribute(AttributeSum.Sum.MAXMANA).Value;
                        break;
                    case AttributeResources.Resources.ACTUALAP:
                        attributeResources.Value = GetAttribute(AttributeSum.Sum.MAXACTIONPOINTS).Value;
                        break;
                }
                attributeMap.Add(attributeResources);
            }
        }

        public AttributeBase GetAttribute(AttributeBase.Base attribute)
        {
            foreach (IAttribute attributeFromMap in attributeMap)
            {
                if (attributeFromMap is AttributeBase && ((AttributeBase)attributeFromMap).Attribute == attribute)
                {
                    return (AttributeBase)attributeFromMap;
                }
            }
            return null;
        }

        public AttributeBonus GetAttribute(AttributeBonus.Bonus attribute)
        {
            foreach (IAttribute attributeFromMap in attributeMap)
            {
                if (attributeFromMap is AttributeBonus && ((AttributeBonus)attributeFromMap).Attribute == attribute)
                {
                    return (AttributeBonus)attributeFromMap;
                }
            }
            return null;
        }

        public AttributeResources GetAttribute(AttributeResources.Resources attribute)
        {
            foreach (IAttribute attributeFromMap in attributeMap)
            {
                if (attributeFromMap is AttributeResources && ((AttributeResources)attributeFromMap).Attribute == attribute)
                {
                    if (((AttributeResources)attributeFromMap).Attribute == AttributeResources.Resources.ACTUALAP && 
                        GetAttribute(AttributeSum.Sum.MAXACTIONPOINTS).Value < attributeFromMap.Value)
                    {
                        attributeFromMap.Value = GetAttribute(AttributeSum.Sum.MAXACTIONPOINTS).Value;
                    }
                    if (((AttributeResources)attributeFromMap).Attribute == AttributeResources.Resources.ACTUALHP &&
                        GetAttribute(AttributeSum.Sum.MAXHP).Value < attributeFromMap.Value)
                    {
                        attributeFromMap.Value = GetAttribute(AttributeSum.Sum.MAXHP).Value;
                    }
                    if (((AttributeResources)attributeFromMap).Attribute == AttributeResources.Resources.ACTUALMANA &&
                        GetAttribute(AttributeSum.Sum.MAXMANA).Value < attributeFromMap.Value)
                    {
                        attributeFromMap.Value = GetAttribute(AttributeSum.Sum.MAXMANA).Value;
                    }
                    return (AttributeResources)attributeFromMap;
                }
            }
            return null;
        }

        public IAttribute GetAttribute(AttributeAbstract.Type attributeType)
        {
            {
                foreach (AttributeBase.Base att in Enum.GetValues(typeof(AttributeBase.Base)))
                {
                    if (att.ToString().Equals(attributeType.ToString()))
                    {
                        return GetAttribute(att);
                    }
                }
                foreach (AttributeBonus.Bonus att in Enum.GetValues(typeof(AttributeBonus.Bonus)))
                {
                    if (att.ToString().Equals(attributeType.ToString()))
                    {
                        return GetAttribute(att);
                    }
                }
                foreach (AttributeResources.Resources att in Enum.GetValues(typeof(AttributeResources.Resources)))
                {
                    if (att.ToString().Equals(attributeType.ToString()))
                    {
                        return GetAttribute(att);
                    }
                }
                foreach (AttributeSum.Sum att in Enum.GetValues(typeof(AttributeSum.Sum)))
                {
                    if (att.ToString().Equals(attributeType.ToString()))
                    {
                        return GetAttribute(att);
                    }
                }
                return null;
            }

        }

        public AttributeSum GetAttribute(AttributeSum.Sum attribute)
        {
            foreach (IAttribute attributeFromMap in attributeMap)
            {
                if (attributeFromMap is AttributeSum && ((AttributeSum)attributeFromMap).Attribute == attribute)
                {
                    switch (((AttributeSum)attributeFromMap).Attribute)
                    {
                        case AttributeSum.Sum.MAXHP:
                            attributeFromMap.Value =
                                GetAttribute(AttributeBase.Base.BASEHP).Value +
                                GetAttribute(AttributeBonus.Bonus.BONUSHP).Value +
                                GetAttribute(AttributeSum.Sum.STRENGTH).Value * 10;
                            return (AttributeSum)attributeFromMap;
                        case AttributeSum.Sum.MAXMANA:
                            attributeFromMap.Value =
                                GetAttribute(AttributeBase.Base.BASEMANA).Value +
                                GetAttribute(AttributeBonus.Bonus.BONUSMANA).Value +
                                GetAttribute(AttributeSum.Sum.INTELLECT).Value * 10;
                            return (AttributeSum)attributeFromMap;
                        case AttributeSum.Sum.MAXACTIONPOINTS:
                            attributeFromMap.Value =
                                GetAttribute(AttributeBase.Base.BASEAP).Value +
                                GetAttribute(AttributeBonus.Bonus.BONUSAP).Value +
                                GetAttribute(AttributeSum.Sum.AGILITY).Value * 0.5f;
                            return (AttributeSum)attributeFromMap;
                        case AttributeSum.Sum.STRENGTH:
                            attributeFromMap.Value =
                                GetAttribute(AttributeBase.Base.BASESTRENGTH).Value +
                                GetAttribute(AttributeBonus.Bonus.BONUSSTRENGTH).Value;
                            return (AttributeSum)attributeFromMap;
                        case AttributeSum.Sum.AGILITY:
                            attributeFromMap.Value =
                                GetAttribute(AttributeBase.Base.BASEAGILITY).Value +
                                GetAttribute(AttributeBonus.Bonus.BONUSAGILITY).Value;
                            return (AttributeSum)attributeFromMap;
                        case AttributeSum.Sum.INTELLECT:
                            attributeFromMap.Value =
                                GetAttribute(AttributeBase.Base.BASEINTELLECT).Value +
                                GetAttribute(AttributeBonus.Bonus.BONUSINTELLECT).Value;
                            return (AttributeSum)attributeFromMap;
                        case AttributeSum.Sum.ATTACKPOWER:
                            attributeFromMap.Value =
                                GetAttribute(AttributeBonus.Bonus.BONUSATTACKPOWER).Value +
                                GetAttribute(AttributeSum.Sum.STRENGTH).Value;
                            return (AttributeSum)attributeFromMap;
                        case AttributeSum.Sum.SPELLPOWER:
                            attributeFromMap.Value =
                                GetAttribute(AttributeBonus.Bonus.BONUSSPELLPOWER).Value +
                                GetAttribute(AttributeSum.Sum.INTELLECT).Value;
                            return (AttributeSum)attributeFromMap;
                        case AttributeSum.Sum.MAXDAMAGE:
                            attributeFromMap.Value =
                                GetAttribute(AttributeSum.Sum.ATTACKPOWER).Value +
                                GetAttribute(AttributeBonus.Bonus.BONUSMAXDAMAGE).Value;
                            return (AttributeSum)attributeFromMap;
                        case AttributeSum.Sum.MINDAMAGE:
                            attributeFromMap.Value =
                                GetAttribute(AttributeSum.Sum.ATTACKPOWER).Value +
                                GetAttribute(AttributeBonus.Bonus.BONUSMINDAMAGE).Value;
                            return (AttributeSum)attributeFromMap;
                        case AttributeSum.Sum.DAMAGE:
                            attributeFromMap.Value = Utils.GetRandomInt((int)GetAttribute(AttributeSum.Sum.MINDAMAGE).Value, (int)GetAttribute(AttributeSum.Sum.MAXDAMAGE).Value);
                            return (AttributeSum)attributeFromMap;
                        case AttributeSum.Sum.CRITCHANCE:
                            attributeFromMap.Value =
                                GetAttribute(AttributeBonus.Bonus.BONUSCRITCHANCE).Value +
                                GetAttribute(AttributeSum.Sum.AGILITY).Value * 0.5f;
                            return (AttributeSum)attributeFromMap;
                        case AttributeSum.Sum.DODGECHANCE:
                            attributeFromMap.Value =
                                GetAttribute(AttributeBonus.Bonus.BONUSDODGECHANCE).Value +
                                GetAttribute(AttributeSum.Sum.AGILITY).Value * 0.5f;
                            return (AttributeSum)attributeFromMap;
                        case AttributeSum.Sum.ARMOR:
                            attributeFromMap.Value =
                                GetAttribute(AttributeBonus.Bonus.BONUSARMOR).Value;
                            return (AttributeSum)attributeFromMap;
                    }

                }
            }
            return null;
        }

    }
}

