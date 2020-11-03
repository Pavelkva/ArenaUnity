using UnityEngine.UIElements.Experimental;

namespace characters
{
    public class AttributeBase : AttributeAbstract, IAttribute
    {
        public enum Base  {
            BASEHP = 100,
            BASEMANA = 101,
            BASEAP = 102,
            BASESTRENGTH = 103,
            BASEAGILITY = 104,
            BASEINTELLECT = 105,
        }
        public new Base Attribute { get; set; }
        public override float Value {get; set;}
    }
}

