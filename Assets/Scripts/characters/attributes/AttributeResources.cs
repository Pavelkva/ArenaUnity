namespace characters {
    public class AttributeResources : AttributeAbstract, IAttributeBonusOrRes, IAttribute
    {
        public enum Resources
        {
            ACTUALHP = 300,
            ACTUALMANA = 301,
            ACTUALAP = 302,
        }
        public override float Value { get; set; }
        public new Resources Attribute { get; set; }

        public AttributeSum.Sum MaxValueType 
        { 
            get 
            { 
                switch (Attribute)
                {
                    case Resources.ACTUALHP:
                        return AttributeSum.Sum.MAXHP;
                    case Resources.ACTUALMANA:
                        return AttributeSum.Sum.MAXMANA;
                    case Resources.ACTUALAP:
                        return AttributeSum.Sum.MAXACTIONPOINTS;
                    default:
                        return 0;

                }
            } 
            private set { } 
        }
    }

}
