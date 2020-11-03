using characters;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using UnityEngine;

namespace Items
{
    public class Equipment : Item
    {
        public enum Parts
        {
            Armor = 0,
            Boots = 1,
            Gloves = 2,
            Helmet = 3,
            Pants = 4,
            MainHand = 5,
            OffHand = 6,
            Cape = 7,
        }

        public Parts Part { get; set; }
        private List<AttributeBonus> bonus;
        public List<AttributeBonus> Bonus { get { return bonus; } private set { } }

        public Equipment()
        {
            bonus = new List<AttributeBonus>();
        }
        
    }
}


