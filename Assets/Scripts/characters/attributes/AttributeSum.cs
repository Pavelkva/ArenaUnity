namespace characters
{
    public class AttributeSum : AttributeAbstract, IAttribute
    {
        public enum Sum
        {
            MAXHP = 400,
            MAXMANA = 401,
            MAXACTIONPOINTS = 402,
            STRENGTH = 403,
            AGILITY = 404,
            INTELLECT = 405,
            ATTACKPOWER = 406,
            MAXDAMAGE = 407,
            MINDAMAGE = 408,
            DAMAGE = 409,
            DODGECHANCE = 410,
            CRITCHANCE = 411,
            SPELLPOWER = 412,
            ARMOR = 413,
        }

        public new Sum Attribute { get; set; }
        public override float Value { get; set; }

    }
}

