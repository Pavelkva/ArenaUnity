using UnityEngine;

namespace characters
{
    public interface ICharacterAttributes
    {
        AttributeBase GetAttribute(AttributeBase.Base attribute);
        AttributeBonus GetAttribute(AttributeBonus.Bonus attribute);
        AttributeResources GetAttribute(AttributeResources.Resources attribute);
        AttributeSum GetAttribute(AttributeSum.Sum attribute);
        IAttribute GetAttribute(AttributeAbstract.Type attribute);
    }
}

