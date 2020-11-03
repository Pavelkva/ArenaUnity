
using System.Xml.Serialization;

namespace characters
{
    public class AttributeBonus : AttributeAbstract, IAttributeBonusOrRes, IAttribute
    {
        public enum Bonus
        {
            BONUSHP = 200,
            BONUSMANA = 201,
            BONUSAP = 202,
            BONUSSTRENGTH = 203,
            BONUSAGILITY = 204,
            BONUSINTELLECT = 205,
            BONUSATTACKPOWER = 206,
            BONUSMAXDAMAGE = 207,
            BONUSMINDAMAGE = 208,
            BONUSDODGECHANCE = 209,
            BONUSCRITCHANCE = 210,
            BONUSSPELLPOWER = 211,
            BONUSARMOR = 212,
        }
        public new Bonus Attribute { get; set; }
        public override float Value { get; set; }
    }
}

