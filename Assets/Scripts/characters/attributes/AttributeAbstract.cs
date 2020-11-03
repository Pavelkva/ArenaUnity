using characters;
using System;
using System.Xml.Serialization;

namespace characters
{
    public abstract class AttributeAbstract : IAttribute
    {
        public enum Type
        {
            MAXHP = AttributeSum.Sum.MAXHP,
            MAXMANA = AttributeSum.Sum.MAXMANA,
            MAXACTIONPOINTS = AttributeSum.Sum.MAXACTIONPOINTS,
            STRENGTH = AttributeSum.Sum.STRENGTH,
            AGILITY = AttributeSum.Sum.AGILITY,
            INTELLECT = AttributeSum.Sum.INTELLECT,
            ATTACKPOWER = AttributeSum.Sum.ATTACKPOWER,
            MAXDAMAGE = AttributeSum.Sum.MAXDAMAGE,
            MINDAMAGE = AttributeSum.Sum.MINDAMAGE,
            DAMAGE = AttributeSum.Sum.DAMAGE,
            DODGECHANCE = AttributeSum.Sum.DODGECHANCE,
            CRITCHANCE = AttributeSum.Sum.CRITCHANCE,
            SPELLPOWER = AttributeSum.Sum.SPELLPOWER,
            ARMOR = AttributeSum.Sum.ARMOR,

            ACTUALHP = AttributeResources.Resources.ACTUALHP,
            ACTUALMANA = AttributeResources.Resources.ACTUALMANA,
            ACTUALAP = AttributeResources.Resources.ACTUALAP,

            BONUSHP = AttributeBonus.Bonus.BONUSHP,
            BONUSMANA = AttributeBonus.Bonus.BONUSMANA,
            BONUSAP = AttributeBonus.Bonus.BONUSAP,
            BONUSSTRENGTH = AttributeBonus.Bonus.BONUSSTRENGTH,
            BONUSAGILITY = AttributeBonus.Bonus.BONUSAGILITY,
            BONUSINTELLECT = AttributeBonus.Bonus.BONUSINTELLECT,
            BONUSATTACKPOWER = AttributeBonus.Bonus.BONUSATTACKPOWER,
            BONUSMAXDAMAGE = AttributeBonus.Bonus.BONUSMAXDAMAGE,
            BONUSMINDAMAGE = AttributeBonus.Bonus.BONUSMINDAMAGE,
            BONUSDODGECHANCE = AttributeBonus.Bonus.BONUSDODGECHANCE,
            BONUSCRITCHANCE = AttributeBonus.Bonus.BONUSCRITCHANCE,
            BONUSSPELLPOWER = AttributeBonus.Bonus.BONUSSPELLPOWER,
            BONUSARMOR = AttributeBonus.Bonus.BONUSARMOR,

            BASEHP = AttributeBase.Base.BASEHP,
            BASEAP = AttributeBase.Base.BASEAP,
            BASEMANA = AttributeBase.Base.BASEMANA,
            BASESTRENGTH = AttributeBase.Base.BASESTRENGTH,
            BASEAGILITY = AttributeBase.Base.BASEAGILITY,
            BASEINTELLECT = AttributeBase.Base.BASEINTELLECT,

        }

        [XmlIgnore]
        public Type Attribute { get; set; }
        public abstract float Value { get; set; }
    }
}

